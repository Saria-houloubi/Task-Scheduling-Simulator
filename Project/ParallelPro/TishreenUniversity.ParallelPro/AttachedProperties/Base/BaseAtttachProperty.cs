
using System;
using System.Windows;

namespace TishreenUniversity.ParallelPro
{
    /// <summary>
    /// Base attached property to replace the vanilla WPF attachedProperty
    /// </summary>
    /// <typeparam name="Parent"> the parent class of the attached property</typeparam>
    /// <typeparam name="Property"> type of the attached property</typeparam>

    public abstract class BaseAttachedProperty<Parent,Property> 
        where Parent : BaseAttachedProperty<Parent,Property>,new()
    {
        #region Public Events

        /// <summary>
        /// Fires when the value is changed
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };


        /// <summary>
        /// Fires when the value is changed even when the value has not been changed
        /// </summary>
        public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };
        #endregion

        #region Public Properties
        /// <summary>
        /// A singleton instance of our parent class
        /// </summary>
        public static Parent instance { get; private set; } = new Parent();

        #endregion

        #region  Attached Property Definitions
        /// <summary>
        /// The Attached property for this class
        /// </summary>
        /// CoerceCallBack will be called each time the property is set even if it did not change value
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
            "Value",
            typeof(Property),
            typeof(BaseAttachedProperty<Parent, Property>),
            new UIPropertyMetadata(
                default(Property),
                new PropertyChangedCallback(OnPropertyValueChanged),
                new CoerceValueCallback(OnPropertyValueUpdated)));
       
        /// <summary>
        /// The call back event when the property is changed even if it is the same value
        /// </summary>
        /// <param name="d"> The UI element that had it's property changed</param>
        /// <param name="e"> Arguments for the event</param>
        private static object OnPropertyValueUpdated(DependencyObject d, object baseValue)
        {
            //Calls the parent function
            instance.onValueUpdated(d, baseValue);

            //Calls event listeners
            instance.ValueUpdated(d, baseValue);

            //We do not want to change the value
            return baseValue;
        }

        /// <summary>
        /// The call back event when the property is changed
        /// </summary>
        /// <param name="d"> The UI element that had it's property changed</param>
        /// <param name="e"> Arguments for the event</param>
        private static void OnPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Calls the parent function
            instance.onValueChange(d, e);

            //Calls event listeners
            instance.ValueChanged(d, e);
        }


        /// <summary>
        /// Gets the attached property in lambda expretion
        /// </summary>
        /// <param name="d"> The element to get the property from</param>
        /// <returns></returns>

        public static Property GetValue(DependencyObject d) => (Property)d.GetValue(ValueProperty);

        /// <summary>
        /// Sets the attached property
        /// </summary>
        /// <param name="d"> The elemnt to get the property from </param>
        /// <param name="value"> The value to set the property with </param>

        public static void SetValue(DependencyObject d, Property value) => d.SetValue(ValueProperty,value);

        #endregion

        #region Event Methods

        /// <summary>
        /// The method when any attached property of this type is change
        /// </summary>
        /// <param name="d"> UI element that this property changed for </param>
        /// <param name="e"> Arguments for this event </param>
        public virtual void onValueChange(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

        /// <summary>
        /// The method when any attached property of this type is change even when the value has not been changed
        /// </summary>
        /// <param name="d"> UI element that this property changed for </param>
        /// <param name="e"> Arguments for this event </param>
        public virtual void onValueUpdated(DependencyObject d, object baseValue) { }

        #endregion
    }

}
