using NewsPaperSBT.Models.BAL;
using NewsPaperSBT.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace NewsPaperSBT.Controllers
{
    public class FinanceController : Controller
    {
        BAL_Finance bal = new BAL_Finance();

        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        // GET: Finance
        public ActionResult Index()
        {
        var    User = NewspaperSBTSession.CurrentUser;
            if (User == null)
            {
                RedirectToAction("index", "login");
            }
         
            return View();
        }



    }
}