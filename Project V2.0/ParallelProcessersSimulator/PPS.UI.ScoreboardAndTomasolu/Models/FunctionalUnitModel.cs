using PPS.UI.Shared.Enums;
using PPS.UI.Shared.Models.Base;
using System;
using System.Collections.Generic;

namespace PPS.UI.ScoreboardAndTomasolu.Models
{
    /// <summary>
    /// The model for the functional unit with status
    /// </summary>
    public class FunctionalUnitModel : BaseNotifyModel
    {

        #region Properties
        /// <summary>
        /// The unique id for the unit
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// The status of the unit if it can take a new instruction
        /// </summary>
        private UnitStatus _Status;

        public UnitStatus Status
        {
            get { return _Status; }
            set { SetProperty(ref _Status, value); }
        }
        /// <summary>
        /// The operation or instruction the unit is prosssing
        /// </summary>
        private BasicFunctions? _Operation;

        public BasicFunctions? Operation
        {
            get { return _Operation; }
            set { SetProperty(ref _Operation, value); }
        }
        /// <summary>
        /// The time or clock-cycle count until this unit is done executing
        /// </summary>
        private int? _Time;

        public int? Time
        {
            get { return _Time; }
            set { SetProperty(ref _Time, value); }
        }

        /// <summary>
        /// The target   registry to store the value in
        /// (Dest Fi)
        /// </summary>
        private string _Target;
        public string Target
        {
            get { return _Target; }
            set { SetProperty(ref _Target, value); }
        }
        /// <summary>
        /// The first source registry or value to get from
        /// (Src Fj)
        /// </summary>
        private string _Source1;
        public string Source1
        {
            get { return _Source1; }
            set { SetProperty(ref _Source1, value); }
        }
        //The soruces will be saved here until they are ready 
        public string _Source1Local { get; set; }
        public string _Source2Local { get; set; }
        /// <summary>
        /// The second source registry or value to get from
        /// (Src Fk)
        /// </summary>
        private string _Source2;
        public string Source2
        {
            get { return _Source2; }
            set { SetProperty(ref _Source2, value); }
        }
        /// <summary>
        /// If Sourceregistery01 is not ready and is the output of an operation that we have 
        /// it will hold what functional unit is doing the operation unitl it is done
        /// and then gets the value
        /// </summary>
        private string _waitingOperationForSource01;
        public string WaitingOperationForSource01
        {
            get { return _waitingOperationForSource01; }
            set { SetProperty(ref _waitingOperationForSource01, value); }
        }
        /// <summary>
        /// If Sourceregistery02 is not ready and is the output of an operation that we have 
        /// it will hold what functional unit is doing the operation unitl it is done
        /// and then gets the value
        /// </summary>
        private string _waitingOperationForSource02;
        public string WaitingOperationForSource02
        {
            get { return _waitingOperationForSource02; }
            set { SetProperty(ref _waitingOperationForSource02, value); }
        }
        /// <summary>
        /// A flag shows if the Source01 is ready or not
        /// </summary>
        private bool _isSource01Ready;
        public bool IsSource01Ready
        {
            get { return _isSource01Ready; }
            set { SetProperty(ref _isSource01Ready, value); }
        }
        /// <summary>
        /// A flag shows if the Source02 is ready or not
        /// </summary>
        private bool _isSource02Ready;
        public bool IsSource02Ready
        {
            get { return _isSource02Ready; }
            set { SetProperty(ref _isSource02Ready, value); }
        }
        /// <summary>
        /// The id of the instruction that the functional unit is working with
        /// </summary>
        public Guid WorkingInstructionID { get; set; }
        /// <summary>
        /// A flag represents that the unit just got freed and can not yet take an instruction unitl the next cycle
        /// </summary>
        public bool JustFreedUp { get; set; }
        /// <summary>
        /// The type of the functions the unit supports
        /// </summary>
        private Dictionary<BasicFunctions, bool> _Functions;
        public Dictionary<BasicFunctions, bool> Functions
        {
            get { return _Functions; }
            set { SetProperty(ref _Functions, value); }
        }
        #endregion

        #region Constructer 
        /// <summary>
        /// Default constructers
        /// </summary>
        public FunctionalUnitModel()
        {

        }
        /// <summary>
        /// Constructer to initialize the properties
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">The name of the Functional unit</param>
        public FunctionalUnitModel(int id, string name, Dictionary<BasicFunctions, bool> functions)
        {
            this.Id = id;
            this.Name = name;
            this.Functions = functions;
        }
        #endregion

        #region Methods

        public bool IsBusy() => Status == UnitStatus.Busy;
        #endregion
    }
}
