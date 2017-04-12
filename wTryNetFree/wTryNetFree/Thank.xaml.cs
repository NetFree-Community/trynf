using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace wTryNetFree
{
    /// <summary>
    /// Interaction logic for Thank.xaml
    /// </summary>
    public partial class Thank : Window
    {
        public Thank()
        {
            InitializeComponent();
            Closing += new CancelEventHandler(CloseApp);
        }

        void CloseApp(object sender, CancelEventArgs e)
        {
            App.Current.Shutdown();
        }
        private void CloseApp(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
