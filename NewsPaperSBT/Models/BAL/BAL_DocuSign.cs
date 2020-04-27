using NewsPaperSBT.Models.Core;
using NewsPaperSBT.Models.PartialClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPaperSBT.Models.BAL
{
    public class BAL_DocuSign
    {
        DAL.DAL dal = new DAL.DAL();

        //todo this method retrun true if upc is reccomanded
        public Boolean checkIsRecommandedUpc(string UpcCode)
        {

            return dal.checkIsRecommandedUpc(UpcCode);

        }
       

     

        //todo this method Will add details of docusign
        public dynamic AdddocusignDetails(PC_Terms param)
        {
            var data= dal.adddocusigndetails(param);
            if (data != null)
            {
                return shared.returnMessageJSON("Your Document Successfully Updated", " ", false);
            }
            return shared.returnMessageJSON("Please try again", " ", true);
        }
    }
}