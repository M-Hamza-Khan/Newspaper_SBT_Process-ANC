using NewsPaperSBT.Models.BAL;
using NewsPaperSBT.Models.PartialClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Serialization;

namespace NewsPaperSBT.Models.Core
{
    public static class Utility
    {
        public static string GetIPAddresses()
        {
            string Address;
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader strmrd = new StreamReader(response.GetResponseStream()))
            {

                Address = strmrd.ReadToEnd();

            }

            int first = Address.IndexOf("Address: ") + 9;
            int last = Address.IndexOf("</body>");
            Address = Address.Substring(first, last - first);
            return Address;

        }



        public static dynamic getuserRegiondetail()
        {
            string json = "";
            var ip = "182.189.22.51";
            //var access_key = "6LdcXOcUAAAAADFzNQKUs86zEaoEq1NtQP8l140f";
            var access_key = "   6LdcXOcUAAAAADFzNQKUs86zEaoEq1NtQP8l140f";
            var url = "http://api.ipstack.com/" + ip + "?access_key=" + access_key;
            using (var webClient = new System.Net.WebClient())
            {
                json = webClient.DownloadString(url);
                // Now parse with JSON.Net
            }
            var jss = new JavaScriptSerializer();
            var DATA = JsonConvert.DeserializeObject<PC_apiUserRegion>(json);



            return DATA;
        }

        public static bool IsValidCaptcha(string resp)
        {
            try
            {
                //6LdfpOUUAAAAACyQSQx1wiu4ajqCSrMMUUPTZQB_
                string acceeskey = ConfigurationManager.AppSettings["captchaserverkey"].ToString();
                var req = (HttpWebRequest)WebRequest.Create
             ("https://www.google.com/recaptcha/api/siteverify?secret=" + acceeskey + "&response=" + resp + "");
                //Google recaptcha Response
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        // Deserialize Json
                        CaptchaResult data = js.Deserialize<CaptchaResult>(jsonResponse);
                        if (Convert.ToBoolean(data.success))
                        {
                            return true;
                        }
                        else
                        {


                            throw new Exception("Invalid Captcha Try Again..");

                        }
                    }
                }

            }

            catch (Exception e)
            {

                throw new Exception("Invalid Captcha Try Again..");


            }
        }
    }
}