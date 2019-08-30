using PPS.UI.Shared.Models;
using PPS.UI.Shared.Models.Base;
using System.Collections.Generic;

namespace PPS.UI.ScoreboardAndTomasolu.Models
{
    /// <summary>
    /// The model for the defferent registers
    /// </summary>
    public class RegisterModel : BaseNotifyModel
    {
        #region Properties
        /// <summary>
        /// The name of the register 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The operation that is working on the register
        /// </summary>
        private string _operation;
        public string Operation
        {
            get { return _operation; }
            set { SetProperty(ref _operation, value); }
        }
        /// <summary>
        /// A flag which will indecate if the regisert value can be used
        /// </summary>
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
        /// <summary>
        /// The list of instruction that reserved this regiseter unit
        /// And if the have the permision to wirte in t
        /// </summary>
        public List<BasicInstructionModel> InstructionReservedRegiseter { get; set; }
        #endregion
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public RegisterModel()
        {
            InstructionReservedRegiseter = new List<BasicInstructionModel>();

        }
        #endregion
    }
}
