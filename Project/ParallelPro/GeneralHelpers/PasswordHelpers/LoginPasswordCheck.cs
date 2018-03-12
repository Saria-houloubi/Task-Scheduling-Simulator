using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GeneralHelpers.Password
{
    /// <summary>
    /// Helper class to check password with 
    /// </summary>
    public static class CheckPassword
    {
        #region Methods

        #region MD5
        /// <summary>
        /// Gets the Hased password and builds it 
        /// </summary>
        /// <param name="md5"></param>
        /// <param name="password">The password that we want to hash</param>
        /// <returns></returns>
        public static async Task<String> GetMd5Hash(string password)
        {
            MD5 md5 = MD5.Create();

            //converting the inpute into bytes
            byte[] data = await Task<byte[]>.Run(() => md5.ComputeHash(Encoding.UTF8.GetBytes(password)));

            //creating a stringBuilder to collect the bytes
            StringBuilder sBuilder = new StringBuilder();

            //formating the data into hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        #endregion


        #region PBKDF

        /// <summary>
        /// Function to genrate the salt 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GenerateSalt(int length)
        {
            var bytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }
        /// <summary>
        /// Get the hased password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GenerateHash(byte[] password, byte[] salt, int iterations, int length = 16)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return deriveBytes.GetBytes(length);
            }
        }
        #endregion
        /// <summary>
        /// Vertify if the hash is equle thet inputed password
        /// </summary>
        /// <param name="hash">The Hased password that we have in the database</param>
        /// <param name="inpute">The password that the user inputs</param>
        /// <returns></returns>
        public static bool VertifyPassword(string hash, string inpute)
        {
            //getting a string compare to compare the two hashs
            StringComparer sComparer = StringComparer.OrdinalIgnoreCase;

            return (0 == sComparer.Compare(hash, inpute));
        }



        #endregion
    }
}
