using GeneralHelpers.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace TishreenUniversity.ParallelPro.Controls
{
    /// <summary>
    /// Interaction logic for AdminSettingsControl.xaml
    /// </summary>
    public partial class AdminSettingsControl : BaseUserControl
    {
        public AdminSettingsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gose to the application page for tests
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenProgram_Click(object sender, RoutedEventArgs e)
        {
            //Go to the application page
            IoC.Appliation.CurrentPage = ApplicationPages.MainWindow;
        }
        /// <summary>
        /// Gets the user back to login page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            //Go to the login page
            IoC.Appliation.CurrentPage = ApplicationPages.LoginPage;
        }


        private void ChangeUsername_Click(object sender,RoutedEventArgs e)
        {

            var box = MessageBox.Show("Confirm Change username to " + AdminUsername.Text, "Conform Change", MessageBoxButton.OKCancel);

            if(box == MessageBoxResult.OK)
            {
                Properties.Settings.Default.AdminUsername = AdminUsername.Text.ToLower();

                try
                {
                    Properties.Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
               var  message = string.Format("Success!\nNew admin username = {0}", AdminUsername.Text);
                MessageBox.Show(message, "Saved Username!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

        }
        private void SaveData_Click(object sender, RoutedEventArgs e)
        {
            //Get the button that sent the event
            var button = (sender as Button);

            //Get the name of the button
            var name = button.Name;
            
            //Get the right password
            var password = name == "admin" ? AdminPassword : TeacherPassword;
            //Conformation message
            var box = MessageBox.Show("Confirm Change " + name + " password = " + password.Password, "Conform Change", MessageBoxButton.OKCancel);

            if (box == MessageBoxResult.OK)
            {

                if (name == "admin")
                {
                    //Generate the salts for the new passwords
                    var adminSalt = CheckPassword.GenerateSalt(8);

                    //Set the values
                    Properties.Settings.Default.Password = Convert.ToBase64String(CheckPassword.GenerateHash(Encoding.UTF8.GetBytes(AdminPassword.Password), adminSalt, 1000));
                    Properties.Settings.Default.AdminSalt = Convert.ToBase64String(adminSalt);
                }
                else
                {
                    //Create the salt for the new password
                    var teacherSalt = CheckPassword.GenerateSalt(8);

                    //Set the values
                    Properties.Settings.Default.TeacherPassword = Convert.ToBase64String(CheckPassword.GenerateHash(Encoding.UTF8.GetBytes(TeacherPassword.Password), teacherSalt, 1000));
                    Properties.Settings.Default.TeacherSalt = Convert.ToBase64String(teacherSalt);
                }
                try
                {
                    Properties.Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var message = string.Format("Success!\nNew {0} password = {1}",name,password.Password);
                MessageBox.Show(message, "Saved Passwords!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
    }
}
