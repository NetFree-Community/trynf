using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
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
    /// Interaction logic for feedbakc2.xaml
    /// </summary>
    public partial class Feedback : Window
    {

        //0 - להתחבר
        //1 - התרשמות
        //2 - סינון נוכחי
        string[] formData = new string[3];
        public Feedback()
        {
            InitializeComponent();
        }
        private void CloseApp(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void setFormData(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            formData[Int32.Parse(radioButton.GroupName)] = radioButton.Name;
        }
        private void Submit(object sender, RoutedEventArgs e)
        {
            string output = "האם היה לי קל להתחבר: " + formData[0] + "\n" + 
                "הערות: " + Connect_note.Text + "\n" +
                "איך התרשמתי מנטפרי: " + formData[1]+ "\n" + 
                "הערות: " + Satisfaction_note.Text + "\n" + 
                "סינון נוכחי: " + formData[2]+ "\n" + 
                "מעוניין להצטרף\n שם: " + name.Text+ "\n" + 
                "טלפון: " + telephone.Text
                ;

            string response = Requset.Send("send-test-feedback/", "POST",  "feedback=" + output);
            //אם המשתמש השאיר פרטים והשליחה נכשלה צריך להודיע לו שזה נכשל כדי שלא יחכה שיחזרו אליו
            //אם הוא לא השאיר פרטים לא נורא אם זה נכשל
            if (response == "true" || telephone.Text.Length == 0)
            {
                Thank thank = new Thank();
                thank.Show();
            }
            else
            {
                Error error = new Error();
                error.Show();
            }
            this.Close();
        }


    }
}
