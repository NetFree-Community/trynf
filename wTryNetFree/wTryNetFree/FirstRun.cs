using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows;
using System.Diagnostics;
using System.IO;

namespace wTryNetFree
{

    public class FirstRun
    {
        private string configString = @"
                      {
                      ""url"": ""http://185.37.150.104:{port}/netfree"",
                      ""listen"": 1080,
                      ""firefox"": ""..\\..\\ff_win32\\firefox.exe"",
                      ""firefoxprofile"": ""..\\..\\ff_profile"",
                      ""apiAddress"": ""http://items.cf:5938/api/""
                      }"
        ;
        //""apiAddress"": ""http://localhost:8080/api/""

        public FirstRun()
        {
            App.config = JsonConvert.DeserializeObject<dynamic>(configString);
            Server.SetConnectPort();
            CreateShortcut();
            App.openMainWindow();
        }
        private void CreateShortcut()
        {
            string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            using (StreamWriter writer = new StreamWriter(deskDir + "\\נסה את נטפרי.url"))
            {
                string app = System.Reflection.Assembly.GetExecutingAssembly().Location;
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + app);
                writer.WriteLine("IconIndex=0");
                string icon = app.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
                writer.Flush();
            }
        }
    }
    
}
