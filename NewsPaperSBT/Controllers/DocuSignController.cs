using NewsPaperSBT.Models.BAL;
using NewsPaperSBT.Models.Core;
using NewsPaperSBT.Models.PartialClasses;
using NewsPaperSBT.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsPaperSBT.Controllers
{
[SessionTimeout(RoleName = "Vendor")]

    public class DocuSignController : Controller
    {
        // GET: DocuSign
        BAL_DocuSign bal = new BAL_DocuSign();

        public ActionResult Index()
        {
            var User = NewspaperSBTSession.CurrentUser;
            if (User == null)
            {
                return RedirectToAction("index", "login");
            }

            return View();

        }

        [HttpPost]
        //add 
        public JsonResult TermsAncConditions(PC_Terms param) {
            var User = NewspaperSBTSession.CurrentUser;
            if (User == null)
            {
                RedirectToAction("index", "login");
            }
            var data = bal.AdddocusignDetails(param);
            BAL_Vendor BALVENDOR =  new  BAL_Vendor();
            PC_Vendor OB = new PC_Vendor();
            OB.UserId = User.Userid;
            OB.DocumentSigned = "Document Signed";
            BALVENDOR.Updatedocumentstatus(OB);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


    
    }
}