using System.Collections.Generic;
using Tishreen.ParallelPro.Core.Models;

namespace Tishreen.ParallelPro.Core.ViewModels.Design
{
    /// <summary>
    /// A Design time data class
    /// </summary>
   public class RegisterResultItemDesign : RegisterResultModel
    {
        #region Singleton

        /// <summary>
        /// A singletone property which we will bind to
        /// </summary>
        public static RegisterResultItemDesign Instance => new RegisterResultItemDesign();
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public RegisterResultItemDesign()
        {
            this.Name = "F1";
            this.Operation = "Add";
        }
        #endregion
    }
}
