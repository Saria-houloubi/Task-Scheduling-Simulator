using System.Collections.Generic;
using ThishreenUniversity.ParallelPro.Enums;
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
        public List<FunctionalUnitWithStatusModel> FunctionalUnits { get; set; }
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
            FunctionalUnits = new List<FunctionalUnitWithStatusModel>()
            {
                new FunctionalUnitWithStatusModel(couter++,"Load",FunctionsTypes.LD),
                new FunctionalUnitWithStatusModel(couter++,"Mult 1",FunctionsTypes.MULT),
                new FunctionalUnitWithStatusModel(couter++,"Mult 2",FunctionsTypes.MULT),
                new FunctionalUnitWithStatusModel(couter++,"Add",FunctionsTypes.ADD),
                new FunctionalUnitWithStatusModel(couter++,"Divide",FunctionsTypes.DIV),
            };
        }

        #endregion
    }
}
