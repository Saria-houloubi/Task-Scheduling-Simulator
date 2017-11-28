
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
        public string Name { get; set; }
        /// <summary>
        /// The target registry to store the value in
        /// </summary>
        public string TargetRegistry { get; set; }
        /// <summary>
        /// The first source registry or value to get from
        /// </summary>
        public string SourceRegistry01 { get; set; }
        /// <summary>
        /// The second source registry or value to get from
        /// </summary>
        public string SourceRegistry02 { get; set; }
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
        /// <param name="sourceRegistry02">The second source registry to get the value from</param>
        public InstructionModel(int id,string name,string targetRegistry,string sourceRegistry01,string sourceRegistry02)
        {
            this.ID = id;
            this.Name = name;
            this.TargetRegistry= targetRegistry;
            this.SourceRegistry01= sourceRegistry01;
            this.SourceRegistry02= sourceRegistry02;
        }
        #endregion
    }
}
