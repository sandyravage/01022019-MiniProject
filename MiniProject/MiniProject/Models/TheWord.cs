using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;

namespace MiniProject.Models
{
    public class TheWord
    {
        public string Message { get; set; }
        public string Subtitle { get; set; }
        const string useragent = "Mozilla / 5.0(Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";

        string[] list =
        {
            "http://foaas.com/bag/:JamesChurchill", "http://foaas.com/family/:JamesChurchill", "http://foaas.com/flying/:from"
            ,"http://foaas.com/horse/:JamesChurchill", "http://foaas.com/no/:JamesChurchill", "http://foaas.com/rtfm/:JamesChurchill",
            "http://foaas.com/tucker/:JamesChurchill", "http://foaas.com/zero/:JamesChurchill"
        };

    public TheWord Generate()
        {
            Random r = new Random();
            string t = list[r.Next(list.Length)];
            HttpWebRequest request = WebRequest.CreateHttp(t);
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.UserAgent = useragent;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var data = new StreamReader(response.GetResponseStream()).ReadToEnd();
                if (data == "null")
                    return null;
                //string data1 = data.Substring(1, data.Length - 2);
                JObject dataObject = JObject.Parse(data);
                var mTemp = dataObject.ToObject<TheWord>();
                return mTemp;
            }
            return null;
        }
    }
}