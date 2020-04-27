using NewsPaperSBT.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPaperSBT.Models.BAL
{

    public class BAL_Finance
    {
        DAL.DAL dal = new DAL.DAL();

        //todo this method retrun true if upc is reccomanded
        public Boolean checkIsRecommandedUpc(string UpcCode) {
            return dal.checkIsRecommandedUpc(UpcCode);



        }
    }
}