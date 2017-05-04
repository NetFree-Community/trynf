using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

//using IWshRuntimeLibrary; // > Ref > COM > Windows Script Host Object  


namespace wTryNetFree
{

    public partial class CustomWindow : Window
    {
        public CustomWindow()
            : base()
        {

            // תרחיש בעייתי:
            //המשתמש מריץ את התוכנה פעם ראשונה, מקים שרת והחלון הראשי הופך להיות מוסתר
            // אחר כך נפתח לו חלון פידבק, והוא לא סוגר אותו דרך הכפתור סגירה אלא מבחוץ
            // החלון הראשי עדיין פועל ואין מי שיסגור אותו
            // לכן שמתי מאזין לכל אירוע סגירה שמרסק את כל התוכנית
            Closing += new CancelEventHandler(CloseApp);
        }
        public void CloseApp<T>(object sender, T e)
        {
            App.Current.Shutdown();
        }
        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
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
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        string configPath = Environment.CurrentDirectory + "\\config.json";
        public static dynamic config;
        
        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);
            if (!File.Exists(configPath))
            {
                new FirstRun();
                return;
            }
            config = JsonConvert.DeserializeObject<dynamic>(
                File.ReadAllText(configPath)
            );

            //פג תוקף המזהה
            
            if (Validation.ExpiredID((string)config.username))
            {
                openMainWindow();
                return;
            }
            WriteConfig();
            Server.Run();
        }

        public static void openMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        public static void WriteConfig()
        {
            File.WriteAllText(Environment.CurrentDirectory + "\\config.json", App.config.ToString());
        }
        
    }
  
}

