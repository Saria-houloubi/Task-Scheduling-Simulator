using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPro.Core.ViewModels.Desgin
{
    /// <summary>
    /// A desgin time data class
    /// </summary>
   public class InstructionItemDesign
    {
        #region Singleton

        /// <summary>
        /// A singletone property which we will bind to
        /// </summary>
        public static InstructionItemDesign Instance { get { return new InstructionItemDesign(); } }
        #endregion

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

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public InstructionItemDesign()
        {
            this.ID = 1;
            this.Name = "ADD";
            this.TargetRegistry = "F7";
            this.SourceRegistry01 = "F1";
            this.SourceRegistry02 = "F0";
        }

        #endregion
    }
}
