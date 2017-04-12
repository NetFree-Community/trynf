using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace wTryNetFree
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }

        // אירוע הכפתור התחברות
        private void btnTry_Click(object sender, RoutedEventArgs e)
        {
            //בודק אם המזהה חיבור ריק
            if (txtid.Text.Length == 0) {
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
            if (!IsValidId(txtid.Text))
            {
                txtErr.Text = "מזהה חיבור לא תקני או שפג תוקפו";
                return;
            }
            string[] userpass = this.txtid.Text.Split(':');
            // שים לב שאני לוקח את התוכן של ההגדרות מהגדרות התוכנית
            string sjson = Properties.Settings.Default.sjson.Replace("{{port}}", "5938").Replace("{{pass}}", userpass[1]).Replace("{{user}}",userpass[0]);

            File.WriteAllText(Environment.CurrentDirectory + "\\config.json", sjson);

            // מריץ את הקובץ
            Process server = new Process();
                server.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                server.StartInfo.Arguments = "nfaw-io\\src\\server -c config.json";
                server.StartInfo.FileName = "nfaw-io\\src\\node.exe";
            server.StartInfo.UseShellExecute = true;
            // מגדיר אותו לרוץ מוסתר
            server.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            server.Start();
            Hide();

            //ממתין שהשרת ייצא
            server.WaitForExit();
            Feedback feedback = new Feedback();
            feedback.Show();
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // סגור את התוכנית
            Close();
        }

        //לוכד לחיצה על אנטר
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnTry_Click(sender, e);
            }

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // אם נלחץ הכפתור השמאלי של העכבר
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // גרור את החלון
                // יש צורך בזה מכיון שהחלון הוא ללא מסגרת 
                // כפי שמוגדר בזאמל
                // WindowStyle="None
                DragMove();
            }
        }
        private Boolean IsValidId(string id)
        {
            //מכסה גם מקרה של כשל בתקשורת עם השרת API
            //המחלקה מחזירה Failed
            //והתוכנית ממשיכה לרוץ כדי לנסות להתחבר
            bool isValid = Requset.Send("test-key/check/" + id, "GET") != "false";
            return isValid;
        }
    }

}
