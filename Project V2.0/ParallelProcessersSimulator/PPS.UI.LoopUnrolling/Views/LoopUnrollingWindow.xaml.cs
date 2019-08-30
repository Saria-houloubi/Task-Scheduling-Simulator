using PPS.UI.Shared.Views.Base;
using System.Windows;

namespace PPS.UI.LoopUnrolling.Views
{
    /// <summary>
    /// Interaction logic for LoopUnrollingWindow.xaml
    /// </summary>
    public partial class LoopUnrollingWindow : BaseWindow
    {
        public LoopUnrollingWindow()
        {
            InitializeComponent();
        }

        private void MoveNextTab_Click(object sender, RoutedEventArgs e)
        {
            TabControl_Part.SelectedIndex = TabControl_Part.SelectedIndex + 1;
        }

       
    }
}
