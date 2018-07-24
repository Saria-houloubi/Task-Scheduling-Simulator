using System.Collections.Generic;
using System.Collections.ObjectModel;
using ThishreenUniversity.ParallelPro.Enums;
using Tishreen.ParallelPro.Core.Models;

namespace Tishreen.ParallelPro.Core
{
    /// <summary>
    /// The logic for the code ,cycles and functional units window
    /// </summary>
    public class CodeCyclesAndFunctionalUnitInformationWindowViewModel : MainAlgorithmsInstructionMenuViewModel
    {
        #region Lists and collections
        /// <summary>
        /// The clock cycles for each functional unit
        /// </summary>
        public Dictionary<FunctionsTypes, int> FunctionClockCycle { get; set; }
        /// <summary>
        /// The number of functional units that are used
        /// </summary>
        public Dictionary<FunctionalUnitsTypes, int> FunctionUnitCount { get; set; }
        #endregion


        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public CodeCyclesAndFunctionalUnitInformationWindowViewModel(List<object> instructionModels,
                                                                    Dictionary<FunctionsTypes, int>  functionCycles,
                                                                    Dictionary<FunctionalUnitsTypes, int> functionsCount)
        {
            
            Instructions = new ObservableCollection<InstructionModel>();
            instructionModels.ForEach(item => Instructions.Add(item as InstructionModel));
            FunctionClockCycle= functionCycles;
            FunctionUnitCount = functionsCount;
        }
        #endregion
    }
}
