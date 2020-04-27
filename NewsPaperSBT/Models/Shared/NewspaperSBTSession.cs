using NewsPaperSBT.Models.DAL;
using NewsPaperSBT.Models.PartialClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPaperSBT.Models.Shared
{
    public static  class NewspaperSBTSession
    {
        public static user CurrentUser
        {
            get { return (HttpContext.Current.Session["UserSession"] as user) ?? null; }

            set { HttpContext.Current.Session["UserSession"] = value; }
        }

        public static PC_OTP OTP
        {
            get { return (HttpContext.Current.Session["OTP"] as PC_OTP) ?? null; }

            set { HttpContext.Current.Session["OTP"] = value; }
        }
        public static string Upcode
        {
            get { return (HttpContext.Current.Session["Upcode"] as String) ?? null; }

            set { HttpContext.Current.Session["Upcode"] = value; }
        }
        public static Tempdata tempdata
        {
            get { return (HttpContext.Current.Session["Optp"] as Tempdata) ?? null; }

            set { HttpContext.Current.Session["Optp"] = value; }
        }
    }
}