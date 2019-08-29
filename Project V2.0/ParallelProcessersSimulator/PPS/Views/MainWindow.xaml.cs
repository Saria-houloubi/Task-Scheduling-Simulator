using PPS.UI.Shared.Views.Base;

namespace PPS.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : BaseWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The function to open the main code write window
        /// the only place allowed to be attached to the view 
        /// all other functions must be as commands
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenMain_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Create the window object
            MainCodeWriteWindow mainCodeWriteWindow = new MainCodeWriteWindow();
            //Show the window to the user
            mainCodeWriteWindow.Show();
            //Close this window
            this.Close();
        }
    }
}
