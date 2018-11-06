using GeneralHelpers.Password;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using ThishreenUniversity.ParallelPro.Enums;
using Tishreen.ParallelPro.Core;

namespace TishreenUniversity.ParallelPro.Windows
{
    /// <summary>
    /// Interaction logic for ScoreBoardingWindow.xaml
    /// </summary>
    public partial class TomasoluWindow : Window
    {
        private bool passwordChecked = false;
        public TomasoluWindow(List<object> instructionList, Dictionary<FunctionsTypes, int> functionClockCycles)
        {
            InitializeComponent();

            //Bind the window with its logic
            this.DataContext = new TomasoluWindowViewModel(instructionList, functionClockCycles);
        }
        #region Private memebers
        /// <summary>
        /// Flag represents if the user can exit out
        /// </summary>
        private bool canExit = false;
        #endregion

        /// <summary>
        /// A event to stop the student from cloeasing the window with out the 
        /// teacher password confirmation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!passwordChecked && IoC.Appliation.IsExamMode)
            {
                //Cancel the exit and show the check password control
                e.Cancel = true;
                CheckForConformationControl.Visibility = Visibility.Visible;
            }
        }

        private void NextExambutton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Shows the password conformation window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckPasswordForRest_Click(object sender, RoutedEventArgs e)
        {
            CheckForConformationControl.Visibility = Visibility;
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
                MessageBox.Show("Correct password all data will be deleted", "Password Checked!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                //Give permission to exit
                passwordChecked = true;
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
            CheckForConformationControl.Visibility = Visibility.Collapsed;
        }
    }
}
