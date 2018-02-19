using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using ThishreenUniversity.ParallelPro.Enums;
using Tishreen.ParallelPro.Core;
using TishreenUniversity.ParallelPro.Windows;

namespace TishreenUniversity.ParallelPro.ViewModels
{
    /// <summary>
    /// The implentaion of the IUIManger class to work with the IoC
    /// </summary>
    public class UIManager : IUIManager
    {
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
                    wantedWindow = new ScoreBoardingWindow(parameter,(List<KeyValuePair<FunctionsTypes,int>>)parameter02);
                    break;
                default:
                    Debugger.Break();
                    break;
            }
            wantedWindow.Show();
        }
    }
}
