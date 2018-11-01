using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using Tishreen.ParallelPro.Core;
using TishreenUniversity.ParallelPro.ViewModels;
using System.IO;
namespace TishreenUniversity.ParallelPro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Custom startup so we can bind the viewModels to the IoC
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            //Let the start up do its work
            base.OnStartup(e);
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            //Setup hte Kernel


            try
            {
                IoC.Setup();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.InnerException.Message);
                MessageBox.Show(ex.InnerException?.InnerException?.Message);
            }

            IoC.Kernel.Bind<IUIManager>().ToConstant(new UIManager());

            //Set the main border background bruhs colore
            Application.Current.Resources["MainBorderBackground"] = new SolidColorBrush(Colors.Black);


            this.MainWindow = new MainWindow();
            this.MainWindow.Show();
        }


        void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            MessageBox.Show(e.Message);
            MessageBox.Show(e.InnerException.Message);
            MessageBox.Show(e.StackTrace.ToString());
            // print out the exception stack trace to a log
        }
    }
}
