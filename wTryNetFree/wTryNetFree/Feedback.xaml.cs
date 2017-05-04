using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace wTryNetFree
{
    /// <summary>
    /// Interaction logic for feedback.xaml
    /// </summary>
    public partial class Feedback : CustomWindow
    {
        public Feedback()
        {
            InitializeComponent();
        }
        private void Submit(object sender, RoutedEventArgs e)
        {
            if (
                easyConnection.SelectedItem == null
                && satisfaction.SelectedItem == null
                && currentFilter.SelectedItem == null
                && easyConnection_note.Text.Length == 0
                && Satisfaction_note.Text.Length == 0
                && name.Text.Length == 0
                && telephone.Text.Length == 0
                )
                return;

            string output =
            "<strong>האם היה לי קל להתחבר: </strong>" + easyConnection.SelectedValue?.ToString() + "<br>" +
            "<strong>הערות: </strong>" + Satisfaction_note.Text.Replace("\n", "<br>") + "<br>" +
            "<strong>איך התרשמתי מנטפרי: </strong>" + satisfaction.SelectedValue?.ToString() + "<br>" +
            "<strong>הערות: </strong>" + Satisfaction_note.Text.Replace("\n", "<br>") + "<br>" +
            "<strong>סינון נוכחי: </strong>" + currentFilter.SelectedValue?.ToString() + "<br>" +
            "<strong>מעוניין להצטרף<br> שם: </strong>" + name.Text+ "<br>" +
            "<strong>טלפון: </strong>" + telephone.Text
            ;
            string response = Requset.Send(App.config.apiAddress.ToString() + "send-test-feedback/", "POST",  "subject=משוב על הניסיון&body=" + output);
            if (telephone.Text.Length == 0 || name.Text.Length == 0) App.config.userReg = false;
            //אם המשתמש השאיר פרטים והשליחה נכשלה צריך להודיע לו שזה נכשל כדי שלא יחכה שיחזרו אליו
            //אם הוא לא השאיר פרטים לא נורא אם זה נכשל
            if (response == "true" || telephone.Text.Length == 0)
            {
                App.config.feedbackSent = true;
                App.WriteConfig();
                Thank thank = new Thank();
                thank.Show();
            }
            else
            {
                Error error = new Error();
                error.Show();
            }
            Hide();
        }


    }
}
