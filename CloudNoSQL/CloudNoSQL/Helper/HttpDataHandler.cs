using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Net;
using Java.IO;
using System.IO;

namespace CloudNoSQL.Class
{
    public class HttpDataHandler
    {
        static String stream = null;
        public HttpDataHandler() { }
        public String GetHTTPData(String urlString)
        {
            try
            {
                URL url = new URL(urlString);
                HttpURLConnection urlConnection = (HttpURLConnection)url.OpenConnection();
                if(urlConnection.ResponseCode== HttpStatus.Ok) // 200
                {
                    BufferedReader r = new BufferedReader(new InputStreamReader(urlConnection.InputStream));
                    StringBuilder sb = new StringBuilder();
                    String line;
                    while ((line = r.ReadLine()) != null)
                        sb.Append(line);
                    stream = sb.ToString();
                    urlConnection.Disconnect();
                }
            }catch(Exception ex)
            {
                System.Console.WriteLine("{0} Exception caught.", ex);
            }
            return stream;
        }

        public void PostHTTPData(String urlString, String json)
        {
            try
            {
                URL url = new URL(urlString);
                HttpURLConnection urlConnection = (HttpURLConnection)url.OpenConnection();
                urlConnection.RequestMethod = "POST";
                urlConnection.DoOutput = true;

                byte[] _out = Encoding.UTF8.GetBytes(json);
                int lenght = _out.Length;

                urlConnection.SetFixedLengthStreamingMode(lenght);
                urlConnection.SetRequestProperty("Content-Type", "application/json");
                urlConnection.SetRequestProperty("charset", "utf-8");

                urlConnection.Connect();
                try
                {
                    Stream str = urlConnection.OutputStream;
                    str.Write(_out, 0, lenght);
                }catch(Exception ex) { System.Console.WriteLine("{0} Exception caught.", ex); }

                var status = urlConnection.ResponseCode; 
            }
            catch (Exception ex) { System.Console.WriteLine("{0} Exception caught.", ex); }
        }

        public void PutHTTPData(String urlString, String json)
        {
            try
            {
                URL url = new URL(urlString);
                HttpURLConnection urlConnection = (HttpURLConnection)url.OpenConnection();
                urlConnection.RequestMethod = "PUT";
                urlConnection.DoOutput = true;

                byte[] _out = Encoding.UTF8.GetBytes(json);
                int lenght = _out.Length;

                urlConnection.SetFixedLengthStreamingMode(lenght);
                urlConnection.SetRequestProperty("Content-Type", "application/json");
                urlConnection.SetRequestProperty("charset", "utf-8");

                urlConnection.Connect();
                try
                {
                    Stream str = urlConnection.OutputStream;
                    str.Write(_out, 0, lenght);
                }
                catch (Exception ex) { System.Console.WriteLine("{0} Exception caught.", ex); }
                var status = urlConnection.ResponseCode;
            }
            catch (Exception ex) { System.Console.WriteLine("{0} Exception caught.", ex); }
        }

        public void DeleteHTTPData(String urlString, String json)
        {
            try
            {
                URL url = new URL(urlString);
                HttpURLConnection urlConnection = (HttpURLConnection)url.OpenConnection();
                urlConnection.RequestMethod = "DELETE";
                urlConnection.DoOutput = true;

                byte[] _out = Encoding.UTF8.GetBytes(json);
                int lenght = _out.Length;

                urlConnection.SetFixedLengthStreamingMode(lenght);
                urlConnection.SetRequestProperty("Content-Type", "application/json");
                urlConnection.SetRequestProperty("charset", "utf-8");

                urlConnection.Connect();
                try
                {
                    Stream str = urlConnection.OutputStream;
                    str.Write(_out, 0, lenght);
                }
                catch (Exception ex) { System.Console.WriteLine("{0} Exception caught.", ex); }
                var status = urlConnection.ResponseCode;
            }
            catch (Exception ex) { System.Console.WriteLine("{0} Exception caught.", ex); }
        }
    }
}