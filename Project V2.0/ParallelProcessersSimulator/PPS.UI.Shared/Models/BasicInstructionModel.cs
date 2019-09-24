using PPS.UI.Shared.Enums;
using PPS.UI.Shared.Models.Base;
using System;

namespace PPS.UI.Shared.Models
{
    /// <summary>
    /// The basic instruction model for the scoreboard and tomasulo algorithm
    /// </summary>
    public class BasicInstructionModel : BaseNotifyModel
    {

        #region Properties
        /// <summary>
        /// The unique id of the instruction
        /// </summary>
        public Guid Id { get; set; }
        public BasicFunctions Function { get; set; }
        private string _Target;

        public string Target
        {
            get { return _Target; }
            set { SetProperty(ref _Target, value); }
        }

        private string _Source1;

        public string Source1
        {
            get { return _Source1; }
            set { SetProperty(ref _Source1, value); }
        }

        private string _Source2;

        public string Source2
        {
            get { return _Source2; }
            set { SetProperty(ref _Source2, value); }
        }

        public bool LastIssued { get; set; }
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
            Id = Guid.NewGuid();
        }

        #endregion

        #region Helpers
        /// <summary>
        /// Clears the clock cycles for the instruction
        /// </summary>
        public void ClearCycles()
        {
            Issue = null;
            Read = null;
            Executed = null;
            WriteBack = null;
        }
        #endregion
    }
}
