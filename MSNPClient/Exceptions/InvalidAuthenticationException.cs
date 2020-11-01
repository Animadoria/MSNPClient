using System;
namespace MSNPClient.Exceptions
{
    [Serializable]
    public class InvalidAuthenticationException : Exception
    {
        /// <summary>
        /// Thrown whenever the server doesn't support the protocol
        /// </summary>
        public InvalidAuthenticationException()
            : base("The username and/or password is incorrect.")
        {
        }


    }
}
