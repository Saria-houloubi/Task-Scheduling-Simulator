using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThishreenUniversity.ParallelPro.Enums
{
    /// <summary>
    /// All the application pages that we can show to the user
    /// </summary>
    public enum ApplicationPages
    {
        /// <summary>
        /// Shows the main window 
        /// </summary>
        MainWindow = 0,
        /// <summary>
        /// Shows the login page
        /// </summary>
        LoginPage = 1,
        /// <summary>
        /// The scoreboarding algo window
        /// </summary>
        ScoreBoarding = 2,
        /// <summary>
        /// The admin setting page
        /// </summary>
        AdminSettings = 3,
        /// <summary>
        /// The student information page before the exam
        /// </summary>
        ExamStudentInformation = 4,
        /// <summary>
        /// The result window after an exam
        /// </summary>
        ResultWindow = 5,
        /// <summary>
        /// The window which holds the code and the information the user entered
        /// </summary>
        CodeInformationWindow = 6,
        /// <summary>
        /// The tomasulo algo window 
        /// </summary>
        Tomasulo = 7,
        LoopUnrolling= 8,
    }
}
