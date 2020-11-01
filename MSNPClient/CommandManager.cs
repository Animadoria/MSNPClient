using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MSNPClient
{
    public class CommandManager
    {
        readonly TcpClient tcp;
        readonly NetworkStream networkStream;
        readonly StreamReader reader;

        private int transactionID = 1;

        public CommandManager(string server, int port)
        {
            tcp = new TcpClient(server, port);
            networkStream = tcp.GetStream();
            reader = new StreamReader(networkStream, Encoding.UTF8);
        }

        public async Task<CommandResult> SendCommandAsync(string command, string args)
        {
            string fullCommand = $"{command} {transactionID} {args}\r\n";
            Console.WriteLine("C: " + fullCommand.Replace("\r\n", ""));

            transactionID++;

            byte[] bytes = Encoding.UTF8.GetBytes(fullCommand);

            await networkStream.WriteAsync(bytes, 0, bytes.Length);

            var result = await reader.ReadLineAsync();
            Console.WriteLine("S: " + result);

            return CommandResult.FromString(result);
        }
    }
}
