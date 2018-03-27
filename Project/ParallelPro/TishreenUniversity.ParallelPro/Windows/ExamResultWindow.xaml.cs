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
using System.Windows.Shapes;
using Tishreen.ParallelPro.Core;

namespace TishreenUniversity.ParallelPro.Windows
{
    /// <summary>
    /// Interaction logic for ExamResultWindow.xaml
    /// </summary>
    public partial class ExamResultWindow : Window
    {

        #region Private memebers
        /// <summary>
        /// Flag represents if the user can exit out
        /// </summary>
        private bool canExit = false;
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public ExamResultWindow()
        {
            InitializeComponent();

            //Bind the context to the exam information view model after all data has been set
            this.DataContext = IoC.ExamInfo;
        }

        #endregion

        #region Eventes
        /// <summary>
        /// A event to stop the student from cloeasing the window with out the 
        /// teacher password confirmation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //If the user is not permissioned to exit
            if (!canExit)
            {
                //Cancel the exit and show the check password control
                e.Cancel = true;
                CheckPasswordControl.Visibility = Visibility.Visible;
            }
            //Else it will restart all the application for new user
            else
            {
                RestartApplication();
            }
        }
        /// <summary>
        /// The event to check if the inpute password is correct to exit out and delete data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckAndExitClick(object sender, RoutedEventArgs e)
        {
            //Convert the salt back to a byte array
            var byteSalt = Convert.FromBase64String(Properties.Settings.Default.TeacherSalt);

            //The hased password for the authentication
            var inputeHased = Convert.ToBase64String(CheckPassword.GenerateHash(Encoding.UTF8.GetBytes(TeacherPassword.Password), byteSalt, 1000));

            //If the password is correct then
            if (CheckPassword.VertifyPassword(Properties.Settings.Default.TeacherPassword, inputeHased))
            {
                MessageBox.Show("Correct password all data will be deleted and application will restart", "Password Checked!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                //Give permission to exit
                canExit = true;
                //And close and restart
                this.Close();
            }
            else
                MessageBox.Show("Wrong password plesase try again", "Password invalid!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        /// <summary>
        /// Hides the check password control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TeacherPassword.Password = null;
            CheckPasswordControl.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region Helpers

        /// <summary>
        /// Closes and restarts the application
        /// </summary>
        private void RestartApplication()
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        #endregion
    }
}
