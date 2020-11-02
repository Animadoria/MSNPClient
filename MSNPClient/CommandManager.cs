using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MSNPClient
{
    /// <summary>
    /// The command manager.
    /// This class manages all the commands.
    /// </summary>
    public class CommandManager
    {
        /// <summary>
        /// The TCP Client.
        /// </summary>
        readonly TcpClient tcp;
        /// <summary>
        /// The network stream.
        /// </summary>
        readonly NetworkStream networkStream;
        /// <summary>
        /// The <c>StreamReader</c>.
        /// </summary>
        readonly StreamReader reader;

        /// <summary>
        /// The current TransactionID. Increments every command.
        /// </summary>
        private int transactionID = 1;

        /// <summary>
        /// The CommandManager constructor.
        /// Should be initialized only one time!
        /// </summary>
        /// <param name="server">The server to connect to.</param>
        /// <param name="port">The port to connect to.</param>
        public CommandManager(string server, int port)
        {
            tcp = new TcpClient(server, port);
            networkStream = tcp.GetStream();
            reader = new StreamReader(networkStream, Encoding.UTF8);
        }

        /// <summary>
        /// Send a command.
        /// </summary>
        /// <param name="command">The command to send.</param>
        /// <param name="args">The command's arguments. </param>
        /// <returns>Returns a <c>CommandResult</c> with the command results.</returns>
        public async Task<CommandResult> SendCommandAsync(string command, string args)
        {
            string fullCommand = $"{command} {transactionID} {args}\r\n";
            Console.WriteLine("C: " + fullCommand.Replace("\r\n", ""));

            transactionID++;

            byte[] bytes = Encoding.UTF8.GetBytes(fullCommand);

            await networkStream.WriteAsync(bytes, 0, bytes.Length);

            string result = "";
            string latest;
            do
            {
                var resultBytes = new byte[tcp.ReceiveBufferSize];
                await networkStream.ReadAsync(resultBytes, 0, tcp.ReceiveBufferSize);
                latest = Encoding.UTF8.GetString(resultBytes).Split('\0')[0];
                result += latest;
            }
            while (!latest.EndsWith("\r\n"));

            Console.WriteLine("S: " + result);
            return CommandResult.FromString(result);
        }

    }
}
