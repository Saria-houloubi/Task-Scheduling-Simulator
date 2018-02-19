using System.Collections.Generic;
using Tishreen.ParallelPro.Core.Models;

namespace Tishreen.ParallelPro.Core.ViewModels.Design
{
    /// <summary>
    /// A Design time data class
    /// </summary>
   public class FunctionalUnitsWithStatusListDesign
    {
        #region Singleton

        /// <summary>
        /// A singletone property which we will bind to
        /// </summary>
        public static FunctionalUnitsWithStatusListDesign Instance { get { return new FunctionalUnitsWithStatusListDesign(); } }
        #endregion

        #region Properties
        /// <summary>
        /// The list that holds all the functional units that the user writes
        /// </summary>
        public List<FunctionalUnitWithStatusModel> Functions{ get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public FunctionalUnitsWithStatusListDesign()
        {
            //Added a counter so we do not add the numbers manule
            int couter = 1;

            //Create and fill the list
            Functions = new List<FunctionalUnitWithStatusModel>()
            {
                new FunctionalUnitWithStatusModel(couter++,"LD"),
                new FunctionalUnitWithStatusModel(couter++,"Mult 1"),
                new FunctionalUnitWithStatusModel(couter++,"Mult 2"),
                new FunctionalUnitWithStatusModel(couter++,"Add"),
                new FunctionalUnitWithStatusModel(couter++,"Divide"),
            };
        }

        #endregion
    }
}
