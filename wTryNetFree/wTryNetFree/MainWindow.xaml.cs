using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Dynamic;
using Newtonsoft.Json;

namespace wTryNetFree
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : CustomWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ConnectionProblem(object sender, RoutedEventArgs e)
        {
            ConnectionProblem connectionProblem = new ConnectionProblem();
            connectionProblem.ShowDialog();
        }
        // אירוע הכפתור התחברות
        private void btnTry_Click(object sender, RoutedEventArgs e)
        {
            //בודק אם המזהה חיבור ריק
            if (txtid.Text.Length == 0)
            {
                txtErr.Text = "אין מזהה חיבור";
                return;
            }

            //בודק אם מזהה החיבור לא מכיל נקודותיים
            if (!txtid.Text.Contains(":"))
            {
                txtErr.Text = "נראה שמזהה החיבור שגוי. מזהה החיבור צריך להיות עם : (נקודותיים)";
                return;
            }
            //בודק חוקיות מזהה
            string[] userpass = this.txtid.Text.Split(':');

            App.config.username = userpass[0];
            App.config.password = userpass[1];

            if (Validation.ExpiredID((string)App.config.username))
            {
                CustomWindow expired;

                if (App.config.feedbackSent == true && App.config.userReg == true) expired = new ExpiredAndUserFillReg();
                else if (App.config.feedbackSent == true && App.config.userReg == false) expired = new ExpiredAndUserNoFillReg(); 
                else expired = new ExpiredAndUserNoSendFeedback();
                
                expired.Show();
                Hide();
                return;
            }
            if (Validation.BadId(txtid.Text))
            {
                txtErr.Text = "מזהה חיבור לא תקני";
                return;
            }
            App.WriteConfig();
            Hide();
            Server.Run();
        }

        //לוכד לחיצה על אנטר
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnTry_Click(sender, e);
            }

        }
    }


}
