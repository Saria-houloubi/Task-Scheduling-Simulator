using Microsoft.Win32;
using Tishreen.ParallelPro.Core;
namespace TishreenUniversity.ParallelPro.Controls
{
    /// <summary>
    /// Interaction logic for InstructionMenuControl.xaml
    /// </summary>
    public partial class InstructionMenuControl : BaseUserControl
    {
        public InstructionMenuControl()
        {
            InitializeComponent();
        }

        private void ChoseTxtFileButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.CheckPathExists)
                {
                    //Set the path of the file to read and check its instructions
                    ((dynamic)this.DataContext).CodeTxtFilePath = openFileDialog.FileName;
                }
            }
        }
    }
}
