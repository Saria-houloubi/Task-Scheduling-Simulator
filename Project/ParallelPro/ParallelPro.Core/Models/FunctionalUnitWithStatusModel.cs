using ThishreenUniversity.ParallelPro.Enums;
namespace Tishreen.ParallelPro.Core.Models
{
    /// <summary>
    /// A model for the functional unit with its status as for busy and the target and Source so on.....
    /// </summary>
    public class FunctionalUnitWithStatusModel : BaseModel
    {
        #region Properties
        /// <summary>
        /// The order ID of the functional unit
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The amount of clock cycles that is still needs
        /// </summary>
        private int? _time;
        public int? Time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); }
        }
        /// <summary>
        /// The function name of the functional unit
        /// </summary>
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        /// <summary>
        /// A flag which represents if the functional unit is busy or can take an operation
        /// </summary>
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
        /// <summary>
        /// The operation that it is working with 
        /// <see cref="FunctionsTypes"/>
        /// </summary>
        private string _operation;
        public string Operation
        {
            get { return _operation; }
            set { SetProperty(ref _operation, value); }
        }
        /// <summary>
        /// The target   registry to store the value in
        /// (Dest Fi)
        /// </summary>
        private string _targerRegistery;
        public string TargetRegistery
        {
            get { return _targerRegistery; }
            set { SetProperty(ref _targerRegistery, value); }
        }
        /// <summary>
        /// The first source registry or value to get from
        /// (Src Fj)
        /// </summary>
        private string _sourceRegistery01;
        public string SourceRegistery01
        {
            get { return _sourceRegistery01; }
            set { SetProperty(ref _sourceRegistery01, value); }
        }
        /// <summary>
        /// The second source registry or value to get from
        /// (Src Fk)
        /// </summary>
        private string _sourceRegistery02;
        public string SourceRegistery02
        {
            get { return _sourceRegistery02; }
            set { SetProperty(ref _sourceRegistery02, value); }
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
        private string _isSource01Ready;
        public string IsSource01Ready
        {
            get { return _isSource01Ready; }
            set { SetProperty(ref _isSource01Ready, value); }
        }
        /// <summary>
        /// A flag shows if the Source02 is ready or not
        /// </summary>
        private string _isSource02Ready;
        public string IsSource02Ready
        {
            get { return _isSource02Ready; }
            set { SetProperty(ref _isSource02Ready, value); }
        }
        /// <summary>
        /// The type of the functions
        /// </summary>
        private FunctionsTypes _function;
        public FunctionsTypes Function
        {
            get { return _function; }
            set { SetProperty(ref _function, value); }
        }
        /// <summary>
        /// The id of the instruction that the functional unit is working with
        /// </summary>
        public int WorkingInstructionID { get; set; }
        /// <summary>
        /// A flag represents that the unit just got freed and can not yet take an instruction unitl the next cycle
        /// </summary>
        public bool JustFreedUp { get; set; }
        #endregion

        #region Constructers
        /// <summary>
        /// Default Constructer
        /// </summary>
        public FunctionalUnitWithStatusModel() { }

        /// <summary>
        /// Constructer to initialize the properties
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">The name of the Functional unit</param>
        public FunctionalUnitWithStatusModel(int id, string name,FunctionsTypes functions)
        {
            this.ID = id;
            this.Name = name;
            this.Function = functions;
        }
        #endregion
    }
}
