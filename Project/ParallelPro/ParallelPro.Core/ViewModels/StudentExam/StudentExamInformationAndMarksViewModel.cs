using System;
using System.Collections.Generic;

namespace Tishreen.ParallelPro.Core
{
    /// <summary>
    /// A static class which holds the student information along the exam time
    /// </summary>
    public class StudentExamInformationAndMarksViewModel
    {
        #region Porperties
        /// <summary>
        /// The student name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The student university number
        /// </summary>
        public int? Number { get; set; }
        /// <summary>
        /// The student lab time
        /// </summary>
        public string ClassTime { get; set; }
        /// <summary>
        /// The start time when the student enterd the exam
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// The end time the student ended the exam
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// The results for each exam with there information
        /// </summary>
        public List<ExamResultModel> Results { get; set; } = new List<ExamResultModel>();

        #endregion
    }
}
