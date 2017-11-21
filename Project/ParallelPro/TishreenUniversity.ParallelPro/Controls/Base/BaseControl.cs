using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TishreenUniversity.ParallelPro
{
    public class BaseViewModel { }

    /// <summary>
    /// A base class for all the custom user controls and windows with animations
    /// </summary>
    public class BaseUserControl : UserControl
    {


        #region Dependency Property
        /// <summary>
        /// A property for slide in/out from left
        /// </summary>
        public bool SlideInOutLeft
        {
            get { return (bool)GetValue(SlideInOutLeftProperty); }
            set { SetValue(SlideInOutLeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SlideIn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SlideInOutLeftProperty =
            DependencyProperty.Register("SlideInOutLeft", typeof(bool), typeof(BaseUserControl), new PropertyMetadata(false, new PropertyChangedCallback(OnSlideInOutLeftPropertyChange)));

        private static void OnSlideInOutLeftPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BaseUserControl control = d as BaseUserControl;


            if ((bool)e.NewValue == true)
            {
                if (!control.IsLoaded)
                {
                    //Create an event which will hook and unhook it self
                    //when the element is loaded
                    RoutedEventHandler OnLoad = null;

                    OnLoad = (ss, ee) =>
                    {
                        //Unhook the event
                        control.Loaded -= OnLoad;

                        Task task = control.SlideInFromLeftAsync(0.1f);

                    };
                    //Hooks the load event
                    control.Loaded += OnLoad;

                }
                else
                {
                    Task task = control.SlideInFromLeftAsync();
                }
            }
            else
            {
                Task task = control.SlideOutToLeftAsync();
            }
        }

        /// <summary>
        /// A property for the slide in/out from bottom 
        /// </summary>
        public bool SlideInOutBottom
        {
            get { return (bool)GetValue(SlideInOutBottomProperty); }
            set { SetValue(SlideInOutBottomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SlideIn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SlideInOutBottomProperty =
            DependencyProperty.Register("SlideInOutBottom", typeof(bool), typeof(BaseUserControl), new PropertyMetadata(false, new PropertyChangedCallback(OnSlideInOutBottomPropertyChange)));

        private static void OnSlideInOutBottomPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BaseUserControl control = d as BaseUserControl;

            if ((bool)e.NewValue == true)
            {
                Task task = control.SlideAndFadeInFromBottomAsync();
            }
            else
            {
                Task task = control.SlideAndFadeOutFromBottomAsync();
            }
        }


        /// <summary>
        /// A property for the fade in/out from bottom 
        /// </summary>
        public bool FadeInOut
        {
            get { return (bool)GetValue(FadeInOutProperty); }
            set { SetValue(FadeInOutProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SlideIn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FadeInOutProperty =
            DependencyProperty.Register("FadeInOut", typeof(bool), typeof(BaseUserControl), new PropertyMetadata(false, new PropertyChangedCallback(OnFadeInOutPropertyChange)));

        private static void OnFadeInOutPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BaseUserControl control = d as BaseUserControl;

            if ((bool)e.NewValue == true)
            {
                Task task = control.FadeInAsync();
            }
            else
            {
                Task task = control.FadeOutAsync();
            }
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// The type of the aniamte in for the controls
        /// </summary>
        public AnimationTypes AnimateInAnimationType { get; set; } = AnimationTypes.FadeIn;

        /// <summary>
        /// The duration that the aniamtion will take finish
        /// </summary>
        public float Secounds { get; set; } = 0.8f;
        #endregion

        #region Constructer

        /// <summary>
        /// Default Constructer
        /// </summary>
        public BaseUserControl()
        {
            //If the page has got an animation hide it is the beginning
            if (this.AnimateInAnimationType != AnimationTypes.None)
                this.Visibility = Visibility.Collapsed;

            //Animate when the page is loaded
            Loaded += BaseControl_LoadedAsync;
        }
        /// <summary>
        /// Calls the animation in method
        /// </summary>
        /// <param name="sender">The element that was loaded</param>
        /// <param name="e"></param>
        private async void BaseControl_LoadedAsync(object sender, System.Windows.RoutedEventArgs e)
        {
            await AnimateIn();
        }

        /// <summary>
        /// Choses the right animation for the control
        /// </summary>
        /// <returns></returns>
        private async Task AnimateIn()
        {
            //If the element has got no animations then do nothing
            if (AnimateInAnimationType == AnimationTypes.None)
            {
                this.Visibility = Visibility.Visible;
                return;
            }
            //Choose the right aniamtion type for the element
            switch (AnimateInAnimationType)
            {
                case AnimationTypes.FadeIn:
                    await this.FadeInAsync(Secounds);
                    break;
                case AnimationTypes.SlideInFromLeft:
                    await this.SlideInFromLeftAsync(Secounds);
                    break;
            }

        }
        #endregion
    }

    /// <summary>
    /// A base class which all the user controls will gain from with the view model added
    /// </summary>
    /// <typeparam name="VM">The View Model for the user control</typeparam>
    public class BaseUserControl<VM> : BaseUserControl
        where VM : BaseViewModel, new()
    {

        #region Private Members
        /// <summary>
        /// The view model that is passed for the user control
        /// </summary>
        private VM mViewModel;

        #endregion

        #region Public Propertise

        /// <summary>
        /// The view model that is passed for the user control
        /// </summary>
        public VM ViewModel
        {
            get { return mViewModel; }
            set
            {
                //If the view model has not been changed just end
                if (value == mViewModel)
                    return;

                //Set the new value
                mViewModel = value;

                //Set the data context
                this.DataContext = mViewModel;
            }
        }

        #endregion

        #region Constructer

        /// <summary>
        /// Default Constructer
        /// </summary>
        public BaseUserControl() : base()
        {
            //Creating the view model
            this.ViewModel = new VM();

        }
        #endregion
    }
}
