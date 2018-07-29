
using System.Collections.Generic;

namespace Tishreen.ParallelPro.Core.Models
{
    /// <summary>
    /// A model for the name and the operation that is working on a register
    /// </summary>
    public class RegisterResultModel : BaseModel
    {
        #region Properties
        /// <summary>
        /// The id of the instruction that it is working on it
        /// </summary>
        public int WorkingInstructionID { get; set; }
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
        /// only the last instruction will be the on to write back
        /// </summary>
        private Dictionary<int,bool> mInstructionReservedRegiseter;
        public Dictionary<int, bool> InstructionReservedRegiseter
        {
            get { return mInstructionReservedRegiseter; }
            set { SetProperty(ref mInstructionReservedRegiseter, value); }
        }
        #endregion

        #region Constructers
        /// <summary>
        /// Called on the create of the object 
        /// Creates the list 
        /// </summary>
        private void OnCreate()
        {
            //Create the list
            InstructionReservedRegiseter = new Dictionary<int, bool>();
        }
        /// <summary>
        /// Default Constructer
        /// </summary>
        public RegisterResultModel() {
            OnCreate();
        }
        
        /// <summary>
        /// Constructer to initialize the properties
        /// </summary>
        /// <param name="name">The name of the register</param>
        /// <param name="operation" >The opreaiton that is working on the register</param>
        public RegisterResultModel(string name,string operation = null)
        {
            OnCreate();
            this.Name = name;
            this.Operation = operation;
        }
        #endregion
    }
}
