
using ThishreenUniversity.ParallelPro.Enums;

namespace Tishreen.ParallelPro.Core.Models
{
    /// <summary>
    /// A model for all the instruction
    /// Funtion TargetRegistry  Source1 Source2
    /// </summary>
    public class InstructionModel 
    {
        #region Properties
        /// <summary>
        /// The order ID of the instruction
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The function name of the instruction
        /// </summary>
        public FunctionsTypes Name { get; set; }
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
        #endregion

        #region Constructers

        /// <summary>
        /// Default Constructer
        /// </summary>
        public InstructionModel() { }
        
        /// <summary>
        /// Constructer to initialize the properties
        /// </summary>
        /// <param name="id">The order ID of the instruction that is shown to the user</param>
        /// <param name="name">The name of the operation</param>
        /// <param name="targetRegistry">The target registry to store the value in</param>
        /// <param name="sourceRegistry01">The first source registry to get the value from</param>
        /// <param name="SourceRegistery02">The second source registry to get the value from</param>
        public InstructionModel(int id, FunctionsTypes name,string targetRegistry,string sourceRegistry01,string SourceRegistery02 = null)
        {
            this.ID = id;
            this.Name = name;
            this.TargetRegistery= targetRegistry;
            this.SourceRegistery01= sourceRegistry01;
            this.SourceRegistery02= SourceRegistery02;
        }
        #endregion
    }
}
