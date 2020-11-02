using System;
namespace MSNPClient.Exceptions
{
    /// <summary>
    /// Thrown whenever the server doesn't support the protocol
    /// </summary>
    [Serializable]
    public class InvalidAuthenticationException : Exception
    {
        public InvalidAuthenticationException()
            : base("The username and/or password is incorrect.")
        {
        }


    }
}
