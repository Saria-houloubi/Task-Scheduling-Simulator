using Microsoft.Win32;
using PPS.UI.ScoreboardAndTomasolu.Enums;
using PPS.UI.ScoreboardAndTomasolu.Models;
using PPS.UI.Shared.Enums;
using PPS.UI.Shared.Models;
using PPS.UI.Shared.ViewModels.Base;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace PPS.UI.ScoreboardAndTomasolu.ViewModels
{
    public class ScoreBoardAndTomasoluWindwoViewModel : BaseViewModel
    {
        #region Properties
        /// <summary>
        /// A flag to check if we are applying scoreboard or tomasolu
        /// </summary>
        private bool _IsTomasoluSelected = true;

        public bool IsTomasoluSelected
        {
            get { return _IsTomasoluSelected; }
            set
            {
                SetProperty(ref _IsTomasoluSelected, value);
                //Reset the values
                SetProcessorOptionsCommand_Execute();
            }
        }

        /// <summary>
        /// If the user can push to the next cycle
        /// </summary>
        private bool _CanGoNextCycle = true;

        public bool CanGoNextCycle
        {
            get { return _CanGoNextCycle; }
            set { SetProperty(ref _CanGoNextCycle, value); }
        }

        /// <summary>
        /// The current clock cycle that the proccsing is in
        /// </summary>
        private int _CurrentClockCycle;

        public int CurrentClockCycle
        {
            get { return _CurrentClockCycle; }
            set { SetProperty(ref _CurrentClockCycle, value); }
        }

        /// <summary>
        /// The functions list for the instructions
        /// </summary>
        public List<string> Functions { get; set; }

        private BasicFunctions _SelectedFunction;

        public BasicFunctions SelectedFunction
        {
            get { return _SelectedFunction; }
            set
            {
                SetProperty(ref _SelectedFunction, value);
                FillTargetAndSoruce();
            }
        }
        /// <summary>
        /// The target registeries or memory spots for the instruction
        /// </summary>
        private ObservableCollection<string> _Targets;

        public ObservableCollection<string> Targets
        {
            get { return _Targets; }
            set { SetProperty(ref _Targets, value); }
        }
        public string SelectedTarget { get; set; }
        /// <summary>
        /// The source from where the instruction is going to get the data from
        /// </summary>
        private ObservableCollection<string> _Sources;

        public ObservableCollection<string> Sources
        {
            get { return _Sources; }
            set { SetProperty(ref _Sources, value); }
        }
        public string SelectedSource1 { get; set; }
        public string SelectedSource2 { get; set; }
        /// <summary>
        /// A flag to check if the user can chose the source2
        /// LD and ST disable this opetion
        /// </summary>
        private bool _CanChoseSource2;

        public bool CanChoseSource2
        {
            get { return _CanChoseSource2; }
            set { SetProperty(ref _CanChoseSource2, value); }
        }

        /// <summary>
        /// The instruction list the user entered
        /// </summary>
        private ObservableCollection<BasicInstructionModel> _Instructions;

        public ObservableCollection<BasicInstructionModel> Instructions
        {
            get { return _Instructions; }
            set { SetProperty(ref _Instructions, value); }
        }
        private BasicInstructionModel _SelectedInstruction;

        public BasicInstructionModel SelectedInstruction
        {
            get { return _SelectedInstruction; }
            set { SetProperty(ref _SelectedInstruction, value); }
        }
        /// <summary>
        /// The count of the functional units 
        /// </summary>
        public Dictionary<FunctionalUnits, int> FunctionalUnitsCount { get; set; }
        /// <summary>
        /// The clock cycle count for the basic instruction to execute
        /// </summary>
        public Dictionary<BasicFunctions, int> InstructionClockCycleCount { get; set; }
        /// <summary>
        /// The functional units for the algorithm
        /// </summary>
        private ObservableCollection<FunctionalUnitModel> _FunctionalUnitsList;

        public ObservableCollection<FunctionalUnitModel> FunctionalUnitsList
        {
            get { return _FunctionalUnitsList; }
            set { SetProperty(ref _FunctionalUnitsList, value); }
        }
        /// <summary>
        /// The register list
        /// </summary>
        private ObservableCollection<RegisterModel> _Registers;

        public ObservableCollection<RegisterModel> Registers
        {
            get { return _Registers; }
            set { SetProperty(ref _Registers, value); }
        }
        /// <summary>
        /// The counter for the registers on the tomasolu algorithm
        /// where if a conflect happen it changes the target
        /// </summary>
        public int TomasoluRegisterCounter { get; set; }
        #endregion

        #region Commands
        /// <summary>
        /// The command to add an instruction
        /// </summary>
        public DelegateCommand AddInstructionCommand { get; set; }
        /// <summary>
        /// Clears the instruction list
        /// </summary>
        public DelegateCommand ClearInstructionsCommand { get; set; }
        /// <summary>
        /// Delete the selected instruction
        /// </summary>
        public DelegateCommand DeleteInstructionCommand { get; set; }
        /// <summary>
        /// Edits the selected instruction
        /// </summary>
        public DelegateCommand EditInstructionCommand { get; set; }
        /// <summary>
        /// The commad to set the functional units and the clcok cycles for the diffrent instruction types
        /// </summary>
        public DelegateCommand SetProcessorOptionsCommand { get; set; }
        /// <summary>
        /// The command to start scoreboading algorithm for one clock cycle
        /// </summary>
        public DelegateCommand MoveOneCycleCommand { get; set; }
        /// <summary>
        /// The command to scoreboard till the end of the algorithm
        /// </summary>
        public DelegateCommand MoveTillEndCommand { get; set; }
        /// <summary>
        /// Adds code from a text file
        /// </summary>
        public DelegateCommand AddCodeFromTxtCommand { get; set; }
        #endregion

        #region Command Methods

        /// <summary>
        /// The function to check if the <see cref="AddCodeFromTxtCommand"/> can get executed
        /// </summary>
        /// <returns></returns>
        public bool AddCodeFromTxtCommand_CanExecute()
        {
            return true;
        }

        /// <summary>
        /// The function that will be called once the <see cref="AddCodeFromTxtCommand"/> is invoked
        /// </summary>
        public void AddCodeFromTxtCommand_Execute()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.CheckPathExists)
                {
                    //The code below need alot of working on i had to cut some corners so I can finish on time 
                    #region Read code from text 

                    //Flags to go throw the instructions
                    bool readFunction = false;
                    bool readTarget = false;
                    bool readSoruce1 = false;
                    bool readSoruce2 = false;
                    //The parameters for the instruction
                    string function = "";
                    string target = "";
                    string source1 = "";
                    string source2 = "";
                    //Flag to know an error happend
                    bool counterdError = false;
                    //Message for the error
                    string errorMessage = "";

                    //read the file 
                    var fileBytes = File.ReadAllBytes(openFileDialog.FileName);
                    //Check if the file is empty
                    if (fileBytes.Length == 0)
                    {
                        ShowErrorMessage("File is Empty!!!");
                        return;
                    }
                    //Create the list
                    Instructions = new ObservableCollection<BasicInstructionModel>();
                    //The instruction that we are reading
                    var readingInstruction = new BasicInstructionModel();
                    //Loop throw the words
                    //matching the with the template
                    foreach (var charByte in fileBytes)
                    {
                        if (!readFunction)
                        {
                            if (charByte == ' ')
                            {
                                //Keep reading until any character
                                if (string.IsNullOrEmpty(function))
                                {
                                    continue;
                                }

                                if (Enum.TryParse(function, out BasicFunctions func))
                                {
                                    readingInstruction.Function = func;
                                    readFunction = true;
                                }
                                else
                                {
                                    counterdError = true;
                                    errorMessage = $"Wrong function name ({function})";
                                    break;
                                }

                            }
                            //Get the upper case
                            var upperCaseChar = Char.ToUpper((char)charByte);

                            function = string.Concat(function, upperCaseChar);
                        }
                        else if (!readTarget)
                        {
                            if (charByte == ' ')
                            {
                                //Keep reading until any character
                                if (string.IsNullOrEmpty(target))
                                {
                                    continue;
                                }

                                if (Enum.TryParse(target, out Registeries regOrMem))
                                {
                                    if (readingInstruction.Function == BasicFunctions.SD)
                                    {
                                        counterdError = true;
                                        errorMessage = $"Wrong target for function ({readingInstruction.Function})";
                                    }
                                    else
                                    {
                                        readingInstruction.Target = regOrMem.ToString();
                                        readTarget = true;
                                    }

                                }
                                else if (Enum.TryParse(target, out Memmories mem))
                                {
                                    if (readingInstruction.Function == BasicFunctions.LD)
                                    {
                                        counterdError = true;
                                        errorMessage = $"Wrong target for function ({readingInstruction.Function})";
                                    }
                                    else
                                    {
                                        readingInstruction.Target = mem.ToString();
                                        readTarget = true;
                                    }

                                }
                                else
                                {
                                    counterdError = true;
                                    errorMessage = $"Wrong register or memory name ({target})";
                                    break;
                                }

                            }
                            //Get the upper case
                            var upperCaseChar = Char.ToUpper((char)charByte);

                            target = string.Concat(target, upperCaseChar);
                        }
                        else if (!readSoruce1)
                        {
                            if (charByte == ',' || charByte == '\r' || charByte == '\t' || charByte == ' ')
                            {
                                if (Enum.TryParse(source1, out Registeries regOrMem))
                                {

                                    if (readingInstruction.Function == BasicFunctions.LD)
                                    {
                                        counterdError = true;
                                        errorMessage = $"Wrong source for function ({readingInstruction.Function})";

                                    }
                                    else
                                    {
                                        readingInstruction.Source1 = regOrMem.ToString();
                                        readSoruce1 = true;
                                        if (readingInstruction.Function == BasicFunctions.LD || readingInstruction.Function == BasicFunctions.SD)
                                        {
                                            Instructions.Add(readingInstruction);
                                            readingInstruction = new BasicInstructionModel();
                                            //Flags to go throw the instructions
                                            readFunction = false;
                                            readTarget = false;
                                            readSoruce1 = false;
                                            readSoruce2 = false;
                                            //The parameters for the instruction
                                            function = "";
                                            target = "";
                                            source1 = "";
                                            source2 = "";
                                            continue;
                                        }
                                    }

                                }
                                else if (Enum.TryParse(source1, out Memmories mem))
                                {

                                    if (readingInstruction.Function == BasicFunctions.SD)
                                    {
                                        counterdError = true;
                                        errorMessage = $"Wrong source for function ({readingInstruction.Function})";

                                    }
                                    else
                                    {
                                        readingInstruction.Source1 = mem.ToString();
                                        readSoruce1 = true;
                                        if (readingInstruction.Function == BasicFunctions.LD || readingInstruction.Function == BasicFunctions.SD)
                                        {
                                            Instructions.Add(readingInstruction);
                                            readingInstruction = new BasicInstructionModel();
                                            //Flags to go throw the instructions
                                            readFunction = false;
                                            readTarget = false;
                                            readSoruce1 = false;
                                            readSoruce2 = false;
                                            //The parameters for the instruction
                                            function = "";
                                            target = "";
                                            source1 = "";
                                            source2 = "";
                                            continue;
                                        }
                                    }

                                }
                                else
                                {
                                    counterdError = true;
                                    errorMessage = $"Wrong register or memory name ({source1})";
                                    break;
                                }

                            }
                            //Get the upper case
                            var upperCaseChar = Char.ToUpper((char)charByte);

                            source1 = string.Concat(source1, upperCaseChar);
                        }
                        else if ((readingInstruction.Function != BasicFunctions.LD || readingInstruction.Function != BasicFunctions.SD) && !readSoruce2)
                        {
                            if (charByte == '\r' || charByte == '\t' || charByte == ' ')
                            {
                                //Keep reading until any character
                                if (string.IsNullOrEmpty(source2))
                                {
                                    continue;
                                }

                                if (Enum.TryParse(source2, out Registeries regOrMem))
                                {
                                    readingInstruction.Source2 = regOrMem.ToString();
                                    readSoruce2 = true;
                                    Instructions.Add(readingInstruction);
                                    readingInstruction = new BasicInstructionModel();
                                    //Reset for next read
                                    readFunction = false;
                                    readTarget = false;
                                    readSoruce1 = false;
                                    readSoruce2 = false;
                                    function = "";
                                    target = "";
                                    source1 = "";
                                    source2 = "";
                                }
                                else if (Enum.TryParse(source2, out Memmories mem))
                                {
                                    readingInstruction.Source2 = mem.ToString();
                                    readSoruce2 = true;
                                    Instructions.Add(readingInstruction);
                                    readingInstruction = new BasicInstructionModel();
                                    //Reset for next read
                                    readFunction = false;
                                    readTarget = false;
                                    readSoruce1 = false;
                                    readSoruce2 = false;
                                    function = "";
                                    target = "";
                                    source1 = "";
                                    source2 = "";
                                }
                                else
                                {
                                    counterdError = true;
                                    errorMessage = $"Wrong register or memory name ({source2})";
                                    break;
                                }

                            }
                            //Get the upper case
                            var upperCaseChar = Char.ToUpper((char)charByte);

                            source2 = string.Concat(source2, upperCaseChar);
                        }

                    }

                    if (counterdError)
                    {
                        ShowErrorMessage(errorMessage);
                        Instructions = new ObservableCollection<BasicInstructionModel>();
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// The function to check if the <see cref="MoveOneCycleCommand"/> can get executed
        /// </summary>
        /// <returns></returns>
        public bool MoveOneCycleCommand_CanExecute()
        {
            return CanGoNextCycle;
        }

        /// <summary>
        /// The function that will be called once the <see cref="MoveOneCycleCommand"/> is invoked
        /// </summary>
        public void MoveOneCycleCommand_Execute()
        {
            ScoreBoard();
        }
        /// <summary>
        /// The function to check if the <see cref="MoveTillEndCommand"/> can get executed
        /// </summary>
        /// <returns></returns>
        public bool MoveTillEndCommand_CanExecute()
        {
            return CanGoNextCycle;
        }

        /// <summary>
        /// The function that will be called once the <see cref="MoveTillEndCommand"/> is invoked
        /// </summary>
        public void MoveTillEndCommand_Execute()
        {
            while (!ScoreBoard())
            {
                ;
            }
        }

        /// <summary>
        /// The function to check if the <see cref="SetProcessorOptionsCommand"/> can get executed
        /// </summary>
        /// <returns></returns>
        public bool SetProcessorOptionsCommand_CanExecute()
        {
            return true;
        }

        /// <summary>
        /// The function that will be called once the <see cref="SetProcessorOptionsCommand"/> is invoked
        /// </summary>
        public void SetProcessorOptionsCommand_Execute()
        {

            //Create the list
            foreach (var item in Instructions)
            {
                item.ClearCycles();
            }
            FunctionalUnitsList = new ObservableCollection<FunctionalUnitModel>();
            CurrentClockCycle = 0;
            CanGoNextCycle = Instructions.Any();
            InstructionClockCycleCount[BasicFunctions.SD] = InstructionClockCycleCount[BasicFunctions.LD];
            InstructionClockCycleCount[BasicFunctions.SUB] = InstructionClockCycleCount[BasicFunctions.ADD];
            //A counter for the id of the functional units
            int conter = 1;

            //Fill the list with the specificed value that the user sent for each function
            for (int i = 1; i <= FunctionalUnitsCount[Enums.FunctionalUnits.Load]; i++, conter++)
            {
                FunctionalUnitsList.Add(new FunctionalUnitModel(conter, "Load " + i, new Dictionary<BasicFunctions, bool> { { BasicFunctions.LD, true }, { BasicFunctions.SD, true } }));
            }
            conter = 1;
            for (int i = 1; i <= FunctionalUnitsCount[Enums.FunctionalUnits.Multiply]; i++, conter++)
            {
                FunctionalUnitsList.Add(new FunctionalUnitModel(conter, "Mult " + i, new Dictionary<BasicFunctions, bool> { { BasicFunctions.MULT, true } }));
            }
            conter = 1;
            for (int i = 1; i <= FunctionalUnitsCount[Enums.FunctionalUnits.Add]; i++, conter++)
            {
                FunctionalUnitsList.Add(new FunctionalUnitModel(conter, "Add " + i, new Dictionary<BasicFunctions, bool> { { BasicFunctions.SUB, true }, { BasicFunctions.ADD, true } }));
            }
            conter = 1;
            for (int i = 1; i <= FunctionalUnitsCount[Enums.FunctionalUnits.Divide]; i++, conter++)
            {
                FunctionalUnitsList.Add(new FunctionalUnitModel(conter, "Divide " + i, new Dictionary<BasicFunctions, bool> { { BasicFunctions.DIV, true } }));
            }
            FillFunctions();
            FillRegisterList();
        }

        /// <summary>
        /// The function to check if the <see cref="EditInstructionCommand"/> can get executed
        /// </summary>
        /// <returns></returns>
        public bool EditInstructionCommand_CanExecute()
        {
            return SelectedInstruction != null;
        }

        /// <summary>
        /// The function that will be called once the <see cref="EditInstructionCommand"/> is invoked
        /// </summary>
        public void EditInstructionCommand_Execute()
        {
            //Get the index of the selected instruction
            var index = Instructions.IndexOf(SelectedInstruction);
            //Remove it from the list
            Instructions.RemoveAt(index);
            //Add the new edited instruction in the sane old spot
            Instructions.Insert(index, new BasicInstructionModel
            {
                Function = SelectedFunction,
                Source1 = SelectedSource1,
                Source2 = SelectedSource2,
                Target = SelectedTarget
            });
        }

        /// <summary>
        /// The function to check if the <see cref="DeleteInstructionCommand"/> can get executed
        /// </summary>
        /// <returns></returns>
        public bool DeleteInstructionCommand_CanExecute()
        {
            return SelectedInstruction != null;
        }

        /// <summary>
        /// The function that will be called once the <see cref="DeleteInstructionCommand"/> is invoked
        /// </summary>
        public void DeleteInstructionCommand_Execute()
        {
            Instructions.Remove(SelectedInstruction);
        }

        /// <summary>
        /// The function to check if the <see cref="ClearInstructionsCommand"/> can get executed
        /// </summary>
        /// <returns></returns>
        public bool ClearInstructionsCommand_CanExecute()
        {
            return true;
        }

        /// <summary>
        /// The function that will be called once the <see cref="ClearInstructionsCommand"/> is invoked
        /// </summary>
        public void ClearInstructionsCommand_Execute()
        {
            Instructions = new ObservableCollection<BasicInstructionModel>();
        }

        /// <summary>
        /// The function to check if the <see cref="AddInstructionCommand"/> can get executed
        /// </summary>
        /// <returns></returns>
        public bool AddInstructionCommand_CanExecute()
        {
            return true;
        }

        /// <summary>
        /// The function that will be called once the <see cref="AddInstructionCommand"/> is invoked
        /// </summary>
        public void AddInstructionCommand_Execute()
        {
            Instructions.Add(new BasicInstructionModel
            {
                Function = SelectedFunction,
                Source1 = SelectedSource1,
                Source2 = SelectedSource2,
                Target = SelectedTarget
            });
        }

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public ScoreBoardAndTomasoluWindwoViewModel()
        {
            //Create commands
            AddInstructionCommand = new DelegateCommand(AddInstructionCommand_Execute, AddInstructionCommand_CanExecute);
            ClearInstructionsCommand = new DelegateCommand(ClearInstructionsCommand_Execute, ClearInstructionsCommand_CanExecute);
            EditInstructionCommand = new DelegateCommand(EditInstructionCommand_Execute, EditInstructionCommand_CanExecute).ObservesProperty(() => SelectedInstruction);
            DeleteInstructionCommand = new DelegateCommand(DeleteInstructionCommand_Execute, DeleteInstructionCommand_CanExecute).ObservesProperty(() => SelectedInstruction);
            SetProcessorOptionsCommand = new DelegateCommand(SetProcessorOptionsCommand_Execute, SetProcessorOptionsCommand_CanExecute);
            MoveOneCycleCommand = new DelegateCommand(MoveOneCycleCommand_Execute, MoveOneCycleCommand_CanExecute).ObservesProperty(() => CanGoNextCycle);
            MoveTillEndCommand = new DelegateCommand(MoveTillEndCommand_Execute, MoveTillEndCommand_CanExecute).ObservesProperty(() => CanGoNextCycle);
            AddCodeFromTxtCommand = new DelegateCommand(AddCodeFromTxtCommand_Execute, AddCodeFromTxtCommand_CanExecute);

            InitializeData();
        }
        /// <summary>
        /// Function to initialze the data for the first run
        /// </summary>
        public void InitializeData()
        {
            //Create lists
            Instructions = new ObservableCollection<BasicInstructionModel>();
            //Create the list and initialize it with the startup data
            FunctionalUnitsCount = new Dictionary<FunctionalUnits, int>
            {
                { FunctionalUnits.Add, 1},
                { FunctionalUnits.Load, 1},
                { FunctionalUnits.Divide, 2},
                { FunctionalUnits.Multiply, 1}
            };
            //Create the list and initialize it with the startup data
            //All the values are just for demo
            InstructionClockCycleCount = new Dictionary<BasicFunctions, int>
            {
                { BasicFunctions.LD, 1},
                { BasicFunctions.SD, 1},
                { BasicFunctions.SUB, 2},
                { BasicFunctions.ADD, 2},
                { BasicFunctions.MULT, 10},
                { BasicFunctions.DIV, 10}
            };

            try
            {
                FillFunctions();
                FillRegisterList();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.GetBaseException().Message);
            }
        }
        #endregion

        #region ScroeBorading Algorithm
        /// <summary>
        /// ScoreBoards the instructions to a specified clock cycle
        /// </summary>
        protected void StartScoreBoardTillClockCycle(int toClockCylce)
        {
            while (!ScoreBoard(toClockCylce))
            {
                ;
            }
        }

        /// <summary>
        /// The score board algorithm 
        /// </summary>
        protected bool ScoreBoard(int toClockCycle = -1)
        {
            //If we arreived into the wanted clock cylce end operation
            if (CurrentClockCycle == toClockCycle)
            {
                return true;
            }

            CurrentClockCycle++;

            for (int i = 0; i < Instructions.Count; i++)
            {
                var instruction = Instructions[i];
                if (instruction.Issue == null)
                {
                    //Issue the instruction if all hazerds are gone
                    IssueIfApproved(instruction);

                    //Restart from top when we issue a new instruction
                    break;
                }
                else if (instruction.Read == null)
                {
                    //Check if we can read the instruction
                    ReadIfApproved(instruction);
                }
                else if (instruction.WriteBack != null)
                {
                    //If the value was just ended up from the past clock cycle
                    if (instruction.WriteBack == CurrentClockCycle - 1)
                    {
                        //Clear the units after a write back
                        FreeUpRegisters(Registers.SingleOrDefault(item => item.InstructionReservedRegiseter.SingleOrDefault(inst => inst.Id == instruction.Id) != null), instruction.Id);
                        RecheckIfRegistersAreFree();
                        if (CheckIfDone())
                        {
                            //Free up all registers       
                            foreach (var item in Registers.Where(reg => reg.IsBusy))
                            {
                                item.IsBusy = false;
                            }
                            //Get the cycle back to the right value
                            CurrentClockCycle--;
                            CanGoNextCycle = false;
                            return true;
                        }
                    }
                }
                else
                {
                    //Execute the instruction
                    ExecuteInstrution(instruction);
                }
            }

            return false;
        }

        #region Which is the right functional unit
        /// <summary>
        /// Checks if the function that is sent can work on the mult functional unit
        /// </summary>
        /// <param name="name">The name of the function</param>
        /// <returns></returns>
        protected bool CanUseMultUnit(string function) => function == BasicFunctions.MULT.ToString();
        /// <summary>
        /// Checks if the functions can work on the add functional unit
        /// </summary>
        /// <param name="function">The name of the function</param>
        /// <returns></returns>
        protected bool CanUseAddUnit(string function) => (function == BasicFunctions.ADD.ToString() || function == BasicFunctions.SUB.ToString());
        /// <summary>
        /// Checks if the functions can work on the integer functional unit
        /// </summary>
        /// <param name="function">The name of the function</param>
        /// <returns></returns>
        protected bool CanUseIntegerUnit(string function) => (function == BasicFunctions.LD.ToString() || function == BasicFunctions.SD.ToString());
        /// <summary>
        /// Checks if the functions can work on the divide functional unit
        /// </summary>
        /// <param name="function">The name of the function</param>
        /// <returns></returns>
        protected bool CanUseDivideUnit(string function) => (function == BasicFunctions.DIV.ToString());
        #endregion

        /// <summary>
        /// Checks if all the hazerds are gone and sets the instruction into issue
        /// </summary>
        /// <param name="instruction">The instruction that we want to issue</param>
        /// <returns></returns>
        protected bool IssueIfApproved(BasicInstructionModel instruction)
        {
            foreach (var unit in FunctionalUnitsList)
            {

                if (unit.Functions.TryGetValue(instruction.Function, out bool value))
                {
                    //Issuee only when functional unit is not busy structural hazard and...
                    if (!unit.IsBusy())
                    {
                        //The target register to write in is free WAW hazard
                        RegisterModel registerToTarget;
                        if (instruction.Function == BasicFunctions.SD)
                        {
                            registerToTarget = Registers.SingleOrDefault(reg => reg.Name == instruction.Source1);
                        }
                        else
                        {
                            registerToTarget = Registers.SingleOrDefault(reg => reg.Name == instruction.Target);
                        }

                        instruction.Issue = CurrentClockCycle;

                        //Set it to busy
                        unit.Status = UnitStatus.Busy;
                        //Assign it the operation values
                        unit.WorkingInstructionID = instruction.Id;
                        unit.Operation = instruction.Function;
                        unit.Target = instruction.Target;
                        unit._Source1Local = instruction.Source1;
                        unit._Source2Local = instruction.Source2;

                        var operationOnSource01 = Registers.SingleOrDefault(reg => reg.Name == instruction.Source1);
                        var operationOnSource02 = Registers.SingleOrDefault(reg => reg.Name == instruction.Source2);
                        //The is null for the load as it loads from memory 
                        if (operationOnSource01 is null || !operationOnSource01.IsBusy)
                        {
                            unit.IsSource01Ready = true;
                            unit.Source1 = instruction.Source1;
                        }
                        else
                        {
                            unit.IsSource01Ready = false;
                            unit.WaitingOperationForSource01 = operationOnSource01.Operation;
                        }
                        if (operationOnSource02 is null)
                        {
                            unit.IsSource02Ready = false;
                            unit.Source2 = instruction.Source2;
                        }
                        else if (!operationOnSource02.IsBusy)
                        {
                            unit.IsSource02Ready = true;
                        }
                        else
                        {
                            unit.IsSource02Ready = false;
                            unit.WaitingOperationForSource02 = operationOnSource02.Operation;
                        }

                        //Set the register as busy
                        registerToTarget.Operation = unit.Operation == BasicFunctions.LD ? string.Format("F[{0}]", unit.Source1) : string.Format("F[{0}]", unit.Name);
                        registerToTarget.IsBusy = true;
                        if (registerToTarget.InstructionReservedRegiseter.Any())
                        {
                            registerToTarget.InstructionReservedRegiseter.LastOrDefault().LastIssued = false;
                        }
                        //Add it to the reserved instructions
                        registerToTarget.InstructionReservedRegiseter.Add(new BasicInstructionModel
                        {
                            Id = instruction.Id,
                            LastIssued = true
                        });

                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Reads the instruction data when all conditions are meet
        /// </summary>
        /// <param name="instruction">The instruction that we want to read</param>
        /// <returns></returns>
        protected bool ReadIfApproved(BasicInstructionModel instruction)
        {
            //Get the functional unit that the instruction is working on
            var unit = FunctionalUnitsList.SingleOrDefault(item => item.WorkingInstructionID == instruction.Id);

            //If both Sources are ready to be read
            if (unit.IsSource01Ready &&
                (unit.IsSource02Ready || string.IsNullOrEmpty(unit._Source2Local)))
            {

                //Read the values and set the clock cycle
                instruction.Read = CurrentClockCycle;

                //Set the amount of the clokc cycles
                unit.Time = InstructionClockCycleCount.SingleOrDefault(function => function.Key == unit.Operation).Value;

                return true;
            }
            return false;
        }
        /// <summary>
        /// executes the instruction each clock cycle until end
        /// </summary>
        protected virtual void ExecuteInstrution(BasicInstructionModel instruction)
        {
            if (IsTomasoluSelected)
            {
                //Get the unit that the instruction is working on
                var unit = FunctionalUnitsList.SingleOrDefault(item => item.WorkingInstructionID == instruction.Id);

                if (--unit.Time == 0)
                {
                    //Set the clock cycle that is completed executing on
                    instruction.Executed = CurrentClockCycle;
                }
                else if (unit.Time < 0)
                {
                    unit.Time = 0;
                    //Get the register the instruction is working on
                    var register = Registers.SingleOrDefault(reg => reg.InstructionReservedRegiseter.SingleOrDefault(inst => inst.Id == instruction.Id) != null);
                    //If the insruction it self is the one reserving the register
                    if (register.InstructionReservedRegiseter.FirstOrDefault().Id == instruction.Id)
                    {
                        //Then it can write back on it
                        //Else it has to wait unitl it is freed up from the past instruction
                        instruction.WriteBack = CurrentClockCycle;
                        ClearUnitFunction(FunctionalUnitsList.SingleOrDefault(item => item.WorkingInstructionID == instruction.Id));
                    }

                }
            }
            else
            {
                //Get the unit that the instruction is working on
                var unit = FunctionalUnitsList.SingleOrDefault(item => item.WorkingInstructionID == instruction.Id);

                if (--unit.Time == 0)
                {
                    //Set the clock cycle that is completed executing on
                    instruction.Executed = CurrentClockCycle;
                }
                else if (unit.Time < 0)
                {
                    unit.Time = 0;
                    //Get the register the instruction is working on
                    var register = Registers.SingleOrDefault(reg => reg.InstructionReservedRegiseter.SingleOrDefault(inst => inst.Id == instruction.Id) != null);
                    var instStatusInReg = register.InstructionReservedRegiseter.SingleOrDefault(item => item.Id == instruction.Id);
                    //If its value is false it can not write and has to be register renames
                    if (!instStatusInReg.LastIssued)
                    {
                        //Then rename the target register of the register renameing process
                        //This way will end the WAW hazard
                        instruction.Target = $"R{++TomasoluRegisterCounter}";
                    }
                    //Always right back the value after the instruction finishs executing
                    instruction.WriteBack = CurrentClockCycle;
                    ClearUnitFunction(FunctionalUnitsList.SingleOrDefault(fcn => fcn.WorkingInstructionID == instruction.Id));
                }
            }

        }
        /// <summary>
        /// Frees up the register for WAR WAW hazerds 
        /// </summary>
        /// <param name="register"></param>
        protected void FreeUpRegisters(RegisterModel register, Guid instructionId)
        {
            var instruction = register.InstructionReservedRegiseter.SingleOrDefault(item => item.Id == instructionId);

            //Remove the instruction that is reserving the register
            register.InstructionReservedRegiseter.Remove(instruction);
            //If there is any more items
            if (register.InstructionReservedRegiseter.Any())
            {
                //then assign the working insruction as the first one
                register.IsBusy = true;
            }
            else
            {
                //Free up the registery
                register.IsBusy = false;
            }

        }
        /// <summary>
        /// Clears up the unit after the instruction writes back
        /// </summary>
        /// <param name="unit"></param>
        protected void ClearUnitFunction(FunctionalUnitModel unit)
        {
            unit.JustFreedUp = true;
            unit.Time = null;
            unit.Status = UnitStatus.Free;
            unit.WorkingInstructionID = Guid.Empty;
            unit.Operation = null;
            unit.Source1 = null;
            unit._Source1Local = null;
            unit._Source2Local = null;
            unit.Source2 = null;
            unit.Target = null;
            unit.WaitingOperationForSource01 = null;
            unit.WaitingOperationForSource02 = null;
            unit.IsSource01Ready = false;
            unit.IsSource02Ready = false;
        }
        /// <summary>
        /// Checks if the register are free so we can start reading 
        /// loops on them each time a result is writen back
        /// </summary>
        protected void RecheckIfRegistersAreFree()
        {
            foreach (var unit in FunctionalUnitsList)
            {
                if (unit._Source1Local != null && unit.IsSource01Ready == false)
                {
                    var registery = Registers.SingleOrDefault(reg => reg.Name == unit._Source1Local);
                    if (!registery.IsBusy ||
                        (registery.IsBusy && registery.InstructionReservedRegiseter.FirstOrDefault().Id == unit.WorkingInstructionID))
                    {
                        unit.Source1 = unit._Source1Local;
                        unit.IsSource01Ready = true;
                        unit.WaitingOperationForSource01 = null;
                    }
                }
                if (unit._Source2Local != null && unit.IsSource02Ready == false)
                {
                    var registery = Registers.SingleOrDefault(reg => reg.Name == unit._Source2Local);

                    if (!registery.IsBusy ||
                        (registery.IsBusy && registery.InstructionReservedRegiseter.FirstOrDefault().Id == unit.WorkingInstructionID))
                    {
                        unit.Source2 = unit._Source2Local;
                        unit.IsSource02Ready = true;
                        unit.WaitingOperationForSource02 = null;
                    }
                }
            }
        }
        /// <summary>
        /// Checks if all instructions has been executed and wrote there values back
        /// </summary>
        /// <returns></returns>
        protected bool CheckIfDone()
        {
            foreach (var instruction in Instructions)
            {
                if (instruction.WriteBack == null)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
        #region Helpers
        /// <summary>
        /// Fills the function list with the data
        /// </summary>
        private void FillFunctions()
        {
            Functions = new List<string>(Enum.GetNames(typeof(BasicFunctions)));
        }
        /// <summary>
        /// The function to fill the target and source list
        /// called once the function is selected
        /// </summary>
        private void FillTargetAndSoruce()
        {
            //Clear up the old selected values
            ClearInstructionValues();
            //If it is a load opperation 
            if (SelectedFunction == BasicFunctions.LD)
            {
                //You can only load to registers
                Targets = new ObservableCollection<string>(Enum.GetNames(typeof(Registeries)));
                //And the source is only a registery
                Sources = new ObservableCollection<string>(Enum.GetNames(typeof(Memmories)));
            }
            //If the operation is a store 
            else if (SelectedFunction == BasicFunctions.SD)
            {
                //You can only store in memeory
                Targets = new ObservableCollection<string>(Enum.GetNames(typeof(Memmories)));
                //And get the data from the registeries
                Sources = new ObservableCollection<string>(Enum.GetNames(typeof(Registeries)));
            }
            else
            {
                //If anything else the target can be any
                Targets = new ObservableCollection<string>(Enum.GetNames(typeof(Memmories)));
                Targets.AddRange(Enum.GetNames(typeof(Registeries)));
                //But only sorce from the registers
                Sources = new ObservableCollection<string>(Enum.GetNames(typeof(Registeries)));
            }

            CanChoseSource2 = !(SelectedFunction == BasicFunctions.LD || SelectedFunction == BasicFunctions.SD);
        }

        /// <summary>
        /// Fills the register list
        /// </summary>
        private void FillRegisterList()
        {
            //Create and fill the list
            Registers = new ObservableCollection<RegisterModel>(Enum.GetNames(typeof(Registeries)).Select(item => new RegisterModel
            {
                Name = item
            }).ToList());
        }

        /// <summary>
        /// Clears the instructions values for the new operation
        /// </summary>
        private void ClearInstructionValues()
        {
            SelectedTarget = null;
            SelectedSource1 = null;
            SelectedSource2 = null;
        }
        #endregion
    }
}
