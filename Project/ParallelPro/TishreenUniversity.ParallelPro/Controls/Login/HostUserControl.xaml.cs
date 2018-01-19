

using System.Windows;
using System.Windows.Controls;


namespace TishreenUniversity.ParallelPro
{
    /// <summary>
    /// Interaction logic for HostUserControl.xaml
    /// </summary>
    public partial class HostUserControl : UserControl
    {

        #region Dependency Properties
        /// <summary>
        /// Dependency proprety
        /// </summary>
        public BaseUserControl CurrentControl
        {
            get { return (BaseUserControl)GetValue(CurrentControlProperty); }
            set { SetValue(CurrentControlProperty, value); }
        }

       /// <summary>
       /// Register the <see cref="CurrentControl"/> as a dependency property which we can access from xaml
       /// </summary>
        public static readonly DependencyProperty CurrentControlProperty =
            DependencyProperty.Register(nameof(CurrentControl), typeof(BaseUserControl), typeof(HostUserControl), new UIPropertyMetadata(new PropertyChangedCallback(CurrentControlPropertyChanged)));



        #endregion

        #region Constructer
        /// <summary>
        /// Default constructer
        /// </summary>
        public HostUserControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Property Changed Event

        /// <summary>
        /// Calls function when <see cref="CurrentControl"/> changes
        /// </summary>
        /// <param name="d">the sender (User control)</param>
        /// <param name="e">The new value (the user new user control)</param>
        private static void CurrentControlPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Getting the value for the old and new controls
            var oldControl = (d as HostUserControl).OldUserControl;
            var newControl = (d as HostUserControl).NewUserControl;

            //Getting the content of the new user control
            var oldControlContent = newControl.Content;

            //Setting the new content to null, reseting the new control host for the new value
            newControl.Content = null;

            //Setting the content to the old control
            oldControl.Content = oldControlContent;

            //Animating out the old control 
            if (oldControlContent is BaseUserControl)
                (oldControlContent as BaseUserControl).ShouldAnimateOut = true;

            //Setting the new control 
            newControl.Content = e.NewValue;
        }

        #endregion
    }
}
