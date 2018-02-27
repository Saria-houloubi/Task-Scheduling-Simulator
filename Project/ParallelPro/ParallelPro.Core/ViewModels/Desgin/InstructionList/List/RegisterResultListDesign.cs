using System;
using System.Collections.Generic;
using ThishreenUniversity.ParallelPro.Enums;
using Tishreen.ParallelPro.Core.Models;

namespace Tishreen.ParallelPro.Core.ViewModels.Design
{
    /// <summary>
    /// A Design time data class
    /// </summary>
   public class RegisterResultListDesign
    {
        #region Singleton

        /// <summary>
        /// A singletone property which we will bind to
        /// </summary>
        public static RegisterResultListDesign Instance { get { return new RegisterResultListDesign(); } }
        #endregion

        #region Properties
        /// <summary>
        /// The list that holds all the registers that the user writes
        /// </summary>
        public List<RegisterResultModel> Registers { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public RegisterResultListDesign()
        {
            //Create and fill the list
            Registers = new List<RegisterResultModel>();

            var registerNames = Enum.GetValues(typeof(RegisteriesAndMemory));

            foreach (var item in registerNames)
            {
                var stringValue = Enum.GetName(typeof(RegisteriesAndMemory), item);
                //If it is a registery spot add it to target
                if ((int)item < 31)
                    Registers.Add(new RegisterResultModel(stringValue));
            }
        }
            #endregion
    }
}
