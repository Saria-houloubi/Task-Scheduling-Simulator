

using ThishreenUniversity.ParallelPro.Enums.Instructions;

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
        public ApplicationPages CurrentPage { get; set; } = ApplicationPages.MainWindow;

    }
}
