using GeneralHelpers;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Tishreen.ParallelPro.Core  
{

    /// <summary>
    /// Base Class for the propertyChange events which will be inherated from other view models
    /// </summary>
    public class BaseViewModel : BindableBase
    {
        #region Protected Members
        /// <summary>
        /// The charecters that we want to trim befor inserting some values to out database
        /// </summary>
        protected char[] charsToTrim = { ' ', '\t' };

        #endregion

        #region Commands Helpers

        /// <summary>
        /// Runs a command if the updating flag is not set/
        /// If the flag is true (indecating that the function is already running) then the action is not run.
        /// If the flag is fale (indecating no running function) then the action is run.
        /// Once the action is finished if it was run, then the flag is reset to false
        /// </summary>
        /// <param name="updatingFlag"> The boolean property flag defining if the command is already running </param>
        /// <param name="action"> The action to run if the command is not already running</param>
        /// <returns></returns>
        protected async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            //Check if the flag property is true (if the function is already running)
            //We passed the flag as an expression because we can not pass properties by refrence so we can update it
            if (updatingFlag.GetPropertyValue())
                return;

            //Set the property flag to true to indecate that we are running
            updatingFlag.SetPropertyValue(true);

            try
            {
                //Run the passed action
                await action();
            }
            finally
            {
                //Set the property flag back to false now because it is finished
                updatingFlag.SetPropertyValue(false);
            }
        }

        /// <summary>
        /// Runs a command if the updating flag is not set/
        /// If the flag is true (indecating that the function is already running) then the action is not run.
        /// If the flag is fale (indecating no running function) then the action is run.
        /// Once the action is finished if it was run, then the flag is reset to false
        /// </summary>
        /// <param name="updatingFlag"> The boolean property flag defining if the command is already running </param>
        /// <param name="action"> The action to run if the command is not already running</param>
        /// <param name="onTrue">Repersents if the property should be true to pressed defalut to true</param>
        /// <param name="change">A falg if false will not change the property only starts the action</param>
        /// <returns></returns>
        protected async Task RunCommandTurnOffProperty(Expression<Func<bool>> updatingFlag, Func<Task> action, bool change = true, bool onTrue = true)
        {
            //Checks if we do not want to change the proeprties
            if (change)
            {
                if (onTrue)
                {
                    //Check if the flag property is true (if the function is already running)
                    //We passed the flag as an expression because we can not pass properties by refrence so we can update it
                    if (!updatingFlag.GetPropertyValue())
                        return;

                    //Set the property flag to true to indecate that we are running
                    updatingFlag.SetPropertyValue(false);
                }
                else
                {
                    //Check if the flag property is true (if the function is already running)
                    //We passed the flag as an expression because we can not pass properties by refrence so we can update it
                    if (updatingFlag.GetPropertyValue())
                        return;

                    //Set the property flag to true to indecate that we are running
                    updatingFlag.SetPropertyValue(true);
                }
            }
            //Run the passed action
            await action();

        }

        /// <summary>
        /// Only runs the action that is passed to it with no flag checking
        /// </summary>
        /// <param name="action">The action that we want to run</param>
        /// <returns></returns>
        protected void OnlyRunCommand(Action action)
        {
            //Run the passed action
            action();
        }
        #endregion

        #region Constructer
        /// <summary>
        /// Default Constructer
        /// </summary>
        public BaseViewModel()
        {
        }
        #endregion

        #region Cross Methods

        /// <summary>
        /// Creats a new thread and sets the action to work there
        /// </summary>
        protected void OnStartThread(ThreadStart action)
        {
            //Creating the thread
            Thread thread = new Thread(action);

            //Starting the thread
            thread.Start();
        }

        ///// <summary>
        ///// Displays an exception message to user 
        ///// </summary>
        ///// <param name="exception">The exception that we want to display</param>
        //protected void RaiseExecptionMessage(Exception exception)
        //{
        //    IoC.UI.ShowExceptionMessage(exception);
        //}

        ///// <summary>
        ///// Displays a message to user asking conformation to delete 
        ///// </summary>
        //protected bool AskConformationToDelete()
        //{
        //    return IoC.UI.AskForConformation();
        //}

        //protected virtual void DeleteCommandMethod()
        //{ //Ask the user for conformation
        //    var conformation = AskConformationToDelete();

        //    //If it was canceled then do not delete
        //    if (!conformation)
        //        throw new Exception("Do not delete,No conformation");
           
        //}
        #endregion
    }
}
