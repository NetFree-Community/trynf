using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wTryNetFree
{

    static class Requset
    {

        internal static string Send(string urlParameters, string method, string body = "")
        {
            //string apiAddress = "http://localhost:8080/api/";
            string apiAddress = "http://items.cf:5938/api/";
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiAddress + urlParameters);
                request.Method = method;

                if(method == "POST")
                {
                    var data = Encoding.UTF8.GetBytes(body);
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    string data = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();
                    return data;
                }
                else return "Failed";
            }
            catch(Exception e)
            {
                return "Failed";
            }
        }

    }
}
