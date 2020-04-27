using NewsPaperSBT.Models.BAL;
using NewsPaperSBT.Models.Core;
using NewsPaperSBT.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Authenticator;
using System.Net;
using System.Web.Script.Serialization;
using NewsPaperSBT.Models.PartialClasses;
using Newtonsoft.Json;
using System.Web.Security;
using System.Web.UI;

namespace NewsPaperSBT.Controllers
{
    public class LoginController : Controller
    {
        shared shared = new shared();

        BAL_Login bal = new BAL_Login();
        // GET: index
        public ActionResult Index()
        {
            // Logout();

            ViewBag.returnip = Utility.GetIPAddresses();
            setmessages();
          Logout();

            return View();
        }

        public void setmessages() {
            shared.checkReturnMessages();
            ViewBag.message = "";
            ViewBag.isvalid = "";
           var messages = NewspaperSBTSession.tempdata;
            if (messages!=null) {
                ViewBag.message = messages.Message;
                ViewBag.isvalid = messages.MessageStatus;
                NewspaperSBTSession.tempdata.Message = "";
                NewspaperSBTSession.tempdata.MessageStatus = 0;
               

            }
        }
      
        //todo this method check user credentials then it trigger Method for validation user region validate
        [HttpPost]
        public ActionResult login(string email, string password)
        {
                 string resp = Request.Form["g-recaptcha-response"];
            try
            {
                Utility.IsValidCaptcha(resp);
                var xuser = bal.Login(email, password);

                //create session
                NewspaperSBTSession.CurrentUser = xuser;

                if (xuser.isActive == false)
                {
                
                    shared.setReturnMessages("", "you are banned to access,<br> please contact ANC Vendor Connect department  ", true);
               
                    return RedirectToAction("index", "login");

                }

                //   return RedirectToAction("Index", "Admin");
                return RedirectToAction("UserRegion", "Login");
            }
            catch (Exception exp)
            {
                shared.setReturnMessages("Login Failed", exp.Message, true);
                return RedirectToAction("index", "login");
            }


          
        }

        public ActionResult Authentication()
        {
            var user = NewspaperSBTSession.CurrentUser;
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (user.Userid.ToString() != null)
            {
                TwoFactorAuthenticator tf = new TwoFactorAuthenticator();
                var secretkey = Encryption.Randomkey(8);
                secretkey = secretkey + user.Fullname;
                user.tokken = secretkey;
                var setupInfo = tf.GenerateSetupCode("NewsPaperSBT.com", user.Fullname, secretkey, false, 100);
                ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                ViewBag.SetupCode = setupInfo.ManualEntryKey;
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult Authentication(int tokken)
        {
            var user = NewspaperSBTSession.CurrentUser;
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            TwoFactorAuthenticator tf = new TwoFactorAuthenticator();
            bool isValid = tf.ValidateTwoFactorPIN(user.tokken.ToString(), tokken.ToString());
            if (isValid)
            {
             
                string PageName = bal.getuserProfileStatus(user.Userid);
                if (!string.IsNullOrEmpty(PageName))
                {

                return RedirectToAction("Index", PageName);
                   
                }
            }
            else {
                return RedirectToAction("Authentication", "login");

            }
            return null;

        }

        //todo this method validate user ip details then trigger method for email otp
        public ActionResult UserRegion()
        {
           

            try
            {
                if (NewspaperSBTSession.OTP == null)
                {
                    BAL_Email Email = new BAL_Email();
                    PC_OTP objOTP = new PC_OTP();
                    objOTP.OPTP = bal.getOTP();
                    objOTP.OPTCreatedDate = DateTime.Now;
                    NewspaperSBTSession.OTP = objOTP;
                    PC_User xuser = new PC_User();
                    xuser.OTP = objOTP.OPTP;
                    xuser.Email = NewspaperSBTSession.CurrentUser.Email;
                    xuser.Fullname = NewspaperSBTSession.CurrentUser.Fullname;
                    //for testin
                    Email.GetEmailTemplate("OTP", xuser);
                }
            }
            catch (Exception e)
            {
                shared.setReturnMessages("Login Failed", e.Message, true);
                return RedirectToAction("Index", "Login");


            }
            return RedirectToAction("EmailOTP", "login");
            // var user = NewspaperSBTSession.CurrentUser;
            // if (user == null)
            // {
            //     return RedirectToAction("Index", "Login");
            // }
            // var data=  Utility.getuserRegiondetail();

            //if (data.ip!="") {

            //    var result=    bal.getipdetails(data.city, data.ip,NewspaperSBTSession.CurrentUser.Userid);
            //     if (result!=null) {


            //         return RedirectToAction("EmailOTP", "login");


            //    }

            //    else {
            //         ViewBag.currentip = data.ip;
            //         return View();

            //     }
            // }
            // return View();

        }
        //todo this method will logout current session
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
  
            Session.Abandon();
            Session.RemoveAll();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddHours(-1));
            Response.Cache.SetNoStore();


            return Json(new { url = Url.Action("Index", "Login") });
        }
        //todo this method send an Email for OPT Verification
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult EmailOTP()
        {
            var user = NewspaperSBTSession.CurrentUser;
            if (user != null) {
                ViewBag.email = user.Email;
            }
            //this is old
            //  if (user == null || NewspaperSBTSession.OTP ==null)

            //edit by eu  this is new
            if (user == null || NewspaperSBTSession.OTP==null)
            {
                return RedirectToAction("Index", "Login");
            }
           

            return View();
        }

        //update password by email
        [AcceptVerbs("Get", "Post")]
        public JsonResult setPassword(string email, string Password)
        {
            BAL_User obj = new BAL_User();
            var data = obj.SetPassword(email, Password);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        //todo this method Generate Otp code and Email to responsible user 
        [HttpPost]
        public ActionResult EmailOTP(string OTP)
        {


            var user = NewspaperSBTSession.CurrentUser;

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var objOTP = NewspaperSBTSession.OTP;
            objOTP.attemp += 1;
            TimeSpan ts = DateTime.Now - objOTP.OPTCreatedDate;

            if (ts.TotalMinutes > 10 || objOTP.attemp > 4)
            {
                ViewBag.Message = Messages.OTPExpireDescription;
                ViewBag.MessageTitle = "1";
                NewspaperSBTSession.OTP = null;
                return View();

            }
            if (OTP == objOTP.OPTP)
            {

                NewspaperSBTSession.OTP = null;


                string PageName = bal.getuserProfileStatus(user.Userid);

                if (!string.IsNullOrEmpty(PageName))
                {

                    return RedirectToAction("Index", PageName);

                }

             
                if (user.AcountType == 1 || user.AcountType == 2) //admin
                {
                    return RedirectToAction("Index", "Admin");


                }
               
               

            }
            shared.setReturnMessages("Invalid OTP ", "", true);
            ViewBag.Message = "Invalid OTP";
            ViewBag.MessageTitle = "";
            return View();
        }
        //todo 
        public ActionResult ForgetPassword() {

            return View();


        }

        ////todo this method will generate a new password and mail  user email address
        //[HttpPost]
        //public ActionResult ForgetPassword(string email)
        //{

        //    BAL_User obj = new BAL_User();
        //    var data = obj.passwordReset(email);
        //        if (data) {
        //        return RedirectToAction("index","login");
        //         }
        //    return View();


        //}
        //todo this method will generate a new password and mail  user email address
        [HttpPost]
        public JsonResult ForgetPassword(string email)
        {
            bool result = false;
            BAL_User obj = new BAL_User();
            var data = obj.passwordReset(email);
            if (data)
            {
                result = true;
                //return RedirectToAction("index", "login");
            }
            //return View();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ResendOTP()
        {
            

            try
            {
                var XobjOTP = NewspaperSBTSession.OTP;
                XobjOTP.attemp += 1;
                TimeSpan ts = DateTime.Now - XobjOTP.OPTCreatedDate;

                if (ts.TotalMinutes > 10)
                {
                    return Json(shared.returnMessageJSON(Messages.OTPExpireDescription, "", true), JsonRequestBehavior.AllowGet);

                }
                NewspaperSBTSession.OTP = null;
              
                    BAL_Email Email = new BAL_Email();
                    PC_OTP objOTP = new PC_OTP();
                    objOTP.OPTP = bal.getOTP();
                    objOTP.OPTCreatedDate = DateTime.Now;
                    NewspaperSBTSession.OTP = objOTP;
                    PC_User xuser = new PC_User();
                    xuser.OTP = objOTP.OPTP;
                    xuser.Email = NewspaperSBTSession.CurrentUser.Email;
                    xuser.Fullname = NewspaperSBTSession.CurrentUser.Fullname;
                    //for testing
                    Email.GetEmailTemplate("OTP", xuser);
                
            }
            catch (Exception )
            {
                return Json(shared.returnMessageJSON("System Error Try Again..","",true), JsonRequestBehavior.AllowGet);



            }
         
            return Json(shared.returnMessageJSON(Messages.OTPResendSucceesDescription, "", false), JsonRequestBehavior.AllowGet);

        }
        //public JsonResult checkipblocked(string Code, int Atempt)
        //{
        //    string ip = Utility.GetIPAddresses();
        //    var data = bal.CheckUpcVerification(Code, Atempt, ip);
        //    return Json(data, JsonRequestBehavior.AllowGet);

        //}

        // todo this method check either Posted Otp valid or invalid then trigger view according to roles


        //todo this method will trigger when user blocked for Signup
        public ActionResult Eror407() {

            return View();
        }
        public void setError() {
            var tmp = NewspaperSBTSession.tempdata;
          
            if (tmp != null)
            {

                TempData["MessageTitle"] = tmp.MessageTitle;
                TempData["Message"] = tmp.Message;

              

            }

        }

        //todo this method will triggered after vendor registered succeesfully
        public ActionResult staticMessage() {
         
            return View();
        }
    }
}