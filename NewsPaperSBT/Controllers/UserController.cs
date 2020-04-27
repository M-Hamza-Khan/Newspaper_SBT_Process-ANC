using NewsPaperSBT.Models.BAL;
using NewsPaperSBT.Models.Core;
using NewsPaperSBT.Models.PartialClasses;
using NewsPaperSBT.Models.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NewsPaperSBT.Controllers
{
    public class UserController : Controller
    {

        BAL_User sup = new BAL_User();
        // GET: User
        public ActionResult Index()
        {
            Logout();
            ViewBag.returnip = Utility.GetIPAddresses();
            return View();
        }


        public ActionResult Logout()
        {
            var user = NewspaperSBTSession.CurrentUser;

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            FormsAuthentication.SignOut();

            Session.Abandon();
            Session.RemoveAll();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddHours(-1));
            Response.Cache.SetNoStore();


            return Json(new { url = Url.Action("Index", "Login") });
        }

        ////todo savingUserData
        //public ActionResult AddUser(PC_Userdetail Model)
        //{

    
         

        //    string JSONString = "";
        //    //todo Add User
        //   var xuser = sup.AddUser(Model.user);
        //    //todo userSubmit Successfully
        //    if (xuser != null)
        //    {
        //        //todo addregion
        //        var xregion = sup.adduserRegion(Model.userregion);
        //        //todo check userregion Add Successfully
        //        if (xuser != null) {
        //            //todo add currently added user data to session
        //            NewspaperSBTSession.CurrentUser = xuser;

        //        }
        //    }
        //    //todo if add userfailed return to view
        //    else {
        //        return View();
        //    }
        //    JSONString = JsonConvert.SerializeObject(xuser);

        //    return RedirectToAction("index", "vendor");

        //}


    }
}