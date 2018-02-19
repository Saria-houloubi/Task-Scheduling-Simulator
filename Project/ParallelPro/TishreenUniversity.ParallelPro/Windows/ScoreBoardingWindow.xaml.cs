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
using ThishreenUniversity.ParallelPro.Enums;
using Tishreen.ParallelPro.Core.ViewModels;

namespace TishreenUniversity.ParallelPro.Windows
{
    /// <summary>
    /// Interaction logic for ScoreBoardingWindow.xaml
    /// </summary>
    public partial class ScoreBoardingWindow : Window
    {
        public ScoreBoardingWindow(List<object> instructionList,List<KeyValuePair<FunctionsTypes,int>> functionClockCycles)
        {
            InitializeComponent();

            //Bind the window with its logic
            this.DataContext = new ScoreBoardingWindowViewModel(instructionList, functionClockCycles);
        }
    }
}
