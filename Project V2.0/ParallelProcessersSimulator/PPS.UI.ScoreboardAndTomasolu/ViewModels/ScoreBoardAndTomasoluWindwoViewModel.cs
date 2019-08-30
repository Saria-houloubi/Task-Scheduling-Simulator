using PPS.UI.ScoreboardAndTomasolu.Enums;
using PPS.UI.Shared.Enums;
using PPS.UI.Shared.Models;
using PPS.UI.Shared.ViewModels.Base;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace PPS.UI.ScoreboardAndTomasolu.ViewModels
{
    public class ScoreBoardAndTomasoluWindwoViewModel : BaseViewModel
    {
        #region Properties
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
        public Dictionary<FunctionalUnits,int> FunctionalUnitsCount{ get; set; }
        /// <summary>
        /// The clock cycle count for the basic instruction to execute
        /// </summary>
        public Dictionary<BasicFunctions,int> InstructionClockCycleCount{ get; set; }
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
        #endregion

        #region Command Methods
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
                { BasicFunctions.ADD, 2},
                { BasicFunctions.MULT, 10},
                { BasicFunctions.DIV, 10}
            };

            try
            {
                FillFunctions();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.GetBaseException().Message);
            }
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
