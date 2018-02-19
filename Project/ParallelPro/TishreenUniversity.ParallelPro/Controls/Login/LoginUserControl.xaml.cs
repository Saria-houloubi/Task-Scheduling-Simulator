using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ThishreenUniversity.ParallelPro.Enums;
using Tishreen.Tishreen.ParallelPro.Core;

namespace TishreenUniversity.ParallelPro.Controls
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : BaseUserControl
    {
        public LoginUserControl()
        {
            InitializeComponent();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            LoginViewModel.LoginCheck("admin", "", UserTypes.admin);
        }
    }
}
