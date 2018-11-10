using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using ThishreenUniversity.ParallelPro.Enums;

namespace Tishreen.ParallelPro.Core.Models.LoopUnrol
{
    /// <summary>
    /// The instructions for the loop unroling algorithem
    /// </summary>
    public class LoopUnrolInstructionModel : BindableBase
    {
        #region Properties
        public int? Order { get; set; }
        /// <summary>
        /// The execution Clock cycles for the selected operation
        /// </summary>
        public int ExecutionClockCycles { get; set; }
        private string mOperation;
        public string Operation
        {
            get { return mOperation; }
            set
            {
                SetProperty(ref mOperation, value);

                if (!string.IsNullOrEmpty(value))
                {
                    if (Enum.TryParse(value, out FunctionsTypes result))
                    {
                        ///Fill the target and source registeries with the right values
                        FillTargetAndSourceRegisteries(result);
                    }
                }
            }
        }
        private string mTargetRegistery;
        public string TargetRegistery
        {
            get { return mTargetRegistery; }
            set { SetProperty(ref mTargetRegistery, value); }
        }
        private string mSourceRegistery01;
        public string SourceRegistery01
        {
            get { return mSourceRegistery01; }
            set { SetProperty(ref mSourceRegistery01, value); }
        }
        private string mSourceRegistery02;
        public string SourceRegistery02
        {
            get { return mSourceRegistery02; }
            set { SetProperty(ref mSourceRegistery02, value); }
        }
        /// <summary>
        /// An immediate value to use for add,sub ...
        /// </summary>
        private int? mImmediateValueOrDisplacmnet;
        public int? ImmediateValueOrDisplacmnet
        {
            get { return mImmediateValueOrDisplacmnet; }
            set { SetProperty(ref mImmediateValueOrDisplacmnet, value); }
        }

        public string DisplacmentStringFormat => Operation == FunctionsTypes.LD.ToString() ? $"-{ImmediateValueOrDisplacmnet}({SourceRegistery01})" : Operation == FunctionsTypes.SD.ToString() ? $"-{ImmediateValueOrDisplacmnet}({TargetRegistery})" : $"{ImmediateValueOrDisplacmnet}";
        /// <summary>
        /// A flag represents if the user can choose another source for the instruction
        /// like in SD/LD only one source
        /// </summary>
        protected bool mCanChooseSource02 = true;
        public bool CanChooseSource02
        {
            get { return mCanChooseSource02; }
            set { SetProperty(ref mCanChooseSource02, value); }
        }

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public LoopUnrolInstructionModel()
        {
            FillFunctionList();
        }
        #endregion
        #region Lists
        /// <summary>
        /// The function list that we have
        /// </summary>
        private ObservableCollection<FunctionsTypes> mFunctions;
        public ObservableCollection<FunctionsTypes> Functions
        {
            get { return mFunctions; }
            set { SetProperty(ref mFunctions, value); }
        }
        private ObservableCollection<string> mTargetRegisteries;
        public ObservableCollection<string> TargetRegisters
        {
            get { return mTargetRegisteries; }
            set { SetProperty(ref mTargetRegisteries, value); }
        }
        private ObservableCollection<string> mSourceRegisteries;
        public ObservableCollection<string> SourceRegisteries
        {
            get { return mSourceRegisteries; }
            set { SetProperty(ref mSourceRegisteries, value); }
        }

        #endregion

        #region Helpers


        /// <summary>
        /// Fill the functions list with the type of functions that we support
        /// </summary>
        protected void FillFunctionList()
        {
            Functions = new ObservableCollection<FunctionsTypes>();

            //Loops thru the enum values an add them to the functions list
            foreach (var item in Enum.GetValues(typeof(FunctionsTypes)))
            {
                Functions.Add((FunctionsTypes)item);
            }

            TargetRegisters = null;
            SourceRegistery01 = null;
            SourceRegistery02 = null;
        }

        /// <summary>
        /// Fill the target and the source registeries or memory that the user can choose
        /// </summary>
        /// <param name="function">The function that we want to restrict some registery or memory access</param>
        /// <param name="editCollection">If true will update the edit collections</param>
        protected void FillTargetAndSourceRegisteries(FunctionsTypes function)
        {
            UpdateListOnFunctionChange(ref mTargetRegisteries, ref mSourceRegisteries, ref mCanChooseSource02, function);
            RaisePropertyChanged(nameof(TargetRegisters));
            RaisePropertyChanged(nameof(SourceRegisteries));
            RaisePropertyChanged(nameof(CanChooseSource02));
        }
        /// <summary>
        /// Updates the lists sent to it based on the value of the function that is sent to it
        /// </summary>
        /// <param name="targetRegistries"></param>
        /// <param name="sourceRegistries"></param>
        /// <param name="canChooseSource02"></param>
        /// <param name="function"></param>
        protected void UpdateListOnFunctionChange(
             ref ObservableCollection<string> targetRegistries,
             ref ObservableCollection<string> sourceRegistries,
             ref bool canChooseSource02,
            FunctionsTypes function)
        {
            targetRegistries = new ObservableCollection<string>();
            sourceRegistries = new ObservableCollection<string>();

            var RegisteryAndMemoryList = Enum.GetValues(typeof(RegisteriesAndMemory));

            foreach (var item in RegisteryAndMemoryList)
            {
                var stringValue = Enum.GetName(typeof(RegisteriesAndMemory), item);
                if (function == FunctionsTypes.LD)
                {
                    //If it is a registery spot add it to target
                    if ((int)item < 31)
                    {
                        targetRegistries.Add(stringValue);
                    }
                    else
                    {
                        sourceRegistries.Add(stringValue);
                    }
                }
                else if (function == FunctionsTypes.SD)
                {
                    //If it is a memory add it to target
                    if ((int)item >= 31)
                    {
                        targetRegistries.Add(stringValue);
                    }
                    else
                    {
                        sourceRegistries.Add(stringValue);
                    }
                }
                else
                {
                    targetRegistries.Add(stringValue);
                    //If it is a registery spot add it to target
                    if ((int)item < 31)
                    {
                        sourceRegistries.Add(stringValue);
                    }
                }
            }
            //Disable source02 if the function is either load or store
            if (function == FunctionsTypes.LD || function == FunctionsTypes.SD || function == FunctionsTypes.ADDi || function == FunctionsTypes.SUBi)
            {
                canChooseSource02 = false;
            }
            else
            {

                canChooseSource02 = true;
            }
            RaisePropertyChanged(nameof(CanChooseSource02));
        }
        #endregion

    }
}
