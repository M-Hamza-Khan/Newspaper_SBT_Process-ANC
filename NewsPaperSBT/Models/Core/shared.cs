using NewsPaperSBT.Models.PartialClasses;
using NewsPaperSBT.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NewsPaperSBT.Models.Core
{
    public class shared : Controller
    {
        public void setReturnMessages(string Title, string Description, bool isError)
        {
            Tempdata ob = new Tempdata();
             ob.MessageTitle = Title;
             ob.Message = Description;
            //var otp = NewspaperSBTSession.Optp;
            if (isError)
            {
                ob.MessageStatus = 1;
              ob.MessageType = "error";
            }
            else
            {
                ob.MessageStatus = 0;
                ob.MessageType = "success";

               
            }
            TempData["MessageTitle"] = ob.MessageTitle;
            TempData["Message"] = ob.Message;


            TempData["MessageStatus"] = ob.MessageStatus;
            TempData["MessageType"] = ob.MessageType;
                NewspaperSBTSession.tempdata = ob;

           // NewspaperSBTSession.Optp= otp;

        }
        public void checkReturnMessages() {
            var user = NewspaperSBTSession.tempdata;
            TempData["MessageTitle"] = "";
            TempData["Message"] = "";
            TempData["MessageStatus"] = "";
            TempData["MessageType"] = "";
            if (user!=null){
               ViewBag.a = user.MessageTitle;
                TempData["MessageTitle"] = user.MessageTitle;
            TempData["Message"] = user.Message;
            TempData["MessageStatus"] = user.MessageStatus;
             TempData["MessageType"]= user.MessageType;

            }
        }


        public static dynamic returnMessageJSON(string Title, string Description, bool isError)
        {

            int ErrorId = 0;
            string MessageType = "";

            if (isError)
            {
                ErrorId = 1;
                MessageType = "error";
            }
            else
            {
                ErrorId = 0;
                MessageType = "success";
            }

            var data = new
            {
                MessageTitle = Title,
                Message = Description,
                MessageStatus = ErrorId,
                MessageType = MessageType
            };

            return data;
        }
    }
}
