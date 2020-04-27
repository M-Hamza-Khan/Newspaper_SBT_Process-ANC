using NewsPaperSBT.Models.Core;
using NewsPaperSBT.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPaperSBT.Models.BAL
{
    public class BAL_Login
    {
        DAL.DAL dal =  new DAL.DAL();
        //todo this method will get user detail pass and email
        public user Login(string username, string password)
        {

            string EncryptedPassword = Encryption.Encrypt(password);
            var user = dal.login(username, EncryptedPassword);
            if (user == null)
            {
                throw new Exception(" invalid user name and password");
            }
            return user;
        }

       
       
        //todo this method gets user profile status ,e.g is he signed docusignforms , e.g user standing
        public string getuserProfileStatus(int userid)
        {


            return dal.getuserProfileStatus(userid);
        }
        //todo this method gets ipdetails by city,ip,userid
        public UserRegion getipdetails(string city, string ip,int userid)
        {

            var dtl = dal.getipdetails(city, ip,userid);

            if (dtl == null)
            {
              //  throw new Exception("data not found");
            }
            return dtl;
        }
        //todo this method create random key for otp
        public string getOTP() {

        return    Encryption.Randomkey(6);
        }

       
    }
}