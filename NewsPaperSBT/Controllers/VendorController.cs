using NewsPaperSBT.Models.BAL;
using NewsPaperSBT.Models.Core;
using NewsPaperSBT.Models.DAL;
using NewsPaperSBT.Models.PartialClasses;
using NewsPaperSBT.Models.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NewsPaperSBT.Controllers
{
    public class VendorController : Controller
    {
        BAL_Vendor objbalvendor = new BAL_Vendor();
        BAL_User objbalUser = new BAL_User();

        //todo retrive Client iP and ReturnToview
        public ActionResult Index()
        {
            ViewBag.returnip = Utility.GetIPAddresses();
            return View();
        }

        [HttpPost]
        //TODO This Method will Check isCaptcha Valid then Addvendor Details,generateUser,Add UserIpDetails , Email User Credentials
        public ActionResult AddVendor(PC_VendorDetails Model)
        {
            string JSONString = "";
            try
            {
                string resp = Model.Vendor.CaptchaResponse;
                if (Utility.IsValidCaptcha(resp))
                {
                    var data = objbalvendor.AddVendor(Model);
                    JSONString = JsonConvert.SerializeObject(data);
                    NewspaperSBTSession.CurrentUser = null;
                }

            }
            catch (Exception e) {

                return Json(shared.returnMessageJSON("", "Invalid Captcha", true), JsonRequestBehavior.AllowGet);
            }
            return Json(JSONString, JsonRequestBehavior.AllowGet);

        }

        //TODO this method will check upccode and redirect if attempt >3
        public JsonResult UPCVerification(string Code, int Atempt)
        {
            var SessionID = System.Web.HttpContext.Current.Session.SessionID.ToString();

            var data = objbalvendor.CheckUpcVerification(Code, Atempt, SessionID);
       

            return Json(data, JsonRequestBehavior.AllowGet);
            
        }

        //todo this method will check is uer block for signup
        public JsonResult checkisuperBlocked()
        {
            string SessionID = System.Web.HttpContext.Current.Session.SessionID.ToString();
            var data = objbalvendor.checkisuserblocked(SessionID);
            if (data) {
                return Json(shared.returnMessageJSON("invalid", "in", Messages.Return_True), JsonRequestBehavior.AllowGet);

            }
            return      Json(shared.returnMessageJSON("invalid", "in", Messages.Return_False), JsonRequestBehavior.AllowGet);



        }

        public JsonResult getvisbledocument() {
            DAL dal = new DAL(); List<Sp_gettermsandconditions_Result> ObjFiles = dal.getterms();
            var data = (from FC in ObjFiles where FC.isActive.Equals(true)
                        select new
                        { FC.fileName, FC.versionid }).ToList().FirstOrDefault();
            if (data != null)
            { return Json(data, JsonRequestBehavior.AllowGet); }
            return Json(data, JsonRequestBehavior.AllowGet); }

        public ActionResult setss() {
            var user = NewspaperSBTSession.CurrentUser;
            if (user == null)
            {
                return RedirectToAction("index", "login");
            }
            return RedirectToAction("Eror407", "login");
        }
        [HttpGet]
        public void DownLoadFile(int versionid)
        {

            DAL dal = new DAL();
            List<Sp_gettermsandconditionsfiles_Result> ObjFiles = dal.gettermswithfile();

            var FileById = (from FC in ObjFiles
                            where FC.versionid.Equals(versionid)
                            select new { FC.fileName, FC.pdfsrc }).ToList().FirstOrDefault();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = ("application/pdf");
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileById.fileName + "" + versionid + ".pdf");
            Response.BinaryWrite(FileById.pdfsrc);

            Response.Flush();
            Response.End();

        }

        public void printview(int versionid) {

            DAL dal = new DAL();
            List<Sp_gettermsandconditionsfiles_Result> ObjFiles = dal.gettermswithfile();

            var FileById = (from FC in ObjFiles
                            where FC.versionid.Equals(versionid)
                            select new { FC.fileName, FC.pdfsrc }).ToList().FirstOrDefault();
            string path = Server.MapPath("/Content/Data/");
            string FilePath = path += FileById.fileName+""+versionid+".pdf";
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }
        }

    }
}