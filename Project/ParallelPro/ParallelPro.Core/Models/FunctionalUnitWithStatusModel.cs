using ThishreenUniversity.ParallelPro.Enums;
namespace Tishreen.ParallelPro.Core.Models
{
    /// <summary>
    /// A model for the functional unit with its status as for busy and the target and sorce so on.....
    /// </summary>
    public class FunctionalUnitWithStatusModel
    {
        #region Properties
        /// <summary>
        /// The order ID of the functional unit
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The amount of clock cycles that is still needs
        /// </summary>
        public int? Time { get; set; }
        /// <summary>
        /// The function name of the functional unit
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A flag which represents if the functional unit is busy or can take an operation
        /// </summary>
        public bool IsBusy { get; set; } = false;
        /// <summary>
        /// The operation that it is working with 
        /// <see cref="FunctionsTypes"/>
        /// </summary>
        public string Operation { get; set; }
        /// <summary>
        /// The target   registry to store the value in
        /// (Dest Fi)
        /// </summary>
        public string TargetRegistery { get; set; }
        /// <summary>
        /// The first source registry or value to get from
        /// (Src Fj)
        /// </summary>
        public string SourceRegistery01 { get; set; }
        /// <summary>
        /// The second source registry or value to get from
        /// (Src Fk)
        /// </summary>
        public string SourceRegistery02 { get; set; }
        /// <summary>
        /// If sorceregistery01 is not ready and is the output of an operation that we have 
        /// it will hold what functional unit is doing the operation unitl it is done
        /// and then gets the value
        /// </summary>
        public string WaitingOperationForSorce01 { get; set; }
        /// <summary>
        /// If sorceregistery02 is not ready and is the output of an operation that we have 
        /// it will hold what functional unit is doing the operation unitl it is done
        /// and then gets the value
        /// </summary>
        public string WaitingOperationForSorce02 { get; set; }
        /// <summary>
        /// A flag shows if the sorce01 is ready or not
        /// </summary>
        public string IsSorce01Ready { get; set; }
        /// <summary>
        /// A flag shows if the sorce02 is ready or not
        /// </summary>
        public string IsSorce02Ready { get; set; }
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
        public FunctionalUnitWithStatusModel(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
        #endregion
    }
}
