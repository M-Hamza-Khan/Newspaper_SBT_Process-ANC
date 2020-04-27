using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPaperSBT.Models.PartialClasses
{
    public class PC_UserRegion
    {

        public int Regionid { get; set; }
        public int Userid { get; set; }
        public String  IP { get; set; }
        public String  Country { get; set; }
        public  String State { get; set; }
        public String City { get; set; }
        public Boolean Isblock { get; set; }

    }
}