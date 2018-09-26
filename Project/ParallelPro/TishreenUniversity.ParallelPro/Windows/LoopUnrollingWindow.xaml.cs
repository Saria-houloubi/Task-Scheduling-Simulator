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
using Tishreen.ParallelPro.Core.ViewModels.LoopUnrolling;

namespace TishreenUniversity.ParallelPro.Windows
{
    /// <summary>
    /// Interaction logic for LoopUnrollingWindow.xaml
    /// </summary>
    public partial class LoopUnrollingWindow : Window
    {
        public LoopUnrollingWindow()
        {
            InitializeComponent();

            DataContext = new LoopUnrollingWindowViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyTab.SelectedIndex = 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyTab.SelectedIndex = 2;

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MyTab.SelectedIndex = 3;

        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MyTab.SelectedIndex = 4;

        }
    }
}
