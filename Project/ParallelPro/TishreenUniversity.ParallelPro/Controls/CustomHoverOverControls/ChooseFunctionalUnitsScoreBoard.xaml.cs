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

namespace TishreenUniversity.ParallelPro.Controls
{
    /// <summary>
    /// Interaction logic for ChooseFunctionalUnitsScoreBoard.xaml
    /// </summary>
    public partial class ChooseFunctionalUnitsScoreBoard : BaseUserControl
    {
        public ChooseFunctionalUnitsScoreBoard()
        {
            InitializeComponent();
            this.AnimateInAnimationType = AnimationTypes.None;
            this.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.FadeOutAsync();
        }
    }
}
