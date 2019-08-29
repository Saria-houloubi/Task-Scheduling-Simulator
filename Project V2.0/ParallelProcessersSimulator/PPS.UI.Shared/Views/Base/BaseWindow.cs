using MahApps.Metro.Controls;

namespace PPS.UI.Shared.Views.Base
{
    /// <summary>
    /// The base window for cross functions
    /// </summary>
    public abstract class BaseWindow : MetroWindow
    {

        #region Properties

        #endregion

        #region Constructer
        /// <summary>
        /// Defauit contructer
        /// </summary>
        public BaseWindow()
        {


            //Start in the center and as max size
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            this.WindowState = System.Windows.WindowState.Maximized;
        }
        #endregion

    }
}
