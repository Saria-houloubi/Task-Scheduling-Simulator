using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThishreenUniversity.ParallelPro.Enums;

namespace Tishreen.ParallelPro.Core
{
    /// <summary>
    /// The exam logic for the student
    /// </summary>
    public class StudentExamInformationViewModel : BaseViewModel
    {
        #region Properties
        /// <summary>
        /// The name of the student
        /// </summary>
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        /// <summary>
        /// Student university nunmber
        /// </summary>
        private int? _number;
        public int? Number
        {
            get { return _number; }
            set { SetProperty(ref _number, value); }
        }
        /// <summary>
        /// The time of the classes that the student can choose from
        /// </summary>
        public List<String> ClassesTime { get; set; } = new List<string>
        {
            "8:00",
            "9:30",
            "11:00",
            "12:30",
            "02:00",
            "03:30"
        };
        /// <summary>
        /// The time of the class that the student selected
        /// </summary>
        private string _selectedClassTime;
        public string SelectedClassTime
        {
            get { return _selectedClassTime; }
            set { SetProperty(ref _selectedClassTime, value); }
        }
        #endregion

        #region Commnads
        /// <summary>
        /// The command to start the exam 
        /// </summary>
        public DelegateCommand EnterExamCommand { get; set; }
        #endregion



        #region Constructer
        /// <summary>
        /// Default Constructer
        /// </summary>
        public StudentExamInformationViewModel()
        {
            SelectedClassTime = ClassesTime[0];

            //Create Commands
            EnterExamCommand = new DelegateCommand(
                //Exceute
                () => 
                IoC.Appliation.CurrentPage = ApplicationPages.MainWindow,
                //Canexecute
                () => 
                !string.IsNullOrEmpty(Name) && Number != null)
            .ObservesProperty(()=>Name).ObservesProperty(()=>Number);
        }
        #endregion

    }
}
