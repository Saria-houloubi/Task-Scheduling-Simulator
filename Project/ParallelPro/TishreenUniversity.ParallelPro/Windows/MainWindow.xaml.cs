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
using TishreenUniversity.ParallelPro.Controls;

namespace TishreenUniversity.ParallelPro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void ExamModeTitle_Loaded(object sender, RoutedEventArgs e)
        {
            //If we are in exam mode
            if (Properties.Settings.Default.IsExamMode)
            {
                //Show the exam title
                ExamModeTitle.Visibility = Visibility.Visible;
            }
            else
            {
                //Hide the exam title
                ExamModeTitle.Visibility = Visibility.Collapsed; 
            }
        }
    }
}
