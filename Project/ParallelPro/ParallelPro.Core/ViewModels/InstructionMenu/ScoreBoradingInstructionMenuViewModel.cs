using Tishreen.ParallelPro.Core.Models;
using Prism.Commands;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

namespace Tishreen.ParallelPro.Core
{
    /// <summary>
    /// The class that will take care of all scoreboarding instruction menu
    /// </summary>
    public class ScoreBoradingInstructionMenuViewModel : BaseViewModel
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
            set { SetProperty(ref _selectedFunction, value); }
        }
        /// <summary>
        /// the target registry for the instruction to store the value in
        /// </summary>
        private string _targetRegistry;
        public string TargetRegistry
        {
            get { return _targetRegistry; }
            set { SetProperty(ref _targetRegistry, value); }
        }
        /// <summary>
        /// The first source registry to get the value from
        /// </summary>
        private string _sourceRegistry01;
        public string SourceRegistry01
        {
            get { return _sourceRegistry01; }
            set { SetProperty(ref _sourceRegistry01, value); }
        }
        /// <summary>
        /// The second soruce registry to get the value from
        /// </summary>
        private string _sourceRegistry02;
        public string SourceRegistry02
        {
            get { return _sourceRegistry02; }
            set { SetProperty(ref _sourceRegistry02, value); }
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
        /// The list of instructions that the user adds
        /// </summary>
        private ObservableCollection<InstructionModel> _instructions;
        public ObservableCollection<InstructionModel> Instructions
        {
            get { return _instructions; }
            set { SetProperty(ref _instructions, value); }
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
        #endregion

        #region Fill Functions
        /// <summary>
        /// Fill the functions list with the type of functions that we support
        /// </summary>
        private void FillFunctionList()
        {
            Functions = new List<string>
            {
                "LD",
                "ADD",
                "SUB",
                "DIV",
                "MULT",
            };
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
        }

        /// <summary>
        /// Default constructer
        /// </summary>
        public ScoreBoradingInstructionMenuViewModel()
        {
            OnStart();
            //Create Commands
            AddInstructionCommand = new DelegateCommand(() =>
            {
                Instructions.Add(new InstructionModel(counter++, SelectedFunction, TargetRegistry.ToUpper(), SourceRegistry01.ToUpper(), SourceRegistry02.ToUpper()));
                EmptyProperties();
            }, () => { return SelectedFunction != null && !string.IsNullOrWhiteSpace(TargetRegistry) && !string.IsNullOrWhiteSpace(SourceRegistry01) && !string.IsNullOrWhiteSpace(SourceRegistry02); }).ObservesProperty(() => SelectedFunction)
                                                                                                                                                                                                         .ObservesProperty(() => TargetRegistry)
                                                                                                                                                                                                         .ObservesProperty(() => SourceRegistry01)
                                                                                                                                                                                                         .ObservesProperty(() => SourceRegistry02);
            DeleteItemCommand = new DelegateCommand(() =>
            {
                ReOrderAfterDelete(SelectedInstruction.ID);
                Instructions.Remove(SelectedInstruction);
            }, () => { return SelectedInstruction != null; }).ObservesProperty(() => SelectedInstruction);
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Empties the properties after adding a new instruction
        /// </summary>
        private void EmptyProperties()
        {
            SelectedFunction = null;
            TargetRegistry = null;
            SourceRegistry01 = null;
            SourceRegistry02 = null;
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
