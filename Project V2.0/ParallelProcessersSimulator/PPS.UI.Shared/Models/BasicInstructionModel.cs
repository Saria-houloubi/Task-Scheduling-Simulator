using PPS.UI.Shared.Enums;
using PPS.UI.Shared.Models.Base;

namespace PPS.UI.Shared.Models
{
    /// <summary>
    /// The basic instruction model for the scoreboard and tomasulo algorithm
    /// </summary>
    public class BasicInstructionModel : BaseNotifyModel
    {

        #region Properties
        public BasicFunctions Function { get; set; }
        public string Target { get; set; }
        public string Source1 { get; set; }
        public string Source2 { get; set; }

        /// <summary>
        /// The clock the instuctions has been issued
        /// </summary>
        private int? _Issue;

        public int? Issue
        {
            get { return _Issue; }
            set { SetProperty(ref _Issue, value); }
        }

        /// <summary>
        /// The clock the instuctions has been read
        /// </summary>
        private int? _Read;

        public int? Read
        {
            get { return _Read; }
            set { SetProperty(ref _Read, value); }
        }

        /// <summary>
        /// The clock the instuctions has done executeing
        /// </summary>
        private int? _Executed;

        public int? Executed
        {
            get { return _Executed; }
            set { SetProperty(ref _Executed, value); }
        }
        /// <summary>
        /// The clock the instuctions has wrote back
        /// </summary>
        private int? _WriteBack;

        public int? WriteBack
        {
            get { return _WriteBack; }
            set { SetProperty(ref _WriteBack, value); }
        }
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
