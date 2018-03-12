using System.Collections.Generic;
using ThishreenUniversity.ParallelPro.Enums;
namespace Tishreen.ParallelPro.Core
{

    /// <summary>
    /// The view model that will update the UI for the user
    /// </summary>
    public interface IUIManager 
    {
        /// <summary>
        /// Opens the sent window for the user
        /// </summary>
        /// <param name="windowType"></param>
        /// <param name="parameter">Any other data we want to send</param>
        /// <param name="parameter02"></param>
        void ShowWinodw(ApplicationPages windowType,List<object> parameter = null,object parameter02 = null);


        /// <summary>
        /// Shows the sent content in a messagebox 
        /// </summary>
        /// <param name="content"></param>
        void ShowMessage(string content);
    }
}
