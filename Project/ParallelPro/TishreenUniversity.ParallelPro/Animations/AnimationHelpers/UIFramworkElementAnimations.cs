
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace TishreenUniversity.ParallelPro
{
    /// <summary>
    /// Framwork animations that we have
    /// </summary>
    public static class UIFramworkElementAnimationsHelpers
    {
        #region Slide in/out from left

        /// <summary>
        /// Slides in a <see cref="FrameworkElement"/> in to the user
        /// </summary>
        /// <param name="element">The element that we want to animate</param>
        /// <param name="secounds">The duration of the animation that will take action</param>
        /// <returns></returns>
        public static async Task SlideInFromLeftAsync(this FrameworkElement element, float secounds = 0.8f)
        {
            //Create a stroy borad
            var storyBorad = new Storyboard();

            //Adds a fade in animation
            storyBorad.AddSlideInFromLeft(element.ActualWidth, secounds);

            //Start the aniamtion
            storyBorad.Begin(element);

            //Make it visible to the user
            element.Visibility = Visibility.Visible;

            //Wait until the animations time ends
            await Task.Delay((int)(secounds * 1000));
        }


        /// <summary>
        /// Slides out a <see cref="FrameworkElement"/>
        /// </summary>
        /// <param name="element">The element that we want to animate</param>
        /// <param name="secounds">The duration of the animation that will take action</param>
        /// <returns></returns>
        public static async Task SlideOutToLeftAsync(this FrameworkElement element, float secounds = 0.8f)
        {
            //Create a stroy borad
            var storyBorad = new Storyboard();

            //Adds a fade in animation
            storyBorad.AddSlideOutToLeft(element.ActualWidth, secounds, KeepMargin: false);

            //Start the aniamtion
            storyBorad.Begin(element);

            //Make it visible to the user
            element.Visibility = Visibility.Visible;

            //Wait until the animations time ends
            await Task.Delay((int)(secounds * 1000));
        }

        #endregion

        #region Slide in/out from right

        /// <summary>
        /// Slides in a <see cref="FrameworkElement"/> in to the user
        /// </summary>
        /// <param name="element">The element that we want to animate</param>
        /// <param name="secounds">The duration of the animation that will take action</param>
        /// <returns></returns>
        public static async Task SlideInFromRightAsync(this FrameworkElement element, float secounds = 0.8f)
        {
            //Create a stroy borad
            var storyBorad = new Storyboard();

            //Adds a fade in animation
            storyBorad.AddSlideInFromRight(element.ActualWidth, secounds);

            //Start the aniamtion
            storyBorad.Begin(element);

            //Make it visible to the user
            element.Visibility = Visibility.Visible;

            //Wait until the animations time ends
            await Task.Delay((int)(secounds * 1000));
        }


        /// <summary>
        /// Slides out a <see cref="FrameworkElement"/>
        /// </summary>
        /// <param name="element">The element that we want to animate</param>
        /// <param name="secounds">The duration of the animation that will take action</param>
        /// <returns></returns>
        public static async Task SlideOutToRightAsync(this FrameworkElement element, float secounds = 0.8f)
        {
            //Create a stroy borad
            var storyBorad = new Storyboard();

            //Adds a fade in animation
            storyBorad.AddSlideOutToRight(element.ActualWidth, secounds, KeepMargin: false);

            //Start the aniamtion
            storyBorad.Begin(element);

            //Make it visible to the user
            element.Visibility = Visibility.Visible;

            //Wait until the animations time ends
            await Task.Delay((int)(secounds * 1000));
        }

        #endregion

        #region Slide in/out from bottom 
        /// <summary>
        /// Slides in a <see cref="FrameworkElement"/> in to the user
        /// </summary>
        /// <param name="element">The element that we want to animate</param>
        /// <param name="secounds">The duration of the animation that will take action</param>
        /// <returns></returns>
        public static async Task SlideInFromBottomAsync(this FrameworkElement element, float secounds = 0.5f)
        {
            //Create a stroy borad
            var storyBorad = new Storyboard();

            //Adds a slide in animation
            storyBorad.AddSlideInFromBottom(element.Height, secounds);

            //Start the aniamtion
            storyBorad.Begin(element);

            //Make it visible to the user
            element.Visibility = Visibility.Visible;

            //Wait until the animations time ends
            await Task.Delay((int)(secounds * 1000));

        }


        /// <summary>
        /// Slides in a <see cref="FrameworkElement"/> in to the user
        /// </summary>
        /// <param name="element">The element that we want to animate</param>
        /// <param name="secounds">The duration of the animation that will take action</param>
        /// <returns></returns>
        public static async Task SlideOutFromBottomAsync(this FrameworkElement element, float secounds = 0.5f)
        {
            //Create a stroy borad
            var storyBorad = new Storyboard();

            //Adds a slide in animation
            storyBorad.AddSlideOutFromBottom(element.Height, secounds);

            //Start the aniamtion
            storyBorad.Begin(element);

            //Make it visible to the user
            element.Visibility = Visibility.Visible;

            //Wait until the animations time ends
            await Task.Delay((int)(secounds * 1000));
        }

        /// <summary>
        /// Slides in a <see cref="FrameworkElement"/> in to the user
        /// </summary>
        /// <param name="element">The element that we want to animate</param>
        /// <param name="secounds">The duration of the animation that will take action</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutFromBottomAsync(this FrameworkElement element, float secounds = 0.5f)
        {
            //Create a stroy borad
            var storyBorad = new Storyboard();

            //Adds a slide in animation
            storyBorad.AddSlideOutFromBottom(element.Height, secounds);

            //Adds a slide in animation
            storyBorad.AddFadeOut(secounds);

            //Start the aniamtion
            storyBorad.Begin(element);

            //Make it visible to the user
            element.Visibility = Visibility.Visible;

            //Wait until the animations time ends
            await Task.Delay((int)(secounds * 1000));
        }

        /// <summary>
        /// Slides in a <see cref="FrameworkElement"/> in to the user
        /// </summary>
        /// <param name="element">The element that we want to animate</param>
        /// <param name="secounds">The duration of the animation that will take action</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromBottomAsync(this FrameworkElement element, float secounds = 0.5f)
        {
            //Create a stroy borad
            var storyBorad = new Storyboard();

            //Adds a slide in animation
            storyBorad.AddSlideInFromBottom(element.Height , secounds);

            //Adds a slide in animation
            storyBorad.AddFadeIn(secounds);

            //Start the aniamtion
            storyBorad.Begin(element);

            //Make it visible to the user
            element.Visibility = Visibility.Visible;

            //Wait until the animations time ends
            await Task.Delay((int)(secounds * 1000));
        }


        #endregion

        #region Fade in/out

        /// <summary>
        /// Fades a <see cref="FrameworkElement"/> in to the user
        /// </summary>
        /// <param name="element">The element that we want to animate</param>
        /// <param name="secounds">The duration of the animation that will take action</param>
        /// <returns></returns>
        public static async Task FadeInAsync(this FrameworkElement element, float secounds = 0.3f)
        {
            //Create a stroy borad
            var storyBorad = new Storyboard();

            //Adds a fade in animation
            storyBorad.AddFadeIn(secounds);

            //Start the aniamtion
            storyBorad.Begin(element);

            //Make it visible to the user
            element.Visibility = Visibility.Visible;

            //Wait until the animations time ends
            await Task.Delay((int)(secounds * 1000));
        }



        /// <summary>
        /// Fades a <see cref="FrameworkElement"/> out of the user
        /// </summary>
        /// <param name="element">The element that we want to animate</param>
        /// <param name="secounds">The duration of the animation that will take action</param>
        /// <returns></returns>
        public static async Task FadeOutAsync(this FrameworkElement element, float secounds = 0.3f)
        {
            //Create a stroy borad
            var storyBorad = new Storyboard();

            //Adds a fade in animation
            storyBorad.AddFadeOut(secounds);

            //Start the aniamtion
            storyBorad.Begin(element);

            //Make it visible to the user
            element.Visibility = Visibility.Visible;

            //Wait until the animations time ends
            await Task.Delay((int)(secounds * 1000));
        }


        #endregion
    }
}
