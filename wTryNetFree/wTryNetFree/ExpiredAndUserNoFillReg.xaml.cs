using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ExpiredAndUserNoFillReg.xaml
    /// </summary>
    public partial class ExpiredAndUserNoFillReg : CustomWindow
    {
        public ExpiredAndUserNoFillReg()
        {
            InitializeComponent();
            Keyboard.Focus(name);
            Closing += new CancelEventHandler(CloseApp);
        }
        private void CloseApp<T>(object sender, T e)
        {
            App.Current.Shutdown();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            if (name.Text.Length == 0) {
                Keyboard.Focus(name);
                return;
            }
            if (telephone.Text.Length == 0 || Regex.Match(telephone.Text, "[A-zא-ת]").Length > 0)
            {
                Keyboard.Focus(telephone);
                return;
            }
            string output =
                "<strong>שם: </strong>" + name.Text + "<br>" +
                "<strong>טלפון: </strong>" + telephone.Text + "<br>";

            Requset.Send(App.config.apiAddress.ToString() + "send-test-feedback/", "POST", "subject=מעוניין להתחבר&body=" + output); // מה להראות לו עכשיו???
            Close();

        }
    }
}
