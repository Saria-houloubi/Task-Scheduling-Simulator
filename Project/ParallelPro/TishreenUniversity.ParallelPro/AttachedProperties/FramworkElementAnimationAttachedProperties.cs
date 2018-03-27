using System.Threading.Tasks;
using System.Windows;

namespace TishreenUniversity.ParallelPro
{
    /// <summary>
    /// A base class to run the animation when the boolean is set to true and hide when set to false
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public abstract class AnimateBasePropert<Parent>: BaseAttachedProperty<Parent,bool>
        where Parent : BaseAttachedProperty<Parent,bool> , new()
    {
        #region Private Members
        /// <summary>
        /// Indicates if it is the first load
        /// </summary>
        public bool FirstLoad { get; set; } = true;
        #endregion

        public override void onValueUpdated(DependencyObject d, object baseValue)
        {
            //If the sender is not a framword elment return
            if (!(d is FrameworkElement))
                return;

            //Get the element
            var element = (d as FrameworkElement);

            //Don't fire if the value has not been changed and it is not the first load
            if (element.GetValue(ValueProperty) == baseValue && !FirstLoad)
                return;

            if (FirstLoad)
            {
                //Create an event which will hook and unhook it self
                //when the element is loaded
                RoutedEventHandler OnLoad = null;

                OnLoad = (ss, ee) =>
                {
                    //Unhook the event
                    element.Loaded -= OnLoad;

                    //Call the animation that we want to do
                    DoAnimation(element, (bool)baseValue);

                    //Sets the first load to false
                    FirstLoad = false;
                };

                //Hooks the load event
                element.Loaded += OnLoad;

            }
            else
                //Do the animation when the value has changed
                DoAnimation(element, (bool)baseValue);
        }
        /// <summary>
        /// The animation function that we will call when the value changed
        /// </summary>
        /// <param name="element">The framwork element</param>
        /// <param name="value">The new value</param>
        public virtual void DoAnimation(FrameworkElement element, bool value) { }

    }

    /// <summary>
    /// A Property to animate the slid from the left
    /// </summary>
    public class AnimateSlideInFromLeftProperty:AnimateBasePropert<AnimateSlideInFromLeftProperty>
    {
        public override async void DoAnimation(FrameworkElement element, bool value)
        {
            //Animate in
            if (value)
                await element.SlideInFromLeftAsync(FirstLoad ? 0 : 0.3f);
            else
                //Animate out..
                await element.SlideOutToLeftAsync(FirstLoad ? 0 : 0.3f);
        }
    }

    /// <summary>
    /// A Property to animate the slid from the right
    /// </summary>
    public class AnimateSlideInFromRightProperty : AnimateBasePropert<AnimateSlideInFromRightProperty>
    {
        public override async void DoAnimation(FrameworkElement element, bool value)
        {
            await Task.Delay(1);
            //Animate in
            //if (value)
            //    await element.SlideAndFadeFromRight(0.8f);
            //else
            //    //Animate out..
            //    await element.SlideAndFadeOutToLeft(0.8f);
        }
    }

}
