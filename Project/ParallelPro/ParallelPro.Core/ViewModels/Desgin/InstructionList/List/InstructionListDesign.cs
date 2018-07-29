using System.Collections.Generic;
using Tishreen.ParallelPro.Core.Models;

namespace Tishreen.ParallelPro.Core.ViewModels.Design
{
    /// <summary>
    /// A Design time data class
    /// </summary>
   public class InstructionListDesign
    {
        #region Singleton

        /// <summary>
        /// A singletone property which we will bind to
        /// </summary>
        public static InstructionListDesign Instance { get { return new InstructionListDesign(); } }
        #endregion

        #region Properties
        /// <summary>
        /// The list that holds all the instructions that the user writes
        /// </summary>
        public List<InstructionModel> Instructions { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public InstructionListDesign()
        {
            //Added a counter so we do not add the numbers manule
            int couter = 1;

            //Create and fill the list
            Instructions = new List<InstructionModel>()
            {
                new InstructionModel(couter++,ThishreenUniversity.ParallelPro.Enums.FunctionsTypes.LD,"F2","+34","R3"),
                new InstructionModel(couter++,ThishreenUniversity.ParallelPro.Enums.FunctionsTypes.LD,"F2","+34","R3"),
                new InstructionModel(couter++,ThishreenUniversity.ParallelPro.Enums.FunctionsTypes.LD,"F2","+34","R3"),
                new InstructionModel(couter++,ThishreenUniversity.ParallelPro.Enums.FunctionsTypes.LD,"F2","+34","R3"),
                new InstructionModel(couter++,ThishreenUniversity.ParallelPro.Enums.FunctionsTypes.LD,"F2","+34","R3"),
                new InstructionModel(couter++,ThishreenUniversity.ParallelPro.Enums.FunctionsTypes.LD,"F2","+34","R3"),
            };
        }

        #endregion
    }
}
