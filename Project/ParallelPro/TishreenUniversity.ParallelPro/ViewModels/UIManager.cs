using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using ThishreenUniversity.ParallelPro.Enums;
using Tishreen.ParallelPro.Core;
using Tishreen.ParallelPro.Core.Models;
using TishreenUniversity.ParallelPro.Windows;

namespace TishreenUniversity.ParallelPro.ViewModels
{
    /// <summary>
    /// The implentaion of the IUIManger class to work with the IoC
    /// </summary>
    public class UIManager : IUIManager
    {
        public void ShowMessage(string content)
        {
            MessageBox.Show(content);
        }

        public void ShowWinodw(ApplicationPages windowType,List<object> parameter = null,object parameter02 = null)
        {

            //The window that we want to be displayed to the user
            Window wantedWindow = new Window();

            switch (windowType)
            {
                case ApplicationPages.MainWindow:
                    wantedWindow = new MainWindow();
                    break;
                case ApplicationPages.ScoreBoarding:
                    wantedWindow = new ScoreBoardingWindow(parameter, (Dictionary<FunctionsTypes, int>)parameter02);
                    break;
                case ApplicationPages.Tomasulo:
                    wantedWindow = new TomasoluWindow(parameter, (List<KeyValuePair<FunctionsTypes, int>>)parameter02);
                    break;
                case ApplicationPages.ResultWindow:
                    wantedWindow = new ExamResultWindow();
                    //Do not let the user to change windows
                    wantedWindow.ShowDialog();
                    return;
                case ApplicationPages.LoginPage:
                    break;
                case ApplicationPages.AdminSettings:
                    break;
                case ApplicationPages.ExamStudentInformation:
                    break;
                case ApplicationPages.CodeInformationWindow:
                    wantedWindow = new CodeCyclesAndFunctionalUnitInformationWindow(parameter[0] as List<object>, parameter[1] as Dictionary<FunctionsTypes, int>, parameter[2] as Dictionary<FunctionalUnitsTypes, int>);
                    break;
                default:
                    Debugger.Break();
                    break;
            }
            wantedWindow.Show();
        }
    }
}
