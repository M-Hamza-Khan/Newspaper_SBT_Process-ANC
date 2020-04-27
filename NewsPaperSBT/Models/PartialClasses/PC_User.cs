using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPaperSBT.Models.PartialClasses
{
    public class PC_User
    {

        public int Userid { get; set; }
        public string Fullname { get; set; }
        public string FirstsName { get; set; }
        public string LastName { get; set; }
        public int Tokken { get; set; }
        public string OTP { get; set; }

        public string Email { get; set; }
        public int AccountTypeid { get; set; }
        public string Password { get; set; }
        public int  Roll { get; set; }
        public Boolean isActive { get; set; }
        public DateTime  CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}