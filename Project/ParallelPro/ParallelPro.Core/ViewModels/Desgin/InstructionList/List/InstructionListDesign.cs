using ParallelPro.Core.Models;
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
                new InstructionModel(couter++,"LD","F2","+34","R3"),
                new InstructionModel(couter++,"ADD","F2","F1","F0"),
                new InstructionModel(couter++,"SUB","F4","F1","F0"),
                new InstructionModel(couter++,"MULT","F9","F8","F0"),
                new InstructionModel(couter++,"LD","F2","+34","R3"),
                new InstructionModel(couter++,"ADD","F2","F1","F0"),
                new InstructionModel(couter++,"SUB","F4","F1","F0"),
                new InstructionModel(couter++,"MULT","F9","F8","F0"),
                new InstructionModel(couter++,"LD","F2","+34","R3"),
                new InstructionModel(couter++,"ADD","F2","F1","F0"),
                new InstructionModel(couter++,"SUB","F4","F1","F0"),
                new InstructionModel(couter++,"MULT","F9","F8","F0"),
            };
        }

        #endregion
    }
}
