
using System.Collections.Generic;
using System.Threading.Tasks;
using ThishreenUniversity.ParallelPro.Enums;
using Tishreen.ParallelPro.Core.Models;

namespace Tishreen.ParallelPro.Core.ViewModels
{
    /// <summary>
    /// The view model and the logic for the scoreboarding alog
    /// </summary>
    public class ScoreBoardingWindowViewModel : BaseViewModel
    {

        #region Properties
        /// <summary>
        /// The list that holds all the instructions that the user writes
        /// </summary>
        public List<InstructionWithStatusModel> Instructions { get; set; }

        /// <summary>
        /// The amount of integer units that can execute functions like Load(LD) Store(SD)
        /// </summary>
        private int _numberOfIntegerUnits = 1;
        public int NumberOfIntegerUnits
        {
            get { return _numberOfIntegerUnits; }
            set { SetProperty(ref _numberOfIntegerUnits, value); }
        }
        /// <summary>
        /// The amount of add/sub units that can execute functions like ADD(LD) SUB(SD)
        /// </summary>
        private int _numberOfAddUnits = 1;
        public int NumberOfAddUnits
        {
            get { return _numberOfIntegerUnits; }
            set { SetProperty(ref _numberOfIntegerUnits, value); }
        }
        /// <summary>
        /// The number of units that can execute the Multiplcation functions MULT
        /// </summary>
        private int _numberOfMultiplyUnits = 2 ;
        public int NumberOfMultiplyUnits
        {
            get { return _numberOfMultiplyUnits; }
            set { SetProperty(ref _numberOfMultiplyUnits, value); }
        }
        /// <summary>
        /// The number of units that can execute the Divistion function DIVID
        /// </summary>
        private int _numberOfDivideUnits = 1;
        public int NumberOfDivideUnits
        {
            get { return _numberOfDivideUnits; }
            set { SetProperty(ref _numberOfDivideUnits, value); }
        }
        #endregion

        #region Constructer
        /// <summary>
        /// Constructer 
        /// </summary>
        /// <param name="instructionList">The list of instruction that the usere enterted</param>
        /// <param name="functionClockCycle">The clock cycles for each function unit</param>
        public ScoreBoardingWindowViewModel(List<object> instructionList, List<KeyValuePair<FunctionsTypes, int>> functionClockCycle)
        {
            FillInstructionList(instructionList);
        }
        /// <summary>
        /// Fills the instruction with status list on the start of the window
        /// </summary>
        /// <param name="instructionList">The instruction list from the user</param>
        private void FillInstructionList(List<object> instructionList)
        {
            Instructions = new List<InstructionWithStatusModel>();

            if (instructionList == null)
                return;
            //Loop throw the list...
            foreach (var item in instructionList)
            {
                var instruction = (InstructionModel)item;
                //Add it to the instruction with status list
                Instructions.Add(new InstructionWithStatusModel(instruction.ID, instruction.Name, instruction.TargetRegistery, instruction.SourceRegistery01, instruction.SourceRegistery02));
            }
        }
        #endregion
    }
}
