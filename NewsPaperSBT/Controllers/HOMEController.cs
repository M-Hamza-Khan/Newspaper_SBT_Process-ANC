using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace NewsPaperSBT.Controllers
{
    public class HOMEController : Controller
    {
        // GET: HOME
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]

        public ActionResult NoAccess()
        {
            return View();
        }
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]

        public ActionResult PageNotfound()
        {
            return View();
        }
    }
}