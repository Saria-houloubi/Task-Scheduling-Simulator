
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace TishreenUniversity.ParallelPro
{
    /// <summary>
    /// A helper class for the storyboards
    /// </summary>
    public static class StoryBoradHelpers
    {
        #region Slide in/out from left
        
        #region Slide From Left Animation


        /// <summary>
        /// Add a slide in from left animation 
        /// </summary>
        /// <param name="storyBoard">The storyboard to add to</param>
        /// <param name="width">The width of the element to slid in</param>
        /// <param name="secounds">The duration of the animation that we want to take action</param>
        /// <param name="decelerationRatio"> The rate of deceleration </param>
        public static void AddSlideInFromLeft(this Storyboard storyBoard, double offSet, float secounds, float decelerationRatio = 0.3f)
        {
            var slideAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(secounds)),
                From = new Thickness(-offSet, 0, 0, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            //Set the target property 
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));

            //Add it to the animations
            storyBoard.Children.Add(slideAnimation);
        }
        #endregion

        #region Slide Out From Left
        /// <summary>
        /// Add a slide out from left animation 
        /// </summary>
        /// <param name="storyBoard">The storyboard to add to</param>
        /// <param name="offset">The width of the element to slid in</param>
        /// <param name="secounds">The duration of the animation that we want to take action</param>
        /// <param name="decelerationRatio"> The rate of deceleration </param>
        public static void AddSlideOutToLeft(this Storyboard storyBoard, double offset, float secounds, float decelerationRatio = 0.9f,bool KeepMargin = true)
        {
            var slideAnimation = new ThicknessAnimation
            {
                From = new Thickness(0),
                To = new Thickness(-offset, 0, KeepMargin ? offset : 0, 0),
                Duration = new Duration(TimeSpan.FromSeconds(secounds)),
                DecelerationRatio = decelerationRatio
            };

            //Set the target property 
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));

            //Add it to the animations
            storyBoard.Children.Add(slideAnimation);
        }
        #endregion

        #endregion

        #region Slide in/out from right

        #region Slide In From Right Animation


        /// <summary>
        /// Add a slide in from right animation 
        /// </summary>
        /// <param name="storyBoard">The storyboard to add to</param>
        /// <param name="width">The width of the element to slid in</param>
        /// <param name="secounds">The duration of the animation that we want to take action</param>
        /// <param name="decelerationRatio"> The rate of deceleration </param>
        public static void AddSlideInFromRight(this Storyboard storyBoard, double offSet, float secounds, float decelerationRatio = 0.3f)
        {
            var slideAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(secounds)),
                From = new Thickness(0, 0, -offSet, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            //Set the target property 
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));

            //Add it to the animations
            storyBoard.Children.Add(slideAnimation);
        }
        #endregion

        #region Slide Out From Right
        /// <summary>
        /// Add a slide to right animation 
        /// </summary>
        /// <param name="storyBoard">The storyboard to add to</param>
        /// <param name="offset">The width of the element to slid in</param>
        /// <param name="secounds">The duration of the animation that we want to take action</param>
        /// <param name="decelerationRatio"> The rate of deceleration </param>
        public static void AddSlideOutToRight(this Storyboard storyBoard, double offset, float secounds, float decelerationRatio = 0.9f, bool KeepMargin = true)
        {
            var slideAnimation = new ThicknessAnimation
            {
                From = new Thickness(0),
                To = new Thickness(KeepMargin ? offset : 0, 0,-offset , 0),
                Duration = new Duration(TimeSpan.FromSeconds(secounds)),
                DecelerationRatio = decelerationRatio
            };

            //Set the target property 
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));

            //Add it to the animations
            storyBoard.Children.Add(slideAnimation);
        }
        #endregion

        #endregion

        #region Slide in/out from bottom

        /// <summary>
        /// Add a slide in from bottom animation 
        /// </summary>
        /// <param name="storyBoard">The storyboard to add to</param>
        /// <param name="offSet">The height of the element to slid in</param>
        /// <param name="secounds">The duration of the animation that we want to take action</param>
        /// <param name="decelerationRatio"> The rate of deceleration </param>
        public static void AddSlideInFromBottom(this Storyboard storyBoard, double offSet, float secounds, float decelerationRatio = 0.3f)
        {
            var slideAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(secounds)),
                From = new Thickness(0,0, 0, -offSet),
                To = new Thickness(0,0,0,0),
                DecelerationRatio = decelerationRatio
            };

            //Set the target property 
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));

            //Add it to the animations
            storyBoard.Children.Add(slideAnimation);
        }

        /// <summary>
        /// Add a slide out from bottom animation 
        /// </summary>
        /// <param name="storyBoard">The storyboard to add to</param>
        /// <param name="offSet">The height of the element to slid in</param>
        /// <param name="secounds">The duration of the animation that we want to take action</param>
        /// <param name="decelerationRatio"> The rate of deceleration </param>
        public static void AddSlideOutFromBottom(this Storyboard storyBoard, double offSet, float secounds, float decelerationRatio = 0.3f)
        {
            var slideAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(secounds)),
                From = new Thickness(0,0,0,0),
                To = new Thickness(0,0, 0, -offSet),
                DecelerationRatio = decelerationRatio
            };

            //Set the target property 
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));

            //Add it to the animations
            storyBoard.Children.Add(slideAnimation);
        }


        #endregion

        #region Fade Animatinos

        /// <summary>
        /// Addes fade in animation to the story board
        /// </summary>
        /// <param name="storyBoard">The story board that we want to add the animation to</param>
        /// <param name="secounds">The duration of the animation that we want to take action</param>
        public static void AddFadeIn(this Storyboard storyBoard, float secounds)
        {
            //Create the animation opecity
            var fadeIn = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(secounds)
            };
            //assigns the animation to the wanted property
            Storyboard.SetTargetProperty(fadeIn, new PropertyPath("Opacity"));

            //addes it to the storyboard
            storyBoard.Children.Add(fadeIn);
        }

        /// <summary>
        /// Addes fade out animation to the story board
        /// </summary>
        /// <param name="storyBoard">The story board that we want to add the animation to</param>
        /// <param name="secounds">The duration of the animation that we want to take action</param>
        public static void AddFadeOut(this Storyboard storyBoard, float secounds)
        {
            //Create the animation opecity
            var fadeOut = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = TimeSpan.FromSeconds(secounds)
            };
            //assigns the animation to the wanted property
            Storyboard.SetTargetProperty(fadeOut, new PropertyPath("Opacity"));

            //addes it to the storyboard
            storyBoard.Children.Add(fadeOut);
        }

        #endregion
    }
}
