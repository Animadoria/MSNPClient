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

            if (int.TryParse(split[0], out _))
            {
                return new CommandResult()
                {
                    Error = true,
                    Command = split[0]
                };
            }
            else
            {
                int.TryParse(split[1], out int trID);
                return new CommandResult
                {
                    Command = split[0],
                    TransactionID = trID,
                    ResultArgs = string.Join(' ', split.Skip(trID == 0 ? 1 : 2))
                };
            }
        }
    }
}
