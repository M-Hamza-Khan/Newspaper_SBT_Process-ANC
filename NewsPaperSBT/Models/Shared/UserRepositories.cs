using NewsPaperSBT.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPaperSBT.Models.Shared
{
    public class UserRepositories
    {
        public static bool CheckAccess(string RoleName, user user)
        {
            DAL.DAL DAL = new Models.DAL.DAL();
            string UserRole = DAL.getRoleName(user.AcountType ?? 3);


            if (RoleName.Contains(UserRole))
            {
                return true;
            }

            return false;
        }
    }
}
