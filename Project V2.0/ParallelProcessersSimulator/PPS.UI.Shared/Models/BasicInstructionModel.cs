using PPS.UI.Shared.Enums;

namespace PPS.UI.Shared.Models
{
    /// <summary>
    /// The basic instruction model for the scoreboard and tomasulo algorithm
    /// </summary>
    public class BasicInstructionModel
    {

        #region Properties
        public BasicFunctions Function { get; set; }
        public string Target { get; set; }
        public string Source1 { get; set; }
        public string Source2 { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public BasicInstructionModel()
        {

        }
        #endregion
    }
}
