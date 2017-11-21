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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //Get the button that sent the event
            Button button = sender as Button;
            
            //Get the close and open textblocks that holds the arrow shapes
            TextBlock closeArrow = button.Template.FindName("closeArrow", button) as TextBlock;
            TextBlock openArrow = button.Template.FindName("openArrow", button) as TextBlock;

            //If the side menu is already open
            if(openArrow.Visibility == Visibility.Visible)
            {
                //Hide the menu
                await SideAlgo.SlideInFromLeftAsync();

                //Show the open arrow
                openArrow.Visibility = Visibility.Hidden;
                closeArrow.Visibility = Visibility.Visible;
            }
            //Else if the window is closed
            else
            {
                //Hide the menu
                await SideAlgo.SlideOutToLeftAsync();

                //Show the open arrow
                closeArrow.Visibility = Visibility.Hidden;
                openArrow.Visibility = Visibility.Visible;
            }
        }
    }
}
