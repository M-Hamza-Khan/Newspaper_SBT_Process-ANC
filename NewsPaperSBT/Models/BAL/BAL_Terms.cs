using NewsPaperSBT.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPaperSBT.Models.BAL
{
    public class BAL_Terms
    {
        DAL.DAL dal = new DAL.DAL();


        //todo this method will update terms 
        public dynamic updateterms(int versionid,bool isactive) {
      var data=      dal.updateterms(versionid, isactive);
            if (data!=null) {

                return shared.returnMessageJSON("Document visiblity updated Successfully ",data.pdfsrc.ToString(), false);


            }
            return shared.returnMessageJSON("ff", data.pdfsrc.ToString(), false);

        }

    }
}