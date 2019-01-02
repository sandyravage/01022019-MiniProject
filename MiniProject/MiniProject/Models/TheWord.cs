using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using MiniProject.Extensions;
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
            "http://foaas.com/bag/JamesChurchill", "http://foaas.com/family/JamesChurchill", "http://foaas.com/flying/JamesChurchill"
            ,"http://foaas.com/horse/JamesChurchill", "http://foaas.com/no/JamesChurchill", "http://foaas.com/rtfm/:JamesChurchill",
            "http://foaas.com/tucker/JamesChurchill", "http://foaas.com/zero/JamesChurchill","http://foaas.com/shit/JamesChurchill",
            "http://foaas.com/single/JamesChurchill", "http://foaas.com/sake/JamesChurchill", "http://foaas.com/question/JamesChurchill",
            "http://foaas.com/programmer/JamesChurchill"
        };

        string[] userList =
        {
            "http://foaas.com/look/"+HttpContext.Current.User.Identity.GetUserFirstName()+"/JamesChurchill","http://foaas.com/ing/"+HttpContext.Current.User.Identity.GetUserFirstName()+"/JamesChurchill", "http://foaas.com/gfy/"+HttpContext.Current.User.Identity.GetUserFirstName()+"/:JamesChurchill",
            "http://foaas.com/equity/"+HttpContext.Current.User.Identity.GetUserFirstName()+"/JamesChurchill", "http://foaas.com/cocksplat/"+HttpContext.Current.User.Identity.GetUserFirstName()+"/JamesChurchill", "http://foaas.com/back/"+HttpContext.Current.User.Identity.GetUserFirstName()+"/JamesChurchill"
        };

        public TheWord Generate()
        {
            string t = "";
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                t = UserChecker();
            }
            else
            {
                Random r = new Random();
                t = list[r.Next(0,list.Length)];
            }
            
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
                JObject dataObject = JObject.Parse(data);
                var mTemp = dataObject.ToObject<TheWord>();
                return mTemp;
            }
            return null;
        }

        private string UserChecker()
        {
            Random r = new Random();
            int num = r.Next(0, 2);
            if (num == 0)
                return list[r.Next(0, list.Length)];
            return userList[r.Next(0, userList.Length)];
        }
    }
}