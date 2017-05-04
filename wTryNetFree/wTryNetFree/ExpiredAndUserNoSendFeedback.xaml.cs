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
    /// Interaction logic for ExpiredAndUserNoSendFeedback.xaml
    /// </summary>
    public partial class ExpiredAndUserNoSendFeedback : CustomWindow
    {
        public ExpiredAndUserNoSendFeedback()
        {
            InitializeComponent();
        }
        private void FillFeedback(object sender, RoutedEventArgs e)
        {
            Feedback feedback = new Feedback();
            feedback.Show();
            Hide();
        }
    }
}
