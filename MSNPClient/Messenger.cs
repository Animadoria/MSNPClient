using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using MSNPClient.Exceptions;
using Tcp.NET.Client;
using Tcp.NET.Client.Models;

namespace MSNPClient
{
    /// <summary>
    /// The main client class.
    /// </summary>
    public class Messenger
    {
        /// <summary>
        /// The MSNP server. Original was messenger.hotmail.com
        /// </summary>
        public string Server = "m1.escargot.log1p.xyz";

        /// <summary>
        /// The passport "Nexus". Original was https://nexus.passport.com/rdr/pprdr.asp
        /// </summary>
        public string Nexus = "https://m1.escargot.log1p.xyz/nexus-mock";

        /// <summary>
        /// The MSNP port.
        /// </summary>
        public int Port = 1863;

        /// <summary>
        /// The protocol version.
        /// </summary>
        public MSNPVersion ProtocolVersion = MSNPVersion.MSNP2;

        /// <summary>
        /// The command manager. Is created on the main constructor.
        /// </summary>
        public readonly CommandManager CommandManager;

        /// <summary>
        /// The TCP Client.
        /// </summary>
        public readonly ITcpNETClient TCPClient;

        /// <summary>
        /// Cached email.
        /// </summary>
        private string email;

        /// <summary>
        /// Main constructor. Sets up CommandManager
        /// </summary>
        public Messenger()
        {
            TCPClient = new TcpNETClient(new ParamsTcpClient()
            {
                Uri = Server,
                Port = Port,
                IsSSL = false
            });
            TCPClient.MessageEvent += TCPClient_MessageEvent;
            TCPClient.ConnectAsync().GetAwaiter().GetResult();
            CommandManager = new CommandManager(TCPClient);
        }

        private Task TCPClient_MessageEvent(object sender, Tcp.NET.Client.Events.Args.TcpMessageClientEventArgs args)
        {
            Console.WriteLine((args.MessageEventType == PHS.Networking.Enums.MessageEventType.Receive ? "S" : "C") + ": " + args.Message.Replace("\r\n", ""));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Connects to the network with the specified credentials.
        /// </summary>
        /// <param name="email">The e-mail address.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <example>
        /// <code>
        /// Messenger messenger = new Messenger();
        /// await messenger.Connect("some@email.com", "somepassword");
        /// </code>
        /// </example>
        /// <exception cref="Exceptions.UnsupportedProtocolException">Thrown when the specified protocol is not accepted by the server.</exception>
        /// <exception cref="InvalidAuthenticationException">Thrown when the specified credentials were not accepted by the server.</exception>
        public async Task Connect(string email, string password)
        {
            this.email = email;
            var ver = await CommandManager.SendCommandAsync("VER", ProtocolVersion.ToString() + " CVR0");
            if (ver.ResultArgs.StartsWith("0 "))
            {
                throw new UnsupportedProtocolException();
            }

            // CVR (with custom version... because why not?)
            await CommandManager.SendCommandAsync("CVR", "0x0409 csharp 3.1 i386 MSNMSGR 5.6.7.8 MSMSGS " + email);

            if ((int)ProtocolVersion < 8)
            {
                // INF
                var inf = await CommandManager.SendCommandAsync("INF", string.Empty);


                // Initial USR
                var iusr = await CommandManager.SendCommandAsync("USR", inf.ResultArgs + " I " + email);
                if (iusr.Error && iusr.Command == "911")
                    throw new InvalidAuthenticationException();

                var challenge = iusr.ResultArgs.Split(" ")[2];

                // Second USR
                var susr = await CommandManager.SendCommandAsync("USR", inf.ResultArgs + " S " + (challenge + password).ToMD5().ToLower());
                if (susr.Error && susr.Command == "911")
                    throw new InvalidAuthenticationException();

            }
            else
            {
                // Initial USR
                var iusr = await CommandManager.SendCommandAsync("USR", "TWN I " + email);
                var challenge = iusr.ResultArgs.Split(" ")[2];

                var ticket = "";

                using (HttpClient client = new HttpClient())
                {
                    var nexusGet = await client.GetAsync(Nexus);
                    var passportUrls = nexusGet.Headers.GetValues("Passporturls").First().Replace("DALogin=", "");

                    client.DefaultRequestHeaders.Add("Authorization", "Passport1.4 OrgVerb=GET,OrgURL=http%3A%2F%2Fmessenger%2Emsn%2Ecom,sign-in="
                        + HttpUtility.UrlEncode(email) + ",pwd=" + HttpUtility.UrlEncode(password)
                        + "," + challenge.Replace("\r\n", ""));

                    var loginGet = await client.GetAsync(passportUrls);
                    if (loginGet.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        throw new InvalidAuthenticationException();

                    ticket = loginGet.Headers.GetValues("Authentication-Info").First().Split('\'')[1];
                }

                await CommandManager.SendCommandAsync("USR", "TWN S " + ticket, false); //Should probably check for OK
            }
            //await CommandManager.SendCommandAsync("SYN", "0", false);
        }

        /// <summary>
        /// Sets the user's presence status and client identification.
        /// </summary>
        /// <param name="status">The presence status.</param>
        /// <param name="identification">The client identification. Default is none.</param>
        /// <returns></returns>
        public async Task SetPresence(PresenceStatus status, ClientIdentification identification = ClientIdentification.None)
        {
            await CommandManager.SendCommandAsync("CHG", Presence.PresenceCodes[status] + " " + (uint)identification);
        }

        /// <summary>
        /// Changes the user's nickname.
        /// </summary>
        /// <param name="newName">The new nickname.</param>
        /// <returns></returns>
        public async Task ChangeNickname(string newName)
        {
            if ((int)ProtocolVersion >= 10)
            {
                await CommandManager.SendCommandAsync("PRP", "MFN " + HttpUtility.UrlPathEncode(newName));
            }
            else
                await CommandManager.SendCommandAsync("REA", email + " " + HttpUtility.UrlPathEncode(newName));
        }
    }
}
