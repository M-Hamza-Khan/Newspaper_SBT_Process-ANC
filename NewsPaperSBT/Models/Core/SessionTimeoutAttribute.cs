using NewsPaperSBT.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsPaperSBT.Models.Core
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public string RoleName { get; set; }
        //ATI_ElevatedTitle_PortalEntities1 db = new ATI_ElevatedTitle_PortalEntities1();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (NewspaperSBTSession.CurrentUser == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Index");
                return;
            }
            else
            {
                if (!UserRepositories.CheckAccess(RoleName, NewspaperSBTSession.CurrentUser))
                {
                    //Don't Have Access
                    filterContext.Result = new RedirectResult("~/Home/NoAccess");
                    return;
                }
                //if (RoleName == "Admin")
                //{
                //    if (!user.tbl_rw_usersAuthentication.FirstOrDefault().SettingsAdmin)
                //    {
                //        //Don't Have Access
                //        filterContext.Result = new RedirectResult("~/Home/NoAccess");
                //        return;
                //    }
                //}
                //else if (RoleName == "CalcFees")
                //{
                //    if (!user.tbl_rw_usersAuthentication.FirstOrDefault().CalculateFees)
                //    {
                //        //Don't Have Access
                //        filterContext.Result = new RedirectResult("~/Home/NoAccess");
                //        return;
                //    }
                //}
                //else if (RoleName == "OrderTitle")
                //{
                //    if (!user.tbl_rw_usersAuthentication.FirstOrDefault().OrderTitle)
                //    {
                //        //Don't Have Access
                //        filterContext.Result = new RedirectResult("~/Home/NoAccess");
                //        return;
                //    }
                //}
                //else if (RoleName == "OrderTracking")
                //{
                //    if (!user.tbl_rw_usersAuthentication.FirstOrDefault().OrderTracking)
                //    {
                //        //Don't Have Access
                //        filterContext.Result = new RedirectResult("~/Home/NoAccess");
                //        return;
                //    }
                //}
            }
            base.OnActionExecuting(filterContext);
        }
    }
}