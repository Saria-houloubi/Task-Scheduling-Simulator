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
        public string StudentMarkPercentage => $"{(int)StudentMark * 100 / FullMark}%";
        /// <summary>
        /// The clock cycle the student choose for the exam
        /// </summary>
        public int ChoosenClockCycle { get; set; }
        /// <summary>
        /// The instructions that the student entered at start of the exam
        /// </summary>
        public List<InstructionModel> Instructions { get; set; }
        /// <summary>
        /// The amount of integer units that can execute functions like Load(LD) Store(SD)
        /// </summary>
        public  int NumberOfLoadUnits { get; set; }
        /// <summary>
        /// The amount of add/sub units that can execute functions like ADD(LD) SUB(SD)
        /// </summary>
        public int NumberOfAddUnits { get; set; }
        /// <summary>
        /// The number of units that can execute the Multiplcation functions MULT
        /// </summary>
        public int NumberOfMultiplyUnits { get; set; }
        /// <summary>
        /// The number of units that can execute the Divistion function DIVID
        /// </summary>
        public int NumberOfDivideUnits { get; set; }

        /// <summary>
        /// The time the student started his/her exam
        /// </summary>
        public DateTime StartTimeInner { get; set; }
        /// <summary>
        /// The time the student ended his/her exam
        /// </summary>
        public DateTime EndTimeInner { get; set; }
        /// <summary>
        /// The time the student started his/her exam
        /// </summary>
        public string StartTime => StartTimeInner.ToString("T");
        /// <summary>
        /// The time the student ended his/her exam
        /// </summary>
        public string EndTime => EndTimeInner.ToString("T");
        
        
        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public ExamResultModel()
        {
            Instructions = new List<InstructionModel>();
        } 
        #endregion
    }
}
