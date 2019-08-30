using PPS.UI.LoopUnrolling.Views;
using PPS.UI.ScoreboardAndTomasolu.Views;
using PPS.UI.Shared.Enums;
using PPS.UI.Shared.ViewModels.Base;
using Prism.Commands;
using System.Diagnostics;
using System.Windows;

namespace PPS.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {

        #region Commands
        /// <summary>
        /// Opens the window for the chosen algorithm
        /// </summary>

        public DelegateCommand<object> OpenWindowCommand { get; set; }
        #endregion

        #region Command Methods


        /// <summary>
        /// The function to check if the <see cref="OpenWindowCommand"/> can get executed
        /// </summary>
        /// <returns></returns>
        public bool OpenWindowCommand_CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// The function that will be called once the <see cref="OpenWindowCommand"/> is invoked
        /// </summary>
        public void OpenWindowCommand_Execute(object parameter)
        {
            if (parameter is WindowNames windowName)
            {
                Window window = new Window();
                switch (windowName)
                {
                    case WindowNames.ScoreBoardAndTomasolu:
                        window = new ScoreBoardAndTomasoluWindwo();
                        break;
                    case WindowNames.LoopUnrolling:
                        window = new LoopUnrollingWindow();
                        break;
                    case WindowNames.Cache:
                        Debugger.Break();
                        break;
                    case WindowNames.Vector:
                        Debugger.Break();
                        break;
                    default:
                        break;
                }
                window.Show();
            }
        }

        #endregion
        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public MainWindowViewModel()
        {
            //Create the commands
            OpenWindowCommand = new DelegateCommand<object>(OpenWindowCommand_Execute, OpenWindowCommand_CanExecute);

        }
        #endregion
    }
}
