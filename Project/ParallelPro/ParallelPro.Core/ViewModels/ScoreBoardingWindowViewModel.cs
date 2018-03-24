
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using ThishreenUniversity.ParallelPro.Enums;
using Tishreen.ParallelPro.Core.Models;
namespace Tishreen.ParallelPro.Core
{
    /// <summary>
    /// The view model and the logic for the scoreboarding alog
    /// </summary>
    public class ScoreBoardingWindowViewModel : BaseViewModel
    {
        #region Private Members
        /// <summary>
        /// The name of this class algorithm
        /// </summary>
        private string algoName = "ScoreBoarding";
        /// <summary>
        /// Holds the result for the student at each exam
        /// Only created if in exam mode
        /// </summary>
        private ExamResultModel mExamResult;
        #endregion
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
        #region Flags
        /// <summary>
        /// A flag to till if we can go to the next clock cycle
        /// </summary>
        private bool _canGoNextCycle = true;
        public bool CanGoNextCycle
        {
            get { return _canGoNextCycle; }
            set { SetProperty(ref _canGoNextCycle, value); }
        }
        #endregion

        #endregion

        #region Commands
        /// <summary>
        /// Fills the function units table for with the sent data
        /// </summary>
        public DelegateCommand FillFunctionalUnitsCommand { get; private set; }
        /// <summary>
        /// The command to start scoreboading algorithm for one clock cycle
        /// </summary>
        public DelegateCommand ScoreBoardOneCycleCommand { get; private set; }
        /// <summary>
        /// The command to scoreboard till the end of the algorithm
        /// </summary>
        public DelegateCommand ScoreBoardTillEndCommand { get; private set; }
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

            //If we are in exam mode
            if (IoC.Appliation.IsExamMode)
                //Set the computer solution aside for compartion
                SetExamPropertiesAndStoreSolution();

            RaisePropertyChanged(nameof(FunctionalUnits));
        }
        #endregion

        #region Constructer
        /// <summary>
        /// Default Constructer 
        /// </summary>
        /// <param name="instructionList">The list of instruction that the usere enterted</param>
        /// <param name="functionClockCycle">The clock cycles for each function unit</param>
        public ScoreBoardingWindowViewModel(List<object> instructionList, List<KeyValuePair<FunctionsTypes, int>> functionClockCycle)
        {
            //If exam mode save the instructions that the student entered
            if(IoC.Appliation.IsExamMode)
            {
                //Set the start of exam data
                mExamResult = new ExamResultModel
                {
                    AlgorithmName = algoName,
                };
                //Save the instructions
                instructionList.ForEach(item => mExamResult.StudentEnteredInstruction.Add((InstructionModel)item));
            }
            //Call on the start to fill the instruction list
            FillInstructionList(instructionList);
            FillRegisterList();
            FunctionClockCycle = functionClockCycle;
            //Create the commands
            FillFunctionalUnitsCommand = new DelegateCommand(FillFunctionalUnitsCommandMethod);
            ScoreBoardOneCycleCommand = new DelegateCommand(StartScoreBoardOneStep);
            ScoreBoardTillEndCommand = new DelegateCommand(StartScoreBoardTillTheEnd);
            CorrectExamAndSetMarkCommand = new DelegateCommand(CorrectExamAndSetMarkCommandMethod);
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
        /// <summary>
        /// ScoreBoards the instructions to a specified clock cycle
        /// </summary>
        private void StartScoreBoardTillClockCycle(List<InstructionWithStatusModel> instructions, List<FunctionalUnitWithStatusModel> functionalUnits, List<RegisterResultModel> registers, int toClockCylce) { while (!ScoreBoard(instructions, functionalUnits, registers, toClockCylce)) ; }
        /// <summary>
        /// ScoreBoard the instruction until it ends
        /// </summary>
        private void StartScoreBoardTillTheEnd() { while (!ScoreBoard(Instructions, FunctionalUnits, Registers)) ; }
        /// <summary>
        /// only one clock cycle for the algorithm
        /// </summary>
        private void StartScoreBoardOneStep() => ScoreBoard(Instructions, FunctionalUnits, Registers);
        /// <summary>
        /// The score board algorithm 
        /// </summary>
        private bool ScoreBoard(List<InstructionWithStatusModel> instructions, List<FunctionalUnitWithStatusModel> functionalUnits, List<RegisterResultModel> registers, int toClockCycle = -1)
        {
            //If we arreived into the wanted clock cylce end operation
            if (ClockCycle == toClockCycle)
                return true;
            ClockCycle++;

            var instructionLenght = instructions.Count;
            for (int i = 0; i < instructionLenght; i++)
            {
                var instruction = instructions[i];
                if (instruction.IssueCycle == null)
                {
                    //Issue the instruction if all hazerds are gone
                    IssueIfApproved(instruction, functionalUnits, registers);

                    //Restart from top when we issue a new instruction
                    i = -1;

                    break;
                }
                else if (instruction.ReadCycle == null)
                    //Check if we can read the instruction
                    ReadIfApproved(instruction, functionalUnits);
                else if (instruction.WriteBackCycle != null)
                {
                    if (CheckIfDone(instructions))
                    {
                        //Get the cycle back to the right value
                        ClockCycle--;
                        CanGoNextCycle = false;
                        return true;
                    }
                }
                else
                    //Execute the instruction
                    ExecuteInstrution(instruction, functionalUnits, registers);
            }

            return false;
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
        private bool IssueIfApproved(InstructionWithStatusModel instruction, List<FunctionalUnitWithStatusModel> functionalUnits, List<RegisterResultModel> registers)
        {
            foreach (var unit in functionalUnits)
            {
                if (unit.Function.ToString() == instruction.Name)
                {
                    //Issuee only when functional unit is not busy structural hazard and...
                    if (!unit.IsBusy)
                    {
                        if (!unit.JustFreedUp)
                        {
                            //The target register to write in is free WAW hazard
                            var registerToTarget = registers.SingleOrDefault(reg => reg.Name == instruction.TargetRegistery);
                            if (!registerToTarget.IsBusy)
                            {
                                instruction.IssueCycle = ClockCycle;


                                //Set it to busy
                                unit.IsBusy = true;
                                //Assign it the operation values
                                unit.WorkingInstructionID = instruction.ID;
                                unit.Operation = instruction.Name;
                                unit.SourceRegistery01 = instruction.SourceRegistery01;
                                unit.SourceRegistery02 = instruction.SourceRegistery02;
                                unit.TargetRegistery = instruction.TargetRegistery;
                                var operationOnSource01 = registers.SingleOrDefault(reg => reg.Name == instruction.SourceRegistery01);
                                var operationOnSource02 = registers.SingleOrDefault(reg => reg.Name == instruction.SourceRegistery02);
                                if (operationOnSource01 is null || operationOnSource01.Operation == null)
                                    unit.IsSource01Ready = true;
                                else
                                {
                                    unit.IsSource01Ready = false;
                                    unit.WaitingOperationForSource01 = operationOnSource01.Operation;
                                }
                                if (operationOnSource02 is null)
                                    unit.IsSource02Ready = false;
                                else if (operationOnSource02.Operation == null)
                                    unit.IsSource02Ready = true;
                                else
                                {
                                    unit.IsSource02Ready = false;
                                    unit.WaitingOperationForSource02 = operationOnSource02.Operation;
                                }

                                //Set the register as busy
                                registerToTarget.Operation = unit.Operation == "LD" ? string.Format("F[{0}]", unit.SourceRegistery01) : string.Format("F[{0}]", unit.Name);
                                registerToTarget.IsBusy = true;
                                registerToTarget.WorkingInstructionID = unit.WorkingInstructionID;

                                return true;
                            }
                        }
                        unit.JustFreedUp = false;
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
        private bool ReadIfApproved(InstructionWithStatusModel instruction, List<FunctionalUnitWithStatusModel> functionalUnits)
        {
            //Get the functional unit that the instruction is working on
            var unit = functionalUnits.SingleOrDefault(item => item.WorkingInstructionID == instruction.ID);

            //If both Sources are ready to be read
            if (unit.IsSource01Ready  &&
                (unit.IsSource02Ready || string.IsNullOrEmpty(unit.SourceRegistery02)))
            {

                //Read the values and set the clock cycle
                instruction.ReadCycle = ClockCycle;

                //Set the amount of the clokc cycles
                unit.Time = FunctionClockCycle.SingleOrDefault(function => function.Key.ToString() == unit.Operation).Value;
                return true;
            }
            return false;
        }
        /// <summary>
        /// executes the instruction each clock cycle until end
        /// </summary>
        private void ExecuteInstrution(InstructionWithStatusModel instruction, List<FunctionalUnitWithStatusModel> functionalUnits, List<RegisterResultModel> registers)
        {
            //Get the unit that the instruction is working on
            var unit = functionalUnits.SingleOrDefault(item => item.WorkingInstructionID == instruction.ID);

            if (--unit.Time == 0)
                //Set the clock cycle that is completed executing on
                instruction.ExecuteCompletedCycle = ClockCycle;
            else if (unit.Time == -1)
            {
                //Set the write back time
                instruction.WriteBackCycle = ClockCycle;
                RecheckIfRegistersAreFree(functionalUnits, registers);
                //Clear the functional unit from its data to reset it
                ClearUnitFunction(unit, registers);


            }
        }
        /// <summary>
        /// Clears up the unit after the instruction writes back
        /// </summary>
        /// <param name="unit"></param>
        private void ClearUnitFunction(FunctionalUnitWithStatusModel unit, List<RegisterResultModel> registers)
        {
            //Free the register up
            registers.SingleOrDefault(reg => reg.WorkingInstructionID == unit.WorkingInstructionID).IsBusy = false;
            unit.JustFreedUp = true;
            unit.Time = null;
            unit.Operation = null;
            unit.IsBusy = false;
            unit.WorkingInstructionID = 0;
            unit.Operation = null;
            unit.SourceRegistery01 = null;
            unit.SourceRegistery02 = null;
            unit.TargetRegistery = null;
            unit.WaitingOperationForSource01 = null;
            unit.WaitingOperationForSource02 = null;
            unit.IsSource01Ready = false;
            unit.IsSource02Ready = false;
        }
        /// <summary>
        /// Checks if the register are free so we can start reading 
        /// loops on them each time a result is writen back
        /// </summary>
        private void RecheckIfRegistersAreFree(List<FunctionalUnitWithStatusModel> functionalUnits, List<RegisterResultModel> registers)
        {
            foreach (var unit in functionalUnits)
            {
                if (unit.IsSource01Ready == false)
                {
                    if (registers.SingleOrDefault(reg => reg.Name == unit.SourceRegistery01).IsBusy == false)
                    {
                        unit.IsSource01Ready = true;
                        unit.WaitingOperationForSource01 = null;
                    }
                }
                if (unit.IsSource02Ready == false)
                {
                    if (registers.SingleOrDefault(reg => reg.Name == unit.SourceRegistery02).IsBusy == false)
                    {
                        unit.IsSource02Ready = true;
                        unit.WaitingOperationForSource01 = null;
                    }
                }
            }
        }
        /// <summary>
        /// Checks if all instructions has been executed and wrote there values back
        /// </summary>
        /// <returns></returns>
        private bool CheckIfDone(List<InstructionWithStatusModel> instructions)
        {
            foreach (var instruction in instructions)
            {
                if (instruction.WriteBackCycle == null)
                    return false;
            }
            return true;
        }

        #endregion

        #region Exam mode properties and functions

        #region Properties
        /// <summary>
        /// The clock cycle the student chooses to exam to 
        /// </summary>
        private int _examClockCycle;
        public int ExamClockCycle
        {
            get { return _examClockCycle; }
            set { SetProperty(ref _examClockCycle, value); }
        }

        #region List Computer holds his solution to compare with the student solution
        /// <summary>
        /// The list that holds all the instructions that the computer solves and is compared with the user input
        /// to set the exam mark
        /// </summary>
        public List<InstructionWithStatusModel> InstructionsExamResult = new List<InstructionWithStatusModel>();
        /// <summary>
        /// The list of functional units that the computer solves and is compared with the user input
        /// to set the exam mark
        /// </summary>
        public List<FunctionalUnitWithStatusModel> FunctionalUnitsExamResult = new List<FunctionalUnitWithStatusModel>();
        /// <summary>
        /// The list that holds all the registers that the computer solves and is compared with the user input
        /// to set the exam mark
        /// </summary>
        public List<RegisterResultModel> RegistersExamResults = new List<RegisterResultModel>();

        #endregion

        #endregion

        #region Exam Commands
        /// <summary>
        /// Corrects the student answears and sets his mark
        /// </summary>
        public DelegateCommand CorrectExamAndSetMarkCommand { get; set; }
        #endregion

        #region Command methods

        /// <summary>
        /// The method for the <see cref="CorrectExamAndSetMarkCommand"/>
        /// +1  for evert correct value
        /// 0 for noncorrect
        /// </summary>
        private void CorrectExamAndSetMarkCommandMethod()
        {
            //The start mark
            float studentMark = 0;
            var fullMark = 0;
            //Correct instrucitons
            //Get the number of elements inside the list 
            int instructionCount = Instructions.Count;
            //loop throw the instructions
            for (int i = 0; i < instructionCount; i++)
                studentMark += InstructionsExamResult[i].CompareInstructions(Instructions[i]);
            //As every instruction has 4 fileds student can enter
            fullMark += instructionCount * 4;

            //Correct functional units
            //Get the number of elements inside the list 
            int functionalUnitsCount = FunctionalUnits.Count;
            //loop throw the functional units
            for (int i = 0; i < functionalUnitsCount; i++)
                studentMark += FunctionalUnitsExamResult[i].CompareFunctionUnits(FunctionalUnits[i]);
            //Only 10 fileds student can fill in exam
            fullMark += functionalUnitsCount * 10;

            //Correct registers
            //Get the number of elements inside the list 
                int RegisterCount = Registers.Count;
            //loop throw the registers
            for (int i = 0; i < RegisterCount; i++)
                studentMark += RegistersExamResults[i].CompareRegisters(Registers[i]);
            //For every right register will hold a mark
            fullMark += RegisterCount;

            //Attach the marks with the exam report 
            mExamResult.StudentMark = studentMark;
            mExamResult.FullMark = fullMark;
            mExamResult.StudentMarkPercentage =(100 * (int)studentMark) / fullMark;

            //Attach the exam result to the student 
            StudentExamInformationAndMarks.Results.Add(mExamResult);


            IoC.UI.ShowWinodw(ApplicationPages.ResultWindow);
        }
        #endregion

        #region Functions
        /// <summary>
        /// Sets the properties of the exam information for the student
        /// and solves to the clock cycle that is provided by the student 
        /// so it compares it with the student solution
        /// </summary>
        private void SetExamPropertiesAndStoreSolution()
        {
            //Set the exam clock cycle
            mExamResult.ChoosenClockCycle = ExamClockCycle;
            //Set the startup values
            Instructions.ForEach(item => InstructionsExamResult.Add(new InstructionWithStatusModel(item)));
            FunctionalUnits.ForEach(item => FunctionalUnitsExamResult.Add(new FunctionalUnitWithStatusModel(item.ID, item.Name, item.Function)));
            Registers.ForEach(item => RegistersExamResults.Add(new RegisterResultModel(item.Name, item.Operation)));
            //Scrore board the instruction to the wanted clockCycle
            StartScoreBoardTillClockCycle(InstructionsExamResult, FunctionalUnitsExamResult, RegistersExamResults, ExamClockCycle);
        }
        #endregion

        #endregion
    }
}
