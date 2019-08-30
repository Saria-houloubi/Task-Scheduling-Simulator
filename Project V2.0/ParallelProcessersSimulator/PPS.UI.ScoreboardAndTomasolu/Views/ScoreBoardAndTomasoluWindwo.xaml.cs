using PPS.UI.Shared.Views.Base;
using System.Windows;

namespace PPS.UI.ScoreboardAndTomasolu.Views
{
    /// <summary>
    /// Interaction logic for ScoreBoardAndTomasoluWindwo.xaml
    /// </summary>
    public partial class ScoreBoardAndTomasoluWindwo : BaseWindow
    {
        public ScoreBoardAndTomasoluWindwo()
        {
            InitializeComponent();
        }

        private void MoveToNextTab_Click(object sender, RoutedEventArgs e)
        {
            TabControl_Part.SelectedIndex = 1;
        }
    }
}
