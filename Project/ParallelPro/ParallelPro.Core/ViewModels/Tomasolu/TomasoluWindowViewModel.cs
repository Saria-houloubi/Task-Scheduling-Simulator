
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using ThishreenUniversity.ParallelPro.Enums;
using Tishreen.ParallelPro.Core.Models;
namespace Tishreen.ParallelPro.Core
{
    /// <summary>
    /// The view model and the logic for the Tomasolu alog
    /// </summary>
    public class TomasoluWindowViewModel : ScoreBoardingWindowViewModel
    {
        #region Constructer
        /// <summary>
        /// Default Constructer 
        /// </summary>
        /// <param name="instructionList">The list of instruction that the usere enterted</param>
        /// <param name="functionClockCycle">The clock cycles for each function unit</param>
        public TomasoluWindowViewModel(List<object> instructionList, Dictionary<FunctionsTypes, int> functionClockCycle) : base(instructionList, functionClockCycle)
        {
            algoName = "Tomasolu";
        }
        #endregion


        #region Overrides

        /// <summary>
        /// In the tomasolu there will be a register renaming that will happen in the right back 
        /// as it will not wait for the register to free up but will rename one and continue the other
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="instructions"></param>
        /// <param name="functionalUnits"></param>
        /// <param name="registers"></param>
        protected override void ExecuteInstrution(InstructionWithStatusModel instruction, List<InstructionWithStatusModel> instructions, List<FunctionalUnitWithStatusModel> functionalUnits, List<RegisterResultModel> registers)
        {
            //Get the unit that the instruction is working on
            var unit = functionalUnits.SingleOrDefault(item => item.WorkingInstructionID == instruction.ID);

            if (--unit.Time == 0)
                //Set the clock cycle that is completed executing on
                instruction.ExecuteCompletedCycle = ClockCycle;
            else if (unit.Time < 0)
            {
                unit.Time = 0;
                //Get the register the instruction is working on
                var register = registers.SingleOrDefault(reg => reg.InstructionReservedRegiseter.Contains(instruction.ID));
                //if it is not the last instruction that reserved the register
                if (register.InstructionReservedRegiseter.LastOrDefault() != instruction.ID)
                {
                    //Then rename the target register of the register renameing process
                    //This way will end the WAW hazard
                    instruction.TargetRegistery = "R1";
                    ////Save the new instructin
                    //var newInst = instruction;
                    ////Get the index to remove and insert in
                    //var index = Instructions.IndexOf(instruction);
                    ////Remove the item
                    //Instructions.RemoveAt(index);
                    ////Add the new item in the old index
                    //Instructions.Insert(index, newInst);
                    ////Change the View
                    //RaisePropertyChanged(nameof(Instructions));
                }
                //Always right back the value after the instruction finishs executing
                instruction.WriteBackCycle = ClockCycle;
                ClearUnitFunction(functionalUnits.SingleOrDefault(fcn => fcn.WorkingInstructionID == instruction.ID), registers);

            }
        }
        #endregion
    }
}