using System;
using System.Text;

namespace MSNPClient
{
    /// <summary>
    /// Miscellaneous extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts a string to MD5.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static string ToMD5(this string input)
        { 
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }

        }

        /// <summary>
        /// Converts a string to hex.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static string ToHex(this string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var hex = BitConverter.ToString(bytes);
            return hex.Replace("-", "");
        }
    }
}
