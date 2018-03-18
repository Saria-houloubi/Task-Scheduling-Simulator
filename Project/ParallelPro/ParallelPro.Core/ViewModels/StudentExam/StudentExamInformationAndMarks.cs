using System;
using System.Collections.Generic;

namespace Tishreen.ParallelPro.Core
{
    /// <summary>
    /// A static class which holds the student information along the exam time
    /// </summary>
    public static class StudentExamInformationAndMarks
    {
        #region Porperties
        /// <summary>
        /// The student name
        /// </summary>
        public static string Name { get; set; }
        /// <summary>
        /// The student university number
        /// </summary>
        public static int? Number { get; set; }
        /// <summary>
        /// The student lab time
        /// </summary>
        public static string ClassTime { get; set; }
        /// <summary>
        /// The start time when the student enterd the exam
        /// </summary>
        public static DateTime StartTime { get; set; }
        /// <summary>
        /// The end time the student ended the exam
        /// </summary>
        public static DateTime EndTime { get; set; }
        /// <summary>
        /// The student exam marks
        /// ex :    1       ;      4       ;   5  ; 10
        /// means first exam; Clock Cycles ; Mark ; Fullmark
        /// </summary>
        public static List<string> ExamMarks { get; set; }
        #endregion
    }
}
