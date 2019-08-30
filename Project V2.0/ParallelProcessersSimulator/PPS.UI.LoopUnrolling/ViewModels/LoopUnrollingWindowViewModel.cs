using PPS.UI.LoopUnrolling.Enums;
using PPS.UI.LoopUnrolling.Models;
using PPS.UI.Shared.Enums;
using PPS.UI.Shared.ViewModels.Base;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PPS.UI.LoopUnrolling.ViewModels
{
    public class LoopUnrollingWindowViewModel : BaseViewModel
    {

        #region Private members
        /// <summary>
        /// The increment value in the loop
        /// </summary>
        private static readonly int LoopIncrementStep = 4;
        #endregion

        #region Properties
        /// <summary>
        /// The counter for the instrions number
        /// </summary>
        private int mCounter;
        public int Counter
        {
            get { return mCounter; }
            set { SetProperty(ref mCounter, value); }
        }
        
       
        private ObservableCollection<LoopUnrolInstructionModel> mInstructions;
        public ObservableCollection<LoopUnrolInstructionModel> Instructions
        {
            get { return mInstructions; }
            set { SetProperty(ref mInstructions, value); }
        }
        /// <summary>
        /// The branch operations
        /// </summary>
        public List<string> BranchOperations { get; set; } = Enum.GetNames(typeof(LoopFunctions)).ToList();
        private LoopFunctions mSelectedBranchOperation;
        public LoopFunctions SelectedBranchOperation
        {
            get { return mSelectedBranchOperation; }
            set { SetProperty(ref mSelectedBranchOperation, value); }
        }

        public string SelectedBranchRegistery01 { get; set; }
        public int LoopCounter { get; set; }
        private int mInstructionNumToLoopTo = 1;
        public int InstructionNumToLoopTo
        {
            get { return mInstructionNumToLoopTo; }
            set { SetProperty(ref mInstructionNumToLoopTo, value); }
        }
        /// <summary>
        /// The registeries to choose from for a branch
        /// </summary>
        public List<string> BranchRegisteries { get; set; } = Enum.GetNames(typeof(Registeries)).ToList();


        /// <summary>
        /// The selected instruction in the main code enter
        /// </summary>
        private LoopUnrolInstructionModel mSelectedInstruction;
        public LoopUnrolInstructionModel SelectedInstruction
        {
            get { return mSelectedInstruction; }
            set { SetProperty(ref mSelectedInstruction, value); }
        }

        /// <summary>
        /// The amount of clock cycles that an integer opperation will take
        /// such as SD(Stor) LD(Load)
        /// Default to 1
        /// </summary>
        protected int _integerClockCycles = 1;
        public int IntegerClockCycles
        {
            get { return _integerClockCycles; }
            set { SetProperty(ref _integerClockCycles, value); }
        }
        /// <summary>
        /// The amount of clock cycles that an Add opperation will take
        /// such as ADD ,SUBD
        /// Default to 2
        /// </summary>
        protected int _floatingPointAddClockCylces = 2;
        public int FloatingPointAddClockCycles
        {
            get { return _floatingPointAddClockCylces; }
            set { SetProperty(ref _floatingPointAddClockCylces, value); }
        }
        /// <summary>
        /// The amount of clock cycles that a Multiply opperation will take
        /// such as MULTD
        /// Default to 10 
        /// </summary>
        protected int _floatingPointMultiplyClockCycles = 10;
        public int FloatinPointMultiplyClockCycles
        {
            get { return _floatingPointMultiplyClockCycles; }
            set { SetProperty(ref _floatingPointMultiplyClockCycles, value); }
        }
        /// <summary>
        /// The amount of clock cycles that a Divide opperation will take
        /// such as DIVD
        /// Default to 40 
        /// </summary>
        protected int _floatingPointDivideClockCycles = 10;
        public int FloatinPointDivideClockCycles
        {
            get { return _floatingPointDivideClockCycles; }
            set { SetProperty(ref _floatingPointDivideClockCycles, value); }
        }
        private int? mUnrollLoopTimes;
        public int? UnrollLoopTimes
        {
            get { return mUnrollLoopTimes; }
            set { SetProperty(ref mUnrollLoopTimes, value); }
        }
        #endregion

     
        #region Commands
        /// <summary>
        /// the command to delete an instruction
        /// </summary>
        public DelegateCommand DeleteInstructionCommand { get; set; }
        /// <summary>
        /// The command to clear the instrion list
        /// </summary>
        public DelegateCommand ClearAllInstructionsCommand { get; set; }
        /// <summary>
        /// The command to add an instruction
        /// </summary>
        public DelegateCommand AddInstructionCommand { get; set; }
        public DelegateCommand ExecuteCodeCommand { get; set; }
        #endregion

        #region Command Metods

        /// <summary>
        /// The function to check if the <see cref="ExecuteCodeCommand"/> can get executed
        /// </summary>
        /// <returns></returns>
        public bool ExecuteCodeCommandCan_Execute()
        {
            return Counter > 0;
        }

        /// <summary>
        /// The function that will be called once the <see cref="ExecuteCodeCommand"/> is invoked
        /// </summary>
        public void ExecuteCodeCommand_Execute()
        {
            ExecutedCodeClockCycles = 3;
            ExecutedInstructions = ExecuteCode(Instructions, false);
        }

        /// <summary>
        /// The function to check if the <see cref="DeleteInstructionCommand"/> can get executed
        /// </summary>
        /// <returns></returns>
        public bool DeleteInstructionCommandCan_Execute()
        {
            return SelectedInstruction != null;
        }
        /// <summary>
        /// The function that will be called once the <see cref="DeleteInstructionCommand"/> is invoked
        /// </summary>
        public void DeleteInstructionCommand_Execute()
        {
            if (SelectedInstruction != null)
            {
                var index = Instructions.IndexOf(SelectedInstruction);
                ReOrderAfterDelete(index + 1);
                Instructions.Remove(SelectedInstruction);
            }
        }
        /// <summary>
        /// The function that will be called once the <see cref="ClearAllInstructionsCommand"/> is invoked
        /// </summary>
        public void ClearAllInstructionsCommand_Execute()
        {
            Counter = 0;
            Instructions = new ObservableCollection<LoopUnrolInstructionModel>();
        }


        /// <summary>
        /// The function that will be called once the <see cref="AddInstructionCommand"/> is invoked
        /// </summary>
        public void AddInstructionCommand_Execute()
        {
            //adds a new row
            Instructions.Add(new LoopUnrolInstructionModel { Order = ++Counter });
        }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public LoopUnrollingWindowViewModel()
        {
            Instructions = new ObservableCollection<LoopUnrolInstructionModel>();


            DeleteInstructionCommand = new DelegateCommand(DeleteInstructionCommand_Execute, DeleteInstructionCommandCan_Execute).ObservesProperty(() => SelectedInstruction);
            ClearAllInstructionsCommand = new DelegateCommand(ClearAllInstructionsCommand_Execute);
            AddInstructionCommand = new DelegateCommand(AddInstructionCommand_Execute);
            ExecuteCodeCommand = new DelegateCommand(ExecuteCodeCommand_Execute, ExecuteCodeCommandCan_Execute).ObservesProperty(() => Counter);
            UnrollCodeCommand = new DelegateCommand(UnrollCodeCommand_Execute, UnrollCodeCommandCan_Execute).ObservesProperty(() => UnrollLoopTimes);
            ExecuteUnrolledCodeCommand = new DelegateCommand(ExecuteUnrolledCodeCommand_Execute, ExecuteUnrolledCodeCommandCan_Execute).ObservesProperty(() => UnrolledInstructions);
            ExecuteScheduledCodeCommand = new DelegateCommand(ExecuteScheduledCodeCommand_Execute, ExecuteScheduledCodeCommandCan_Execute).ObservesProperty(() => UnrolledExecutedInstructions);
        }
        #endregion

        #region Code Execution

        #region Properties
        /// <summary>
        /// The instruction after execution with the stalls
        /// </summary>
        private ObservableCollection<LoopUnrolInstructionModel> mExecutedInstructions;

        public ObservableCollection<LoopUnrolInstructionModel> ExecutedInstructions
        {
            get { return mExecutedInstructions; }
            set { SetProperty(ref mExecutedInstructions, value); }
        }

        private int? mExecutedCodeClockCycles = 2;
        public int? ExecutedCodeClockCycles
        {
            get { return mExecutedCodeClockCycles; }
            set { SetProperty(ref mExecutedCodeClockCycles, value); }
        }
        private int? mUnrolledExecutedCodeClockCycles = 2;
        public int? UnrolledExecutedCodeClockCycles
        {
            get { return mUnrolledExecutedCodeClockCycles; }
            set { SetProperty(ref mUnrolledExecutedCodeClockCycles, value); }
        }
        private int? mUnrolledScheduledExecutedCodeClockCycles = 2;
        public int? UnrolledScheduledExecutedCodeClockCycles
        {
            get { return mUnrolledScheduledExecutedCodeClockCycles; }
            set { SetProperty(ref mUnrolledScheduledExecutedCodeClockCycles, value); }
        }

        #endregion

        #region Commands
        public DelegateCommand UnrollCodeCommand { get; set; }
        #endregion

        #region Command Methods

        /// <summary>
        /// The function to check if the <see cref="UnrollCodeCommand"/> can get executed
        /// </summary>
        /// <returns></returns>
        public bool UnrollCodeCommandCan_Execute()
        {
            return UnrollLoopTimes > 0;
        }

        /// <summary>
        /// The function that will be called once the <see cref="UnrollCodeCommand"/> is invoked
        /// </summary>
        public void UnrollCodeCommand_Execute()
        {
            UnrollAndExecuteCodeLoop();
        }

        #endregion

        private ObservableCollection<LoopUnrolInstructionModel> ExecuteCode(ObservableCollection<LoopUnrolInstructionModel> instructions, bool unrolling = true, bool scheduled = false)
        {
            string stallCycle = "Stall Num ";
            //Add the first instruction as it will execute first
            var ExecutedInstructions = new ObservableCollection<LoopUnrolInstructionModel>();
            //The instructions that are in execution
            var InstructionsInExecution = new ObservableCollection<LoopUnrolInstructionModel>();
            //Set the execution clock cycle
            int executionCycles = 0;
            int stallCounter = 0;

            //Loop throw the instructions 
            for (int i = 0; i < instructions.Count; i++)
            {
                foreach (var item in InstructionsInExecution)
                {
                    item.ExecutionClockCycles--;
                }

                switch (Enum.Parse(typeof(BasicFunctions), instructions[i].Operation))
                {
                    case BasicFunctions.LD:
                        executionCycles = IntegerClockCycles;
                        break;
                    case BasicFunctions.SD:
                        {
                            //If  load cam after a store 
                            if (i + 1 < instructions.Count && instructions[i + 1].Operation == BasicFunctions.LD.ToString())
                            {
                                //No stalles
                                executionCycles = 0;
                                break;
                            }
                        }
                        executionCycles = IntegerClockCycles;
                        break;
                    case BasicFunctions.ADD:
                    case BasicFunctions.SUB:
                        executionCycles = FloatingPointAddClockCycles;
                        break;
                    case BasicFunctions.DIV:
                        executionCycles = FloatinPointDivideClockCycles;
                        break;
                    case BasicFunctions.MULT:
                        executionCycles = FloatinPointMultiplyClockCycles;
                        break;
                }
                //Added a plus 1 to the execution so it dose not count the first iteration as a cycle 
                instructions[i].ExecutionClockCycles = executionCycles + 1;
                //Add the first instruction immediately
                if (i == 0)
                {
                    //Add the instrucion into execution
                    InstructionsInExecution.Add(instructions[i]);
                    //Add it to the end list
                    ExecutedInstructions.Add(instructions[i]);
                }
                else
                {
                    //If there is not data hazerds
                    if (InstructionsInExecution.FirstOrDefault(item => item.ExecutionClockCycles > 0 && (item.TargetRegistery == instructions[i].SourceRegistery01 || item.TargetRegistery == instructions[i].SourceRegistery02)) == null)
                    {
                        //Add the instrucion into execution
                        InstructionsInExecution.Add(instructions[i]);
                        //Add it to the end list
                        ExecutedInstructions.Add(instructions[i]);

                        stallCounter = 0;
                    }
                    else
                    {
                        //Add a stall
                        ExecutedInstructions.Add(new LoopUnrolInstructionModel
                        {
                            SourceRegistery01 = stallCycle + ++stallCounter
                        });
                        //Stay on the same instruction until it enters execution
                        i--;
                    }
                }
                if (scheduled)
                {
                    UnrolledScheduledExecutedCodeClockCycles++;
                }
                else if (unrolling)
                {
                    UnrolledExecutedCodeClockCycles++;
                }
                else
                {
                    ExecutedCodeClockCycles++;
                }
            }
            var lastOrder = instructions.LastOrDefault().Order;
            ExecutedInstructions.Add(new LoopUnrolInstructionModel
            {
                Order = lastOrder + 1,
                Operation = BasicFunctions.SUB.ToString(),
                TargetRegistery = SelectedBranchRegistery01,
                SourceRegistery01 = SelectedBranchRegistery01,
                ImmediateValueOrDisplacmnet = unrolling || scheduled ? LoopIncrementStep * UnrollLoopTimes : LoopIncrementStep,
            });
            stallCounter = 0;
            //Add the stalls for the sub operation
            for (int i = 0; i < FloatingPointAddClockCycles; i++)
            {
                //Add a stall
                ExecutedInstructions.Add(new LoopUnrolInstructionModel
                {
                    SourceRegistery01 = stallCycle + ++stallCounter
                });

                if (scheduled)
                {
                    UnrolledScheduledExecutedCodeClockCycles++;
                }
                else if (unrolling)
                {
                    UnrolledExecutedCodeClockCycles++;
                }
                else
                {
                    ExecutedCodeClockCycles++;
                }
            }
            ExecutedInstructions.Add(new LoopUnrolInstructionModel
            {
                Order = lastOrder + 2,
                Operation = SelectedBranchOperation.ToString(),
                SourceRegistery01 = SelectedBranchRegistery01,
            });

            ExecutedInstructions.Add(new LoopUnrolInstructionModel
            {
                SourceRegistery01 = stallCycle + 1,
            });

            if (scheduled)
            {
                SpeedUp = (double)((ExecutedCodeClockCycles * UnrollLoopTimes * 1.0) / UnrolledScheduledExecutedCodeClockCycles * 1.0); 
            }
            return ExecutedInstructions;
        }

        #endregion

        #region Unroll loop
        #region Commands
        public DelegateCommand ExecuteUnrolledCodeCommand { get; set; }

        public DelegateCommand ExecuteScheduledCodeCommand { get; set; }

        #endregion

        #region Command Methods

        /// <summary>
        /// The function to check if the <see cref="ExecuteScheduledCodeCommand"/> can get executed
        /// </summary>
        /// <returns></returns>
        public bool ExecuteScheduledCodeCommandCan_Execute()
        {
            return UnrolledExecutedInstructions != null;
        }

        /// <summary>
        /// The function that will be called once the <see cref="ExecuteScheduledCodeCommand"/> is invoked
        /// </summary>
        public void ExecuteScheduledCodeCommand_Execute()
        {
            ScheduledExecutedLoopCode = ScheduledUnrolledLoop();
        }


        /// <summary>
        /// The function to check if the <see cref="ExecuteUnrolledCodeCommand"/> can get executed
        /// </summary>
        /// <returns></returns>
        public bool ExecuteUnrolledCodeCommandCan_Execute()
        {
            return UnrolledInstructions != null;
        }

        /// <summary>
        /// The function that will be called once the <see cref="ExecuteUnrolledCodeCommand"/> is invoked
        /// </summary>
        public void ExecuteUnrolledCodeCommand_Execute()
        {
            //Execute the instructions
            UnrolledExecutedCodeClockCycles = 3;
            UnrolledExecutedInstructions = ExecuteCode(UnrolledInstructions, true);
        }

        #endregion
        
        #region Properties
        /// <summary>
        /// The instruction after execution and unrolling with the stalls
        /// </summary>
        private ObservableCollection<LoopUnrolInstructionModel> mUnrolledInstructions;

        public ObservableCollection<LoopUnrolInstructionModel> UnrolledInstructions
        {
            get { return mUnrolledInstructions; }
            set { SetProperty(ref mUnrolledInstructions, value); }
        }
        private ObservableCollection<LoopUnrolInstructionModel> mUnrolledExecutedInstructions;

        public ObservableCollection<LoopUnrolInstructionModel> UnrolledExecutedInstructions
        {
            get { return mUnrolledExecutedInstructions; }
            set { SetProperty(ref mUnrolledExecutedInstructions, value); }
        }
        #endregion

        private void UnrollAndExecuteCodeLoop()
        {
            //If we want to unroll more than the loop counter 
            if (LoopCounter < UnrollLoopTimes)
            {
                //Spread the loop up
                UnrollLoopTimes = LoopCounter;
            }
            var integerIncremnt = 4;
            var displacmentIncrement = 0;
            int order = 0;
            UnrolledInstructions = new ObservableCollection<LoopUnrolInstructionModel>();
            var leftRegistersForRename = Enum.GetNames(typeof(Registeries)).ToList();
            if (SelectedBranchRegistery01 != null)
            {
                leftRegistersForRename.Remove(SelectedBranchRegistery01);
            }
            //get The left registers that are not used
            foreach (var item in Instructions)
            {
                if (leftRegistersForRename.Contains(item.TargetRegistery))
                {
                    leftRegistersForRename.Remove(item.TargetRegistery);
                }
                if (leftRegistersForRename.Contains(item.SourceRegistery01))
                {
                    leftRegistersForRename.Remove(item.SourceRegistery01);
                }
                if (leftRegistersForRename.Contains(item.SourceRegistery02))
                {
                    leftRegistersForRename.Remove(item.SourceRegistery02);
                }

            }
            for (int i = 0; i < InstructionNumToLoopTo - 1; i++)
            {
                var newInstruction = new LoopUnrolInstructionModel
                {
                    Order = ++order,
                    Operation = Instructions[i].Operation,
                    SourceRegistery01 = Instructions[i].SourceRegistery01,
                    SourceRegistery02 = Instructions[i].SourceRegistery02,
                    TargetRegistery = Instructions[i].TargetRegistery,
                    ImmediateValueOrDisplacmnet = Instructions[i].ImmediateValueOrDisplacmnet,
                };

                UnrolledInstructions.Add(newInstruction);
            }


            for (int i = 0; i < UnrollLoopTimes; i++)
            {
                for (int j = InstructionNumToLoopTo - 1; j < Instructions.Count; j++)
                {
                    var newInstruction = new LoopUnrolInstructionModel
                    {
                        Order = ++order,
                        Operation = Instructions[j].Operation,
                        SourceRegistery01 = Instructions[j].SourceRegistery01,
                        SourceRegistery02 = Instructions[j].SourceRegistery02,
                        TargetRegistery = Instructions[j].TargetRegistery,
                        ImmediateValueOrDisplacmnet = Instructions[j].ImmediateValueOrDisplacmnet,
                    };

                    //If the instruction is a load ...
                    if (newInstruction.Operation == BasicFunctions.LD.ToString() || newInstruction.Operation == BasicFunctions.SD.ToString())
                    {
                        //Then we need to move the displacment
                        newInstruction.ImmediateValueOrDisplacmnet += displacmentIncrement;

                    }
                    UnrolledInstructions.Add(newInstruction);
                }
                displacmentIncrement += integerIncremnt;
            }

            var targetRegDic = new Dictionary<string, string>();
            for (int i = Instructions.Count; i < UnrolledInstructions.Count; i++)
            {
                if (UnrolledInstructions[i].Operation == BasicFunctions.SD.ToString())
                {
                    //If the registery must be renamed
                    if (targetRegDic.TryGetValue(UnrolledInstructions[i].SourceRegistery01 ?? "", out string newSource))
                    {
                        UnrolledInstructions[i].SourceRegistery01 = newSource;
                    }
                    continue;
                }
                //If we went throw a loop
                if (i % Instructions.Count == 0)
                {
                    targetRegDic = new Dictionary<string, string>();
                }
                //Get a registery to rename for
                var renamedReg = leftRegistersForRename.FirstOrDefault();
                //Check if any lefy
                if (renamedReg != null)
                {

                    try
                    {
                        targetRegDic.Add(UnrolledInstructions[i].TargetRegistery, renamedReg);

                    }
                    catch (Exception)
                    {
                        //MessageBox.Show("Please check code and try agian");
                        break;
                    }
                    UnrolledInstructions[i].TargetRegistery = renamedReg;
                    leftRegistersForRename.Remove(renamedReg);
                }
                else
                {
                    ShowErrorMessage("No more left empty registers!!");
                    break;
                }

                try
                {
                    //If the registery must be renamed
                    if (targetRegDic.TryGetValue(UnrolledInstructions[i].SourceRegistery01, out string value))
                    {
                        UnrolledInstructions[i].SourceRegistery01 = value;
                    }
                    //If the registery must be renamed
                    if (targetRegDic.TryGetValue(UnrolledInstructions[i].SourceRegistery02 ?? "", out string value2))
                    {
                        UnrolledInstructions[i].SourceRegistery02 = value2;
                    }
                }
                catch (Exception) 
                {
                    ShowErrorMessage("Please check code and try agian");
                    break;
                }
            }
        }

        #region Schedul code
        private ObservableCollection<LoopUnrolInstructionModel> ScheduledUnrolledLoop()
        {
            var ScheduledExecutedLoopCode = new ObservableCollection<LoopUnrolInstructionModel>();

            var operations = Instructions.OrderBy(item => item.Order).Select(item => item.Operation).Distinct().ToList();
            int order = 0;
            //Get all the operations in order
            foreach (var op in operations)
            {
                //Get the instructions that got this operation
                var opInstructions = UnrolledInstructions.Where(item => item.Operation == op).ToList();

                //Loop throw the instructions and add them 
                foreach (var inst in opInstructions)
                {
                    var newInstruction = new LoopUnrolInstructionModel
                    {
                        Order = ++order,
                        Operation = inst.Operation,
                        SourceRegistery01 = inst.SourceRegistery01,
                        SourceRegistery02 = inst.SourceRegistery02,
                        TargetRegistery = inst.TargetRegistery,
                        ImmediateValueOrDisplacmnet = inst.ImmediateValueOrDisplacmnet,
                    };

                    ScheduledExecutedLoopCode.Add(newInstruction);
                }
            }
            UnrolledScheduledExecutedCodeClockCycles = 3;

            return ExecuteCode(ScheduledExecutedLoopCode, scheduled: true);
        }

        #endregion

        #endregion

        #region Unroll Scheduled Executed Loop

        #region Properties
        private ObservableCollection<LoopUnrolInstructionModel> mScheduledExecutedLoopCode;
        public ObservableCollection<LoopUnrolInstructionModel> ScheduledExecutedLoopCode
        {
            get { return mScheduledExecutedLoopCode; }
            set { SetProperty(ref mScheduledExecutedLoopCode, value); }
        }

        /// <summary>
        /// The speed up percentage for the last unrolled schualed code
        /// </summary>
        private double _speedUp;
        public double SpeedUp
        {
            get { return this._speedUp; }
            set { SetProperty(ref _speedUp, value); }
        }
        #endregion


        #endregion

        #region Helpers
        /// <summary>
        /// ReSets the instruction number when the user deletes an item
        /// </summary>
        /// <param name="instructionNumber">The Id of the instruction that was deleted</param>
        protected void ReOrderAfterDelete(int instructionNumber)
        {
            //Empty list holds the instructions that we need to move to hold the order right
            var listToMove = new List<LoopUnrolInstructionModel>();
            //Looping throw all the items
            foreach (var item in Instructions)
            {
                //If the id is greater than the instrions that we are deleteing then...
                if (item.Order > instructionNumber)
                {
                    //Add it to the moving list
                    listToMove.Add(item);
                }
            }
            //Downgrade the counter by one for the deleted item
            Counter--;
            //Loop throw the list that we want to move
            foreach (var item in listToMove)
            {
                //Get the index of the item that we want to edit
                var index = Instructions.IndexOf(item);
                //Remove it
                Instructions.RemoveAt(index);
                //Set the right id
                item.Order--;
                //Re add it to the collection
                Instructions.Insert(index, item);
            }
        }
        #endregion
    }
}
