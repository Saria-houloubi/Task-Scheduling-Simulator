using System.Collections.Generic;
using Tishreen.ParallelPro.Core.Models;

namespace Tishreen.ParallelPro.Core.ViewModels.Design
{
    /// <summary>
    /// A Design time data class
    /// </summary>
   public class InstructionWithStatusListDesign
    {
        #region Singleton

        /// <summary>
        /// A singletone property which we will bind to
        /// </summary>
        public static InstructionWithStatusListDesign Instance { get { return new InstructionWithStatusListDesign(); } }
        #endregion

        #region Properties
        /// <summary>
        /// The list that holds all the instructions that the user writes
        /// </summary>
        public List<InstructionWithStatusModel> Instructions { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public InstructionWithStatusListDesign()
        {
            //Added a counter so we do not add the numbers manule
            int couter = 1;

            //Create and fill the list
            Instructions = new List<InstructionWithStatusModel>()
            {
                new InstructionWithStatusModel(couter++,"LD","F2","+34","R3",1,2,3,4),
                new InstructionWithStatusModel(couter++,"ADD","F2","F1","F0",5,6,7,8),
                new InstructionWithStatusModel(couter++,"SUB","F4","F1","F0",9,10,11,12),
                new InstructionWithStatusModel(couter++,"MULT","F9","F8","F0",13,14,15,16),
                new InstructionWithStatusModel(couter++,"LD","F2","+34","R3",17,18,19,20),
                new InstructionWithStatusModel(couter++,"ADD","F2","F1","F0",21,22,23,24),
                new InstructionWithStatusModel(couter++,"SUB","F4","F1","F0",25,26,27,28),
                new InstructionWithStatusModel(couter++,"MULT","F9","F8","F0",29,30,31,32),
                new InstructionWithStatusModel(couter++,"LD","F2","+34","R3",33,34,35,36),
            };
        }

        #endregion
    }
}
