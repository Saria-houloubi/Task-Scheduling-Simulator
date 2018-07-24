using System.Collections.Generic;
using System.Windows;
using ThishreenUniversity.ParallelPro.Enums;
using Tishreen.ParallelPro.Core;

namespace TishreenUniversity.ParallelPro.Windows
{
    /// <summary>
    /// Interaction logic for CodeCyclesAndFunctionalUnitInformationWindow.xaml
    /// </summary>
    public partial class CodeCyclesAndFunctionalUnitInformationWindow : Window
    {
        public CodeCyclesAndFunctionalUnitInformationWindow(List<object> instructionModels,
                                                                    Dictionary<FunctionsTypes, int> functionCycles,
                                                                   Dictionary<FunctionalUnitsTypes, int> functionsCount)
        {
            InitializeComponent();

            this.DataContext = new CodeCyclesAndFunctionalUnitInformationWindowViewModel(instructionModels,functionCycles,functionsCount);
        }

    }
}
