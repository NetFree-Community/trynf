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
            if (this.txtid.Text.Length == 0) {
                this.txtErr.Text = "אין מזהה חיבור";
                // אם שגוי, יוצא מהפונקצייה
                return;
            }

            //בודק אם מזהה החיבור לא מכיל נקודותיים
            if (!this.txtid.Text.Contains(":"))
            {
                this.txtErr.Text = "נראה שמזהה החיבור שגוי. מזהה החיבור צריך להיות עם : (נקודותיים)";
                return;
            }
            // עכשיו, אחרי שכל הבדיקות עברו תקין, אני ממשיך הלאה
            // מצהיר על מערך
            // ומפצל את המזהה לשם משתמש וסיסמא
                string[] userpass = this.txtid.Text.Split(':');
            // מצהיר על המשתנה
            // שים לב שאני לוקח את התוכן של ההגדרות מהגדרות התוכנית

                string sjson = Properties.Settings.Default.sjson.Replace("{{port}}", "5938").Replace("{{pass}}", userpass[1]).Replace("{{user}}",userpass[0]);

            // כותב את התוכן לקובץ
            System.IO.File.WriteAllText(Environment.CurrentDirectory + "\\config.json", sjson);

            // מריץ את הקובץ

            Process p = new Process();
                p.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                p.StartInfo.Arguments = "nfaw-io\\src\\server -c config.json";
                p.StartInfo.FileName = "nfaw-io\\src\\node.exe";
                p.StartInfo.UseShellExecute = true;
            // מגדיר אותו לרוץ מוסתר
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();

            // מסתיר את המסך
            this.Hide();
            //ממתין שהפיירפוקס ייצא
            p.WaitForExit();
            //אחרי שהוא יצא, יוצא גם מהתוכנית
            this.Close();
            
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // סגור את התוכנית
            this.Close();

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
    }
}
