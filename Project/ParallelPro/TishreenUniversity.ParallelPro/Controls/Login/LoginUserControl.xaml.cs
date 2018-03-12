using GeneralHelpers.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ThishreenUniversity.ParallelPro.Enums;
using Tishreen.Tishreen.ParallelPro.Core;

namespace TishreenUniversity.ParallelPro.Controls
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : BaseUserControl
    {
        public LoginUserControl()
        {
            InitializeComponent();
        }

        private void Login(object sender, RoutedEventArgs e)
        {

            //Send the password for check
            var check = LoginViewModel.LoginCheck("admin", Password.Password, Properties.Settings.Default.Password, Properties.Settings.Default.Salt, UserTypes.admin);

            if (!check)
                MessageBox.Show("Username or Password are not correct\n please check and try again!", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // var salt = CheckPassword.GenerateSalt(8);
        //Properties.Settings.Default.Password = Convert.ToBase64String(CheckPassword.GenerateHash(Encoding.UTF8.GetBytes("admin"),salt, 1000));
        // Properties.Settings.Default.Salt = Convert.ToBase64String(salt);
        // Properties.Settings.Default.Save();


    }
}
