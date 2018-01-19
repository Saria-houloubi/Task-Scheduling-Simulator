using ParallelPro.Core.IoC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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

            //Setup hte Kernel
            IoC.Setup();

            this.MainWindow = new MainWindow();
            this.MainWindow.Show();

        }
    }
}
