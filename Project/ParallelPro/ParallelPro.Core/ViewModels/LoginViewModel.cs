using ThishreenUniversity.ParallelPro.Enums.Enums;
using System.Diagnostics;
using Tishreen.ParallelPro.Core.IoC;
using Ninject;
using Tishreen.ParallelPro.Core;
using ThishreenUniversity.ParallelPro.Enums.Instructions;

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
        public static bool LoginCheck(string username,string password,UserTypes userType)
        {
            switch (userType)
            {
                case UserTypes.admin:
                    if (username == "admin")
                        IoC.Kernel.Get<ApplicationViewModel>().CurrentPage = ApplicationPages.MainWindow;
                        return true;
                case UserTypes.student:
                    if (username == "student")
                        IoC.Kernel.Get<ApplicationViewModel>().CurrentPage = ApplicationPages.MainWindow;
                        return true;
                default:
                    Debugger.Break();
                    return false;
                    break;
            }
            return false;
        }
    }
}
