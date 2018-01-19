
using System.Windows;
using System.Windows.Controls;

namespace TishreenUniversity.ParallelPro
{

    public class HasTextProperty : BaseAttachedProperty<HasTextProperty, bool>
    {
        /// <summary>
        /// Sets the property value based on the if the passwordBox.password lenght is more than 1
        /// So it checks if it has any text 
        /// </summary>
        /// <param name="sender"></param>
        public static void SetValue(DependencyObject sender)
        {
            //If the length is bigger than 0 then it has text in it
            HasTextProperty.SetValue(sender, (sender as PasswordBox).Password.Length > 0 );
        }
    }

    public  class MonitorPasswordProperty : BaseAttachedProperty<MonitorPasswordProperty,bool>
    {
        public override void onValueChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Getting the password object from the calles
            var passwordBox = (d as PasswordBox);

            //Checking for safty
            if (passwordBox == null)
                return;

            //Clear Previous events
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;

            //If calles monitorPassword is true..
            if ((bool)e.NewValue)
            {
                //Sets the default value
                HasTextProperty.SetValue(passwordBox);

                //Listiens to value changed event
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }
        /// <summary>
        /// The event that will trigger when the passwordbox.password changes
        /// </summary>
        /// <param name="sender">The passwordBox</param>
        /// <param name="e"></param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            HasTextProperty.SetValue((sender as PasswordBox));
        }
    }
}
