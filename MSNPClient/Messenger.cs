using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using MSNPClient.Exceptions;

namespace MSNPClient
{
    /// <summary>
    /// The main client class. Idk what to type i only made this class right now
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
        public MSNPVersion ProtocolVersion = MSNPVersion.MSNP12;

        private CommandManager commandManager;

        /// <summary>
        /// Main constructor. Sets up CommandManager
        /// </summary>
        public Messenger()
        {
            commandManager = new CommandManager(Server, Port);
        }

        public async Task Connect(string email, string password)
        {
            var ver = await commandManager.SendCommandAsync("VER", ProtocolVersion.ToString() + " CVR0");
            if (ver.ResultArgs.StartsWith("0 "))
            {
                throw new UnsupportedProtocolException();
            }

            // CVR (with custom version... because why not?)
            await commandManager.SendCommandAsync("CVR", "0x0409 csharp 3.1 i386 MSNMSGR 4.20.Blaze.It MSMSGS " + email);

            if ((int)ProtocolVersion < 8)
            {
                // INF
                var inf = await commandManager.SendCommandAsync("INF", string.Empty);


                // Initial USR
                var iusr = await commandManager.SendCommandAsync("USR", inf.ResultArgs + " I " + email);
                if (iusr.Error && iusr.Command == "911")
                    throw new InvalidAuthenticationException();

                var challenge = iusr.ResultArgs.Split(" ")[2];

                // Second USR
                var susr = await commandManager.SendCommandAsync("USR", inf.ResultArgs + " S " + (challenge + password).ToMD5().ToLower());
                if (susr.Error && susr.Command == "911")
                    throw new InvalidAuthenticationException();
                
            }
            else
            {
                // Initial USR
                var iusr = await commandManager.SendCommandAsync("USR", "TWN I " + email);
                var challenge = iusr.ResultArgs.Split(" ")[2];

                var ticket = "";

                using (HttpClient client = new HttpClient())
                {
                    var nexusGet = await client.GetAsync(Nexus);
                    var passportUrls = nexusGet.Headers.GetValues("Passporturls").First().Replace("DALogin=", "");

                    client.DefaultRequestHeaders.Add("Authorization", "Passport1.4 OrgVerb=GET,OrgURL=http%3A%2F%2Fmessenger%2Emsn%2Ecom,sign-in="
                        + HttpUtility.UrlEncode(email) + ",pwd=" + HttpUtility.UrlEncode(password)
                        + "," + challenge);

                    var loginGet = await client.GetAsync(passportUrls);
                    if (loginGet.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        throw new InvalidAuthenticationException();
                    
                    ticket = loginGet.Headers.GetValues("Authentication-Info").First().Split('\'')[1];
                }

                var susr = await commandManager.SendCommandAsync("USR", "TWN S " + ticket);
            }
        }

        public async Task SetPresence(PresenceStatus status, ClientIdentification identification)
        {
            await commandManager.SendCommandAsync("CHG", Presence.PresenceCodes[status] + " " + identification);
        }
    }
}
