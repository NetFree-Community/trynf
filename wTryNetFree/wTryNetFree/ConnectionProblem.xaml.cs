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
    /// Interaction logic for ConnectionProblem.xaml
    /// </summary>
    public partial class ConnectionProblem : CustomWindow
    {
        public ConnectionProblem()
        {
            InitializeComponent();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            string output =
                "<strong>שם: </strong>" + name.Text + "<br>" +
                "<strong>מייל: </strong><a href='mailto:" + Email.Text + "'>" + Email.Text + "</a><br>" +
                "<strong>תיאור הבעיה: </strong>" + Problem.Text.Replace("\n", "<br>");
            Requset.Send(App.config.apiAddress.ToString()+ "send-test-feedback", "POST", "subject=קשיים בחיבור&body=" + output);
            MessageBox.Show("תודה על הדיווח");
            App.Current.Shutdown();
        }
    }
}
