using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Tcp.NET.Client;

namespace MSNPClient
{
    /// <summary>
    /// The command manager.
    /// This class manages all the commands.
    /// </summary>
    public class CommandManager
    {
        /// <summary>
        /// The current TransactionID. Increments every command.
        /// </summary>
        private int transactionID = 1;

        private string lastResponse = "";

        /// <summary>
        /// The TCP client.
        /// </summary>
        private readonly ITcpNETClient tcpClient;

        /// <summary>
        /// The CommandManager constructor.
        /// Should be initialized only one time!
        /// </summary>
        /// <param name="tcp">The TCP client.</param>
        public CommandManager(ITcpNETClient tcp)
        {
            tcpClient = tcp;
            tcpClient.MessageEvent += TcpClient_MessageEvent; ;
        }

        private Task TcpClient_MessageEvent(object sender, Tcp.NET.Client.Events.Args.TcpMessageClientEventArgs args)
        {
            if (args.MessageEventType == PHS.Networking.Enums.MessageEventType.Receive)
                lastResponse = args.Message;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Send a command.
        /// </summary>
        /// <param name="command">The command to send.</param>
        /// <param name="args">The command's arguments. </param>
        /// <returns>Returns a <c>CommandResult</c> with the command results.</returns>
        public async Task<CommandResult> SendCommandAsync(string command, string args, bool wait = true)
        {
            string fullCommand = $"{command} {transactionID} {args}\r\n";
           // Console.WriteLine("C: " + fullCommand.Replace("\r\n", ""));

            await tcpClient.SendToServerRawAsync(fullCommand);
            //await networkStream.WriteAsync(bytes, 0, bytes.Length);

            if (!wait) return null;

            while (string.IsNullOrWhiteSpace(lastResponse) || lastResponse.Split(' ').Length < 2 || lastResponse.Split(' ')[1] != transactionID.ToString())
            {
                await Task.Delay(1);
            }

            transactionID++;
            return CommandResult.FromString(lastResponse);
        }

    }
}
