using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace wTryNetFree
{
    class Validation
    {
        public static bool ExpiredID(string username)
        {
            if (Regex.Match(username, "[0-9]").Length == 0) return true;
            long userId = int.Parse(Regex.Replace(username, "[A-z]+", ""));
            long JSDateNow = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            if ((userId * 60000) < JSDateNow)
            {
                return true;
            }
            return false;
        }
        public static bool BadId(string id)
        {
            //מכסה גם מקרה של כשל בתקשורת עם השרת API
            //המחלקה מחזירה Failed
            //והתוכנית ממשיכה לרוץ כדי לנסות להתחבר
            return Requset.Send(App.config.apiAddress.ToString() + "test-key/check/" + id, "GET") == "false";
        }
    }
}
