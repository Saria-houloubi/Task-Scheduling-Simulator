using ThishreenUniversity.ParallelPro.Enums;

namespace Tishreen.ParallelPro.Core
{

    /// <summary>
    /// The view model for the state of the hole application 
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        /// <summary>
        /// The current page that will be shown to the user 
        /// </summary>
        private ApplicationPages _currentPage = ApplicationPages.LoginPage;
        public ApplicationPages CurrentPage
        {
            get { return _currentPage; }
            set { SetProperty(ref _currentPage, value); }
        }

    }
}
