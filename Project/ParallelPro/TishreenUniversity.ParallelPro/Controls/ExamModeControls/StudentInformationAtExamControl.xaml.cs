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
using Tishreen.ParallelPro.Core;

namespace TishreenUniversity.ParallelPro.Controls
{
    /// <summary>
    /// Interaction logic for StudentInformationAtExamControl.xaml
    /// </summary>
    public partial class StudentInformationAtExamControl : BaseUserControl<StudentExamInformationViewModel>
    {
        public StudentInformationAtExamControl()
        {
            InitializeComponent();
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

    }
}
