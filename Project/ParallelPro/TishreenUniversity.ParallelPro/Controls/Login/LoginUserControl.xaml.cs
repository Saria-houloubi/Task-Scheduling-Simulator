using GeneralHelpers.Password;
using System;
using System.Collections.Generic;
using System.IO;
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
using Tishreen.ParallelPro.Core;
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

            //Set the exam mode status
            IoC.Appliation.IsExamMode = Properties.Settings.Default.IsExamMode;

        }

        private void Login(object sender, RoutedEventArgs e)
        {
            //when the checkbox is not check means the user is not a student
            if (!(bool)checkbox.IsChecked && Username.Text.ToLower() != Properties.Settings.Default.AdminUsername)
            {
                MessageBox.Show("Username is invalid\n please check and try again!", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //Send the password for check
            var check = LoginViewModel.LoginCheck(Username.Text, Password.Password, Properties.Settings.Default.Password, Properties.Settings.Default.AdminSalt,(bool)checkbox.IsChecked?UserTypes.student:UserTypes.admin);

            if (!check)
                MessageBox.Show("Password are is incorrect\n please check and try again!", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// The event when the exam button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExamMode_Click(object sender, RoutedEventArgs e) => IoC.Appliation.CurrentPage = ApplicationPages.ExamStudentInformation;
        
    }
}
