
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// The list of functional units that we have
        /// </summary>
        public List<FunctionalUnitWithStatusModel> FunctionalUnits { get; set; }
        /// <summary>
        /// The list that holds all the registers that the user writes
        /// </summary>
        public List<RegisterResultModel> Registers { get; set; }
        /// <summary>
        /// The clock cycles for each functional unit
        /// </summary>
        public List<KeyValuePair<FunctionsTypes, int>> FunctionClockCycle { get; set; }
        /// <summary>
        /// The amount of integer units that can execute functions like Load(LD) Store(SD)
        /// </summary>
        private int _numberOfLoadUnits = 1;
        public int NumberOfLoadUnits
        {
            get { return _numberOfLoadUnits; }
            set { SetProperty(ref _numberOfLoadUnits, value); }
        }
        /// <summary>
        /// The amount of add/sub units that can execute functions like ADD(LD) SUB(SD)
        /// </summary>
        private int _numberOfAddUnits = 1;
        public int NumberOfAddUnits
        {
            get { return _numberOfAddUnits; }
            set { SetProperty(ref _numberOfAddUnits, value); }
        }
        /// <summary>
        /// The number of units that can execute the Multiplcation functions MULT
        /// </summary>
        private int _numberOfMultiplyUnits = 2;
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
        /// <summary>
        /// The clock cycle that we are at 
        /// </summary>
        private int _clockCycle = 0;
        public int ClockCycle
        {
            get { return _clockCycle; }
            set { SetProperty(ref _clockCycle, value); }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Fills the function units table for with the sent data
        /// </summary>
        public DelegateCommand FillFunctionalUnitsCommand { get; private set; }
        /// <summary>
        /// The command to start scoreboading algorithm
        /// </summary>
        public DelegateCommand ScoreBoardCommand { get; private set; }
        #endregion

        #region Command Metods
        /// <summary>
        /// The method that is called when the command is executed
        /// </summary>
        private void FillFunctionalUnitsCommandMethod()
        {
            //Create the list
            FunctionalUnits = new List<FunctionalUnitWithStatusModel>();

            //A counter for the id of the functional units
            int conter = 1;

            //Fill the list with the specificed value that the user sent for each function
            for (int i = 1; i <= NumberOfLoadUnits; i++, conter++)
                FunctionalUnits.Add(new FunctionalUnitWithStatusModel(conter, "Load " + i, FunctionsTypes.LD));
            for (int i = 1; i <= NumberOfMultiplyUnits; i++, conter++)
                FunctionalUnits.Add(new FunctionalUnitWithStatusModel(conter, "Mult " + i, FunctionsTypes.MULT));
            for (int i = 1; i <= NumberOfAddUnits; i++, conter++)
                FunctionalUnits.Add(new FunctionalUnitWithStatusModel(conter, "Add " + i, FunctionsTypes.ADD));
            for (int i = 1; i <= NumberOfDivideUnits; i++, conter++)
                FunctionalUnits.Add(new FunctionalUnitWithStatusModel(conter, "Divide " + i, FunctionsTypes.DIV));

            RaisePropertyChanged(nameof(FunctionalUnits));
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
            //Call on the start to fill the instruction list
            FillInstructionList(instructionList);
            FillRegisterList();
            FunctionClockCycle = functionClockCycle;
            //Create the commands
            FillFunctionalUnitsCommand = new DelegateCommand(FillFunctionalUnitsCommandMethod);
            ScoreBoardCommand = new DelegateCommand(ScoreBoard);
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
        /// <summary>
        /// Fills the register list
        /// </summary>
        private void FillRegisterList()
        {
            //Create and fill the list
            Registers = new List<RegisterResultModel>();

            var registerNames = Enum.GetValues(typeof(RegisteriesAndMemory));

            foreach (var item in registerNames)
            {
                var stringValue = Enum.GetName(typeof(RegisteriesAndMemory), item);
                //If it is a registery spot add it to target
                if ((int)item < 31)
                    Registers.Add(new RegisterResultModel(stringValue));
            }
        }
        #endregion

        #region ScroeBorading Algorithm
        private void ScoreBoard()
        {
            ClockCycle++;

            var instructionLenght = Instructions.Count;
            for (int i = 0; i < instructionLenght; i++)
            {
                var instruction = Instructions[i];
                if (instruction.IssueCycle == null)
                {
                    //Issue the instruction if all hazerds are gone
                    IssueIfApproved(instruction);
                    
                    //Restart from top when we issue a new instruction
                    i = -1;

                    break;
                }
                else if (instruction.ReadCycle == null)
                    //Check if we can read the instruction
                    ReadIfApproved(instruction);
                else if (instruction.WriteBackCycle != null)
                {
                    if (CheckIfDone())
                        return;
                }
                else
                    //Execute the instruction
                    ExecuteInstrution(instruction);
            }
        }

        #region Which is the right functional unit
        /// <summary>
        /// Checks if the function that is sent can work on the mult functional unit
        /// </summary>
        /// <param name="name">The name of the function</param>
        /// <returns></returns>
        private bool CanUseMultUnit(string function) => function == FunctionsTypes.MULT.ToString();
        /// <summary>
        /// Checks if the functions can work on the add functional unit
        /// </summary>
        /// <param name="function">The name of the function</param>
        /// <returns></returns>
        private bool CanUseAddUnit(string function) => (function == FunctionsTypes.ADD.ToString() || function == FunctionsTypes.SUB.ToString());
        /// <summary>
        /// Checks if the functions can work on the integer functional unit
        /// </summary>
        /// <param name="function">The name of the function</param>
        /// <returns></returns>
        private bool CanUseIntegerUnit(string function) => (function == FunctionsTypes.LD.ToString() || function == FunctionsTypes.SD.ToString());
        /// <summary>
        /// Checks if the functions can work on the divide functional unit
        /// </summary>
        /// <param name="function">The name of the function</param>
        /// <returns></returns>
        private bool CanUseDivideUnit(string function) => (function == FunctionsTypes.DIV.ToString());
        #endregion

        /// <summary>
        /// Checks if all the hazerds are gone and sets the instruction into issue
        /// </summary>
        /// <param name="instruction">The instruction that we want to issue</param>
        /// <returns></returns>
        private bool IssueIfApproved(InstructionWithStatusModel instruction)
        {
            foreach (var unit in FunctionalUnits)
            {
                if (unit.Function.ToString() == instruction.Name)
                {
                    //Issuee only when functional unit is not busy structural hazard and...
                    if (!unit.IsBusy)
                    {

                        //The target register to write in is free WAW hazard
                        var registerToTarget = Registers.SingleOrDefault(reg => reg.Name == instruction.TargetRegistery);
                        if (registerToTarget.Operation == null)
                        {
                            instruction.IssueCycle = ClockCycle;

                            //Set the register as busy
                            registerToTarget.Operation = unit.Name;

                            //Set it to busy
                            unit.IsBusy = true;
                            //Assign it the operation values
                            unit.WorkingInstructionID = instruction.ID;
                            unit.Operation = instruction.Name;
                            unit.SourceRegistery01 = instruction.SourceRegistery01;
                            unit.SourceRegistery02 = instruction.SourceRegistery02;
                            unit.TargetRegistery = instruction.TargetRegistery;
                            var operationOnSource01 = Registers.SingleOrDefault(reg => reg.Name == instruction.SourceRegistery01);
                            var operationOnSource02 = Registers.SingleOrDefault(reg => reg.Name == instruction.SourceRegistery02);
                            if (operationOnSource01 is null || operationOnSource01.Operation == null)
                                unit.IsSource01Ready = "Yes";
                            else
                            {
                                unit.IsSource01Ready = "No";
                                unit.WaitingOperationForSource01 = operationOnSource01.Operation;
                            }
                            if (operationOnSource02 is null)
                                unit.IsSource02Ready = "";
                            else if (operationOnSource02.Operation == null)
                                unit.IsSource02Ready = "Yes";
                            else
                            {
                                unit.IsSource02Ready = "No";
                                unit.WaitingOperationForSource02 = operationOnSource02.Operation;
                            }

                            return true;
                        }
                    }
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// Reads the instruction data when all conditions are meet
        /// </summary>
        /// <param name="instruction">The instruction that we want to read</param>
        /// <returns></returns>
        private bool ReadIfApproved(InstructionWithStatusModel instruction)
        {
            //Get the functional unit that the instruction is working on
            var functionalUnitForTheInstruction = FunctionalUnits.SingleOrDefault(unit => unit.WorkingInstructionID == instruction.ID);

            //If both Sources are ready to be read
            if (functionalUnitForTheInstruction.IsSource01Ready == "Yes" &&
                (functionalUnitForTheInstruction.IsSource02Ready == "Yes" || string.IsNullOrEmpty(functionalUnitForTheInstruction.IsSource02Ready)))
            {
                //Read the values and set the clock cycle
                instruction.ReadCycle = ClockCycle;

                //Set the amount of the clokc cycles
                functionalUnitForTheInstruction.Time = FunctionClockCycle.SingleOrDefault(function => function.Key.ToString() == functionalUnitForTheInstruction.Operation).Value;
                return true;
            }
            return false;
        }
        /// <summary>
        /// executes the instruction each clock cycle until end
        /// </summary>
        private void ExecuteInstrution(InstructionWithStatusModel instruction)
        {
            //Get the unit that the instruction is working on
            var unit = FunctionalUnits.SingleOrDefault(item => item.WorkingInstructionID == instruction.ID);

            if (--unit.Time == 0)
                //Set the clock cycle that is completed executing on
                instruction.ExecuteCompletedCycle = ClockCycle;
            else if (unit.Time == -1)
            {
                //Set the write back time
                instruction.WriteBackCycle = ClockCycle;
                //Clear the functional unit from its data to reset it
                ClearUnitFunction(unit);
            }
        }
        /// <summary>
        /// Clears up the unit after the instruction writes back
        /// </summary>
        /// <param name="unit"></param>
        private void ClearUnitFunction(FunctionalUnitWithStatusModel unit)
        {
            unit.Time = null;
            unit.Operation = null;
            unit.IsBusy = false;
            unit.WorkingInstructionID = 0;
            unit.Operation = null;
            unit.SourceRegistery01 = null ;
            unit.SourceRegistery02 = null;
            unit.TargetRegistery = null;
            unit.WaitingOperationForSource01 = null;
            unit.WaitingOperationForSource02 = null;
            unit.IsSource01Ready = null;
            unit.IsSource02Ready = null;
            //Free the register up
            Registers.SingleOrDefault(reg => reg.Operation == unit.Name).Operation = null;
            RecheckIfRegistersAreFree();
        }
        /// <summary>
        /// Checks if the register are free so we can start reading 
        /// loops on them each time a result is writen back
        /// </summary>
        private void RecheckIfRegistersAreFree()
        {
            foreach (var unit in FunctionalUnits)
            {
                if (unit.IsSource01Ready == "No")
                {
                    if (Registers.SingleOrDefault(reg => reg.Name == unit.SourceRegistery01).Operation == null)
                    {
                        unit.IsSource01Ready = "Yes";
                        unit.WaitingOperationForSource01 = null;
                    }
                }
                if (unit.IsSource02Ready == "No")
                {
                    if (Registers.SingleOrDefault(reg => reg.Name == unit.SourceRegistery02).Operation == null)
                    {
                        unit.IsSource02Ready = "Yes";
                        unit.WaitingOperationForSource01 = null;
                    }
                }
            }
        }
        /// <summary>
        /// Checks if all instructions has been executed and wrote there values back
        /// </summary>
        /// <returns></returns>
        private bool CheckIfDone()
        {
            foreach (var instruction in Instructions)
            {
                if (instruction.WriteBackCycle == null)
                    return false;
            }
            return true;
        }

        #endregion
    }
}
