using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wTryNetFree
{
    class Server
    {
        public static void Run()
        {
            Process server = new Process();
            server.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
            server.StartInfo.Arguments = "nfaw-io\\src\\server -c config.json";
            server.StartInfo.FileName = "nfaw-io\\src\\node.exe";
            server.StartInfo.UseShellExecute = true;
            // מגדיר אותו לרוץ מוסתר
            server.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            server.Start();
            //אם הוא כבר שלח פידבק פעם אחת אפשר לסגור את התוכנה
            if (App.config.feedbackSent == true)
            {
                App.Current.Shutdown();
            }
            //ממתין שהשרת ייצא
            server.WaitForExit();
            OnClose();
        }
        public static void OnClose() {
            Feedback feedback = new Feedback();
            feedback.Show();
        }

        public static void SetConnectPort()
        {
            int[] possiblePorts = { 53, 80, 110, 143, 443, 1000, 1433, 3306, 5060, 5938, 21, 25  }; // שניתי את הסדר כי שתי הפורטים האחרונים עושים בעיות איטיים ומתנתקים
            foreach (int port in possiblePorts)
            {
                string url = App.config.url.ToString().Replace("{port}", port.ToString());
                if (
                    Requset.Send(url, "GET") == "netfree server"
                )
                {
                    App.config.url = url;
                    return;
                }
            }
            MessageBox.Show("מצטערים, לא הצלחנו ליצור תקשורת עם שרת הסינון. \n ");
            App.Current.Shutdown();
        }
    }

}
