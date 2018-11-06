namespace Tishreen.ParallelPro.Core.Models
{
    /// <summary>
    /// Holds the instruction infromation when it reserves an registery
    /// </summary>
    public class InstructionRegisterReservationModel
    {

        #region Properties
        /// <summary>
        /// The instruction that reserved the registery
        /// </summary>
        public int InstructionId { get; set; }
        /// <summary>
        /// If it was last instruction issued 
        /// </summary>
        public bool LastIssued { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public InstructionRegisterReservationModel()
        {

        }
        #endregion
    }

}
