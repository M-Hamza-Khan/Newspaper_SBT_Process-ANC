using NewsPaperSBT.Models.BAL;
using NewsPaperSBT.Models.Core;
using NewsPaperSBT.Models.PartialClasses;
using NewsPaperSBT.Models.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NewsPaperSBT.Models.DAL;

using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace NewsPaperSBT.Controllers
{

 [SessionTimeout(RoleName = "Admin,Finance")]
    public class AdminController : Controller
    {
        BAL_User BAL = new BAL_User();
        BAL_Vendor objvendor = new BAL_Vendor();
        BAL_Terms objTerms = new BAL_Terms();
        DAL dal = new DAL();
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult Index()
        {
            var user = NewspaperSBTSession.CurrentUser;
            if (user == null)
            {
                RedirectToAction("index", "login");
            }
            return View();
        }
        // to do this method is used for band user to login

        [AcceptVerbs("Get", "Post")]
        public JsonResult BandUserToLogin(int Userid, bool isActive)
        {
            string data = BAL.BandUserToLogin(Userid, isActive);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //todo get All users 
        [HttpPost]
        public JsonResult GetAllUsers()
        {
            var user = NewspaperSBTSession.CurrentUser;
            if (user == null)
            {
                RedirectToAction("index", "login");
            }


            string JSONString = "";
            var data = BAL.GetAllUsers();
            JSONString = JsonConvert.SerializeObject(data);
            return Json(JSONString, JsonRequestBehavior.AllowGet);
        }

        //todo get all system users
        [HttpPost]
        public JsonResult Getsystemusers()
        {
            var user = NewspaperSBTSession.CurrentUser;
            if (user == null)
            {
                RedirectToAction("index", "login");
            }


            string JSONString = "";
            var data = BAL.GetSystemusers();
            JSONString = JsonConvert.SerializeObject(data);
            return Json(JSONString, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Getsystemusersbyid(int userid)
        {
            var user = NewspaperSBTSession.CurrentUser;
            if (user == null)
            {
                RedirectToAction("index", "login");
            }


            string JSONString = "";
            var data = BAL.GetSystemusersbyid(userid);
            if (data != null) data[0].Password = Encryption.Decrypt(data[0].Password);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //todo this method create a user on given Details
        [HttpPost]
        public JsonResult Adduser(PC_User model)
        {
            var user = NewspaperSBTSession.CurrentUser;
            if (user == null)
            {
                RedirectToAction("index", "login");
            }
            var data = BAL.Createuser(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //todo this method update a user on given Details
        [HttpPost]
        public JsonResult updateuser(PC_User model)
        {
            var user = NewspaperSBTSession.CurrentUser;
            if (user == null)
            {
                RedirectToAction("index", "login");
            }
            var data = BAL.Updateuser(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //todo this method will get all users by id 


        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public ActionResult EditVendor(int? userid)
        {
            var user = NewspaperSBTSession.CurrentUser;
            if (user == null)
            {
                return RedirectToAction("index", "login");
            }
            object data = null;
            if (!string.IsNullOrEmpty(userid.ToString()))
            {

                data = BAL.GetUsersByid(userid ?? 1);
            }
            return View(data);

        }


        [HttpPost]
        public JsonResult UpdateVendor(PC_Vendor model)
        {
            var user = NewspaperSBTSession.CurrentUser;

            if (user == null)
            {
                RedirectToAction("index", "login");
            }
            var data = objvendor.updateVendor(model);
            var JSONString = JsonConvert.SerializeObject(data);

            return Json(JSONString, JsonRequestBehavior.AllowGet);
        }
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]

        public ActionResult termsandconditions()
        {
            var user = NewspaperSBTSession.CurrentUser;
            if (user == null)
            {
              RedirectToAction("index", "login");
            }

            return View();
        }
        [HttpPost]
        public ActionResult termsandconditions(HttpPostedFileBase file)
        {
             if (file!=null)
            {
                if (Path.GetExtension(file.FileName) == ".pdf")
                {

                    string filename = Path.GetFileName(file.FileName);
                    string contentType = file.ContentType;
                    using (Stream fs = file.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            dal.saveterms(bytes);

                        }

                    }
                }
                else {
                    ViewBag.FileStatus = "Invalid file format.";

                }

                return View();


            }
            return View();

        }

        [HttpPost]
        public JsonResult getterms()
        {
            var user = NewspaperSBTSession.CurrentUser;
            if (user == null)
            {
               RedirectToAction("index", "login");
            }


            string JSONString = "";
            var data = dal.getterms();
            JSONString = JsonConvert.SerializeObject(data);
            return Json(JSONString, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public void DownLoadFile(int id)
        {


            List<Sp_gettermsandconditionsfiles_Result> ObjFiles = dal.gettermswithfile();

            var FileById = (from FC in ObjFiles
                            where FC.versionid.Equals(id)
                            select new { FC.fileName, FC.pdfsrc }).ToList().FirstOrDefault();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = ("application/pdf");
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileById.fileName+""+id+".pdf");
            Response.BinaryWrite(FileById.pdfsrc);
           
            Response.Flush();
            Response.End();

        }



        [HttpPost]
        public JsonResult UpdateTerms(int versionid,Boolean isactive)
        {
            var user = NewspaperSBTSession.CurrentUser;
            if (user == null)
            {
                //RedirectToAction("index", "login");
            }


            var data = objTerms.updateterms(versionid,isactive);
            if (data != null) {
                if ( data.MessageStatus==0)
                {
                    if (isactive)
                    {
                        ChangeTermsAndconditions(versionid);

                    }
                }
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        
            public JsonResult getvisbledocument()
             {
           
            List<Sp_gettermsandconditions_Result> ObjFiles = dal.getterms();

            var data = (from FC in ObjFiles
                            where FC.isActive.Equals(true)
                            select new { FC.fileName, FC.versionid }).ToList().FirstOrDefault();

            if (data != null) {

                return Json(data, JsonRequestBehavior.AllowGet);

            }
            return Json(data, JsonRequestBehavior.AllowGet);



        }
        public void ChangeTermsAndconditions(int versionid) {
            //delete all files first
            try { 
            string path = Server.MapPath("/Content/Data");

            DirectoryInfo directory = new DirectoryInfo(path);

            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                dir.Delete(true);
            }
            }
            catch (Exception e) { }

            //add newly visible files 
            List<Sp_gettermsandconditionsfiles_Result> ObjFiles = dal.gettermswithfile();

            var FileById = (from FC in ObjFiles
                            where FC.versionid.Equals(versionid)
                            select new { FC.fileName, FC.pdfsrc }).ToList().FirstOrDefault();
            System.IO.File.WriteAllBytes(Server.MapPath("/Content/Data/TermsAndConditions_V"+versionid+".pdf"), FileById.pdfsrc);

        }
    }
}

 