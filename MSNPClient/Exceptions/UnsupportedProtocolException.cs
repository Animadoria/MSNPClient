using System;
namespace MSNPClient.Exceptions
{
    [Serializable]
    public class UnsupportedProtocolException : Exception
    {
        /// <summary>
        /// Thrown whenever the server doesn't support the protocol
        /// </summary>
        public UnsupportedProtocolException()
            : base("The specified protocol was not accepted by the server.")
        {
        }


    }
}
