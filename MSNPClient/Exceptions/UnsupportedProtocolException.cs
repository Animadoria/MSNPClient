using System;
namespace MSNPClient.Exceptions
{
    /// <summary>
    /// Thrown whenever the server doesn't support the protocol
    /// </summary>
    [Serializable]
    public class UnsupportedProtocolException : Exception
    {
        public UnsupportedProtocolException()
            : base("The specified protocol was not accepted by the server.")
        {
        }


    }
}
