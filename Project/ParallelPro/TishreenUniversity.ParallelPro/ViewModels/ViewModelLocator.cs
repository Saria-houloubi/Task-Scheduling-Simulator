using Ninject;
using Tishreen.ParallelPro.Core;

namespace TishreenUniversity.ParallelPro
{
    /// <summary>
    /// For the setup of the current page
    /// </summary>
    public class ViewModelLocator : BaseViewModel
    {
        #region SingleTone Properties

        /// <summary>
        /// The instace to bind to in xaml
        /// </summary>
        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();
        /// <summary>
        /// The viewmodel that holds the cuurent page instance
        /// </summary>
        public static ApplicationViewModel ApplicationViewModel => IoC.Kernel.Get<ApplicationViewModel>();
        #endregion
    }
}
