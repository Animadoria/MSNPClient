using System;
using System.Linq;

namespace MSNPClient
{
    public class CommandResult
    {
        public string Command { get; set; }
        public int TransactionID { get; set; }
        public string ResultArgs { get; set; }
        public bool Error { get; set; }

        public static CommandResult FromString(string input)
        {
            var split = input.Split(' ');

            return int.TryParse(split[0], out _)
                ? new CommandResult()
                {
                    Error = true,
                    Command = split[0]
                }
                : new CommandResult()
                {
                    Command = split[0],
                    TransactionID = int.Parse(split[1]),
                    ResultArgs = string.Join(' ', split.Skip(2))
                };
        }
    }
}
