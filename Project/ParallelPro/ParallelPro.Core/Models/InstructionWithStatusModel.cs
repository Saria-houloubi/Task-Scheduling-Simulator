namespace Tishreen.ParallelPro.Core.Models
{
    /// <summary>
    /// A model for all the instruction with there status record
    /// Funtion TargetRegistry  Source1 Source2 issue Fitched Executed WriteResult
    /// </summary>
    public class InstructionWithStatusModel : BaseModel
    {
        #region Properties
        /// <summary>
        /// The order ID of the instruction
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The function name of the instruction
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The target registry to store the value in
        /// </summary>
        public string TargetRegistery { get; set; }
        /// <summary>
        /// The first source registry or value to get from
        /// </summary>
        public string SourceRegistery01 { get; set; }
        /// <summary>
        /// The second source registry or value to get from
        /// </summary>
        public string SourceRegistery02 { get; set; }
        /// <summary>
        /// The clock cycle when this operation was issued
        /// </summary>
        private int? _issueCycle;
        public int? IssueCycle
        {
            get { return _issueCycle; }
            set { SetProperty(ref _issueCycle, value); }
        }
        /// <summary>
        /// The clock cylce when the operation was read
        /// </summary>
        private int? _readCycle;
        public int? ReadCycle
        {
            get { return _readCycle; }
            set { SetProperty(ref _readCycle, value); }
        }
        /// <summary>
        /// The clock cycle when the execute started
        /// </summary>
        private int? _executeCompletedCycle;
        public int? ExecuteCompletedCycle
        {
            get { return _executeCompletedCycle; }
            set { SetProperty(ref _executeCompletedCycle, value); }
        }
        /// <summary>
        /// The clock cycle that the operation will write the result back
        /// </summary>
        private int? _writeBackCycle;
        public int? WriteBackCycle
        {
            get { return _writeBackCycle; }
            set { SetProperty(ref _writeBackCycle, value); }
        }
        #endregion

        #region Constructers
        /// <summary>
        /// Default Constructer
        /// </summary>
        public InstructionWithStatusModel() { }

        /// <summary>
        /// Constructer to initialize the properties
        /// </summary>
        /// <param name="id">The order ID of the instruction that is shown to the user</param>
        /// <param name="name">The name of the operation</param>
        /// <param name="targetRegistry">The target registry to store the value in</param>
        /// <param name="sourceRegistry01">The first source registry to get the value from</param>
        /// <param name="sourceRegistery02">The second source registry to get the value from</param>
        /// <param name="issueCycle">The Clock cycle that the operation was issued or summend</param>
        /// <param name="readCycle">The clock cylce that the operation was read on</param>
        /// <param name="executeCycle">The Clock cycle that the operation started executing</param>
        /// <param name="writeBackCycle">The clock cycle that the operation finished executing and is writing the result back</param>
        public InstructionWithStatusModel(int id, string name, string targetRegistry, string sourceRegistry01, string sourceRegistery02 = null, int? issueCycle = null, int? readCycle = null, int? executeCycle = null, int? writeBackCycle = null)
        {
            this.ID = id;
            this.Name = name;
            this.TargetRegistery= targetRegistry;
            this.SourceRegistery01= sourceRegistry01;
            this.SourceRegistery02= sourceRegistery02;
            this.IssueCycle = issueCycle;
            this.ReadCycle = readCycle;
            this.ExecuteCompletedCycle = executeCycle;
            this.WriteBackCycle = writeBackCycle;
        }
        #endregion
    }
}
