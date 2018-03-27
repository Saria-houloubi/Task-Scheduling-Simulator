using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Tishreen.ParallelPro.Core;

namespace TishreenUniversity.ParallelPro.Controls
{
    /// <summary>
    /// Interaction logic for MainWindowInstructionAndAlgoControl.xaml
    /// </summary>
    public partial class MainWindowInstructionAndAlgoControl : BaseUserControl<MainAlgorithmsInstructionMenuViewModel>
    {
        #region Helper Flags
        /// A flag represnets if the side function clock cycle is open
        /// </summary>
        private bool IsSideClockCycleMenuOpen = false;

        #endregion

        public MainWindowInstructionAndAlgoControl()
        {
            InitializeComponent();
            //Slide out the window once it is loaded
            SideClockCycles.Loaded += SideClockCycles_Loaded;
        }

        private async void SideClockCycles_Loaded(object sender, RoutedEventArgs e)
        {
            //Get the menu
            var sideMenu = (MainWindowFunctionClockCycles)sender;
            //This will run on the main ui thread
            await sideMenu.SlideOutToRightAsync();
        }

        private async void ChangeSideMenuState(object sender, RoutedEventArgs e)
        {
            //Get the sender
            Button button = (Button)sender;
           //then animate the menu to its respacte place
                if (IsSideClockCycleMenuOpen)
                    await SideClockCycles.SlideOutToRightAsync();
                else
                    await SideClockCycles.SlideInFromRightAsync();
                //Filp the value using xor gate
                IsSideClockCycleMenuOpen ^= true;
        }

    }
}
