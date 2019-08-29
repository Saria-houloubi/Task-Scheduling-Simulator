using Prism.Mvvm;
using System.Windows;

namespace PPS.UI.Shared.ViewModels.Base
{
    /// <summary>
    /// A base view model for any cross function
    /// </summary>
    public abstract class BaseViewModel : BindableBase
    {

        #region Properties

        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public BaseViewModel()
        {

        }
        #endregion

        #region Helpers


        protected void ShowErrorMessage(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion
    }
}
