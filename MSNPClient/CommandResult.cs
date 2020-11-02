using System;
using System.Linq;

namespace MSNPClient
{
    /// <summary>
    /// A command result.
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// The string that represents the command.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// The Transaction ID. Returns 0 for a result without a transaction ID.
        /// </summary>
        public int TransactionID { get; set; }

        /// <summary>
        /// The result arguments.
        /// </summary>
        public string ResultArgs { get; set; }

        /// <summary>
        /// If an error was returned. The error code is present in <c>Command</c>.
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// Returns a <c>CommandResult</c>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
