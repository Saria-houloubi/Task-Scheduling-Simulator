using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tishreen.ParallelPro.Core.Models;

namespace Tishreen.ParallelPro.Core
{
    /// <summary>
    /// A model holds the exam results for each student
    /// </summary>
    public class ExamResultModel
    {
        #region Properties
        /// <summary>
        /// The order number of the exam
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// The name of the algorithm that the student is on
        /// </summary>
        public string AlgorithmName { get; set; }
        /// <summary>
        /// The full mark on the exam
        /// </summary>
        public int FullMark { get; set; }
        /// <summary>
        /// The student mark on this exam
        /// </summary>
        public float StudentMark { get; set; }
        /// <summary>
        /// The student mark in %
        /// </summary>
        public int StudentMarkPercentage { get; set; }
        /// <summary>
        /// The clock cycle the student choose for the exam
        /// </summary>
        public int ChoosenClockCycle { get; set; }
        /// <summary>
        /// The instructions that the student entered at start of the exam
        /// </summary>
        public List<InstructionModel> StudentEnteredInstruction { get; set; }
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public ExamResultModel()
        {
            StudentEnteredInstruction = new List<InstructionModel>();
        } 
        #endregion
    }
}
