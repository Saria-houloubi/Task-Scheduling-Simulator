using Tishreen.ParallelPro.Core.Models;
using Prism.Commands;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using ThishreenUniversity.ParallelPro.Enums;
using Ninject;

namespace Tishreen.ParallelPro.Core
{
    /// <summary>
    /// The class that will take care of all algoritms instruction menu
    /// </summary>
    public class MainAlgorithmsInstructionMenuViewModel : BaseViewModel
    {
        #region Private Members
        /// <summary>
        /// The couter for the instructions
        /// </summary>
        private int counter = 1;
        #endregion

        #region Properties
        /// <summary>
        /// The selected function that the user wants for the new instruction
        /// </summary>
        private string _selectedFunction;
        public string SelectedFunction
        {
            get { return _selectedFunction; }
            set
            {
                SetProperty(ref _selectedFunction, value);

                ///Fill the target and source registeries with the right values
                if (value != null)
                    FillTargetAndSourceRegisteries(value);
            }
        }
        /// <summary>
        /// the target registry for the instruction to store the value in
        /// </summary>
        private string _selectedTargetRegistery;
        public string SelectedTargetRegistery
        {
            get { return _selectedTargetRegistery; }
            set { SetProperty(ref _selectedTargetRegistery, value); }
        }
        /// <summary>
        /// The first source registry to get the value from
        /// </summary>
        private string _selectedSourceRegistery01;
        public string SelectedSourceRegistery01
        {
            get { return _selectedSourceRegistery01; }
            set { SetProperty(ref _selectedSourceRegistery01, value); }
        }
        /// <summary>
        /// The second soruce registry to get the value from
        /// </summary>
        private string _slectedSourceRegistery02;
        public string SelectedSourceRegistery02
        {
            get { return _slectedSourceRegistery02; }
            set { SetProperty(ref _slectedSourceRegistery02, value); }
        }
        /// <summary>
        /// The instruction that is seleted for edit or delete
        /// </summary>
        private InstructionModel _selectedInstruction;
        public InstructionModel SelectedInstruction
        {
            get { return _selectedInstruction; }
            set { SetProperty(ref _selectedInstruction, value); }
        }

        /// <summary>
        /// The amount of clock cycles that an integer opperation will take
        /// such as SD(Stor) LD(Load)
        /// Default to 1
        /// </summary>
        private int _integerClockCycles = 1;
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
        private int _floatingPointAddClockCylces = 2;
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
        private int _floatingPointMultiplyClockCycles = 10;
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
        private int _floatingPointDivideClockCycles = 10;
        public int FloatinPointDivideClockCycles
        {
            get { return _floatingPointDivideClockCycles; }
            set { SetProperty(ref _floatingPointDivideClockCycles, value); }
        }

        #region Lists And Collections
        /// <summary>
        /// Holds all the function unite that we can do like ADD, SUB ... 
        /// </summary>
        private List<string> _functions;
        public List<string> Functions
        {
            get { return _functions; }
            set { SetProperty(ref _functions, value); }
        }
        /// <summary>
        /// Holds all the target registries that the user can use
        /// </summary>
        public ObservableCollection<string> TargetRegistries { get; set; }
        /// <summary>
        /// Holds all the source registries that the user can use
        /// </summary>
        public ObservableCollection<string> SourceRegisteries { get; set; }
        /// <summary>
        /// The list of instructions that the user adds
        /// </summary>
        private ObservableCollection<InstructionModel> _instructions;
        public ObservableCollection<InstructionModel> Instructions
        {
            get { return _instructions; }
            set { SetProperty(ref _instructions, value); }
        }
        #endregion

        #region Flags
        /// <summary>
        /// A flag represents if the user can choose another source for the instruction
        /// like in SD/LD only one source
        /// </summary>
        private bool _canChooseSource02 = true;
        public bool CanChooseSource02
        {
            get { return _canChooseSource02; }
            set { SetProperty(ref _canChooseSource02, value); }
        }
        #endregion

        #endregion

        #region Commands
        /// <summary>
        /// The command to add an instruction to the <see cref="Instructions"/>
        /// </summary>
        public DelegateCommand AddInstructionCommand { get; private set; }
        /// <summary>
        /// The command that delete an instruction
        /// </summary>
        public DelegateCommand DeleteItemCommand { get; private set; }
        /// <summary>
        /// The command to open the spacific algorithm window
        /// </summary>
        public DelegateCommand<object> OpenAlgoWindowCommand { get; private set; }
        #endregion

        #region Fill Functions
        /// <summary>
        /// Fill the functions list with the type of functions that we support
        /// </summary>
        private void FillFunctionList()
        {
            Functions = new List<string>();

            //Loops thru the enum values an add them to the functions list
            foreach (var item in Enum.GetValues(typeof(FunctionsTypes)))
                Functions.Add(item.ToString());
        }

        /// <summary>
        /// Fill the target and the source registeries or memory that the user can choose
        /// </summary>
        /// <param name="function">The function that we want to restrict some registery or memory access</param>
        private void FillTargetAndSourceRegisteries(string function = null)
        {
            TargetRegistries.Clear();
            SourceRegisteries.Clear() ;

            var RegisteryAndMemoryList = Enum.GetValues(typeof(RegisteriesAndMemory));

            foreach (var item in RegisteryAndMemoryList)
            {
                var stringValue = Enum.GetName(typeof(RegisteriesAndMemory), item);
                if (function == FunctionsTypes.LD.ToString())
                {
                    //If it is a registery spot add it to target
                    if ((int)item < 31) 
                        TargetRegistries.Add(stringValue);
                    else
                        SourceRegisteries.Add(stringValue);
                }
                else if (function == FunctionsTypes.SD.ToString())
                {
                    //If it is a memory add it to target
                    if ((int)item >= 31)
                        TargetRegistries.Add(stringValue);
                    else
                        SourceRegisteries.Add(stringValue);
                } 
                else
                {
                    TargetRegistries.Add(stringValue);
                    //If it is a registery spot add it to target
                    if ((int)item < 31)
                        SourceRegisteries.Add(stringValue);
                }
            }
            //Disable source02 if the function is either load or store
            if (function == FunctionsTypes.LD.ToString() || function == FunctionsTypes.SD.ToString())
                CanChooseSource02 = false;
            else
                CanChooseSource02 = true;

        }
        #endregion

        #region Constructer
        /// <summary>
        /// The method that will be called on the create of the class
        /// </summary>
        private void OnStart()
        {
            //Fill the lists
            FillFunctionList();

            //Create the list
            Instructions = new ObservableCollection<InstructionModel>();
            TargetRegistries = new ObservableCollection<string>();
            SourceRegisteries = new ObservableCollection<string>();

        }

        /// <summary>
        /// Default constructer
        /// </summary>
        public MainAlgorithmsInstructionMenuViewModel()
        {
            OnStart();
            //Create Commands
            AddInstructionCommand = new DelegateCommand(() =>
            {
                Instructions.Add(new InstructionModel(counter++, SelectedFunction, SelectedTargetRegistery, SelectedSourceRegistery01, SelectedSourceRegistery02));
                EmptyProperties();
            }, () => { return SelectedFunction != null && !string.IsNullOrWhiteSpace(SelectedTargetRegistery) && !string.IsNullOrEmpty(SelectedSourceRegistery01) && (!string.IsNullOrEmpty(SelectedSourceRegistery02) || SelectedFunction == FunctionsTypes.LD.ToString() || SelectedFunction == FunctionsTypes.SD.ToString()); }).ObservesProperty(() => SelectedFunction).ObservesProperty(() => SelectedTargetRegistery).ObservesProperty(() => SelectedSourceRegistery01).ObservesProperty(() => SelectedSourceRegistery02);
            DeleteItemCommand = new DelegateCommand(() =>
            {
                ReOrderAfterDelete(SelectedInstruction.ID);
                Instructions.Remove(SelectedInstruction);
            }, () => { return SelectedInstruction != null; }).ObservesProperty(() => SelectedInstruction);
            OpenAlgoWindowCommand = new DelegateCommand<object>((parameter) =>
            {
                //Setting the list for the functionlal unit clock cycles
                var functionClockCycles = new List<KeyValuePair<FunctionsTypes, int>>
                {
                    new KeyValuePair<FunctionsTypes, int>(FunctionsTypes.ADD, FloatingPointAddClockCycles),
                    new KeyValuePair<FunctionsTypes, int>(FunctionsTypes.DIV,FloatinPointDivideClockCycles),
                    new KeyValuePair<FunctionsTypes, int>(FunctionsTypes.LD,IntegerClockCycles),
                    new KeyValuePair<FunctionsTypes, int>(FunctionsTypes.MULT,FloatinPointMultiplyClockCycles),
                    new KeyValuePair<FunctionsTypes, int>(FunctionsTypes.SD,IntegerClockCycles),
                    new KeyValuePair<FunctionsTypes, int>(FunctionsTypes.SUB,FloatingPointAddClockCycles),
                };
               IoC.Kernel.Get<IUIManager>().ShowWinodw((ApplicationPages)parameter,new List<object>(Instructions),functionClockCycles);
            });
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Empties the properties after adding a new instruction
        /// </summary>
        private void EmptyProperties()
        {
            SelectedFunction = null;
            SelectedTargetRegistery = null;
            SelectedSourceRegistery01 = null;
            SelectedSourceRegistery02 = null;
        }
        /// <summary>
        /// ReSets the instruction number when the user deletes an item
        /// </summary>
        /// <param name="instructionNumber">The Id of the instruction that was deleted</param>
        private void ReOrderAfterDelete(int instructionNumber)
        {
            //Empty list holds the instructions that we need to move to hold the order right
            var listToMove = new List<InstructionModel>();
            //Looping throw all the items
            foreach (var item in Instructions)
            {
                //If the id is greater than the instrions that we are deleteing then...
                if (item.ID > instructionNumber)
                    //Add it to the moving list
                    listToMove.Add(item);
            }
            //Downgrade the counter by one for the deleted item
            counter--;
            //Loop throw the list that we want to move
            foreach (var item in listToMove)
            {
                //Get the index of the item that we want to edit
                var index = Instructions.IndexOf(item);
                //Remove it
                Instructions.RemoveAt(index);
                //Set the right id
                item.ID--;
                //Re add it to the collection
                Instructions.Insert(index, item);
            }
        }

        #endregion

    }
}
    