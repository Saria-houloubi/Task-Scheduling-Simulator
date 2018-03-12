using ThishreenUniversity.ParallelPro.Enums;
using Tishreen.ParallelPro.Core;
using System.Text;
using GeneralHelpers.Password;
using System;
using Ninject;

namespace Tishreen.Tishreen.ParallelPro.Core
{
    /// <summary>
    /// The logic for the login procces
    /// </summary>
    public static class LoginViewModel
    {
        /// <summary>
        /// Checks if the right password and username for the login proccess and sets the restrictions if the user is a student
        /// </summary>
        /// <param name="username">The username for the user</param>
        /// <param name="password">The user password</param>
        /// <param name="userType">What user is loginning in admin of a student</param>
        /// <returns></returns>
        public static bool LoginCheck(string username, string inputPasssword, string password, string salt, UserTypes userType)
        {
            //Convert the salt back to a byte array
            var byteSalt = Convert.FromBase64String(salt);

            //The hased password for the authentication
            var hasedPassword = Convert.ToBase64String(CheckPassword.GenerateHash(Encoding.UTF8.GetBytes(inputPasssword), byteSalt, 1000));

            //Check if the password was correct
            if (CheckPassword.VertifyPassword(hasedPassword, password))
            {
                //Show up the admin menu
                IoC.Kernel.Get<ApplicationViewModel>().CurrentPage = ApplicationPages.AdminSettings;
                return true;
            }
            return false;
        }
    }
}
