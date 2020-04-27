using NewsPaperSBT.Models.BAL;
using NewsPaperSBT.Models.Core;
using NewsPaperSBT.Models.DAL;
using NewsPaperSBT.Models.PartialClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPaperSBT.Models.DAL
{
    public class DAL
    {
        NewsPaperSBTEntities db = new NewsPaperSBTEntities();
        //todo this method will get email template by tempname
        public EmailTemplates getEmailTemplate(string TemplateName)
        {
            return db.EmailTemplates.Where(x => x.Template == TemplateName).SingleOrDefault();
        }
        //todo this method is used for inactive user by id 
        public string BandUserToLogin(int Userid, bool isActive)
        {
            string result = "Failed";
            var us = db.user.Where(x => x.Userid == Userid).FirstOrDefault();
            if (us != null)
            {
                us.isActive = isActive;
                db.Entry(us).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                result = "Success";
            }
            return result;
        }

        //todo check is email exist already
        public bool EmailExists(string email)
        {
            int Count = db.user.Where(x => x.Email == email).Count();

            return Count > 0;
        }

        //todo get email setting 
        public EmailSettings EmailSettings()
        {
            return db.EmailSettings.OrderByDescending(x => x.Id).FirstOrDefault();
        }
        //todo this method will validate email and password
        public user login(string userName, string password)
        {
            return db.user.Where(x => x.Email == userName && x.Password == password).SingleOrDefault();
        }

        //todo this method will get ipdetails by city,ip,userid
        public UserRegion getipdetails(string city, string ip, int userid)
        {
            return db.UserRegion.Where(x => x.City == city && x.IP == ip && x.Userid == userid && x.Isblock == false).SingleOrDefault();
        }


        //this method check either provided upccode is recommanded or not
        public Boolean checkIsRecommandedUpc(string Code)
        {
            Boolean retval = false;
            try { retval = Boolean.Parse(db.UPCMaster.Where(x => x.UPC_Code == Code && x.isrecom == true).SingleOrDefault().isrecom.ToString()); } catch (Exception e) { retval = false; }
            return retval;

        }
        
        //edit by eu
      public void UpdateUserAcount(int userid)
        {
            db.UpadteUserAcountType(userid);
            db.SaveChanges();
        }
        //add docusign details 
        public Docusign adddocusigndetails(PC_Terms param)
        {
            Docusign doc = new Docusign();
            doc.userid = Convert.ToInt32(Shared.NewspaperSBTSession.CurrentUser.Userid);
            doc.name = param.Name;
            doc.pdfVersion = param.versionid;
            doc.createdDate = DateTime.Now;
            db.Entry(doc).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return doc;
        }
        //todo this method will generate new user 
        public user CreateUser(PC_User model)
        {
            if (model.AccountTypeid == 0) { model.AccountTypeid = 3; }
            user us = new user();
            us.Fullname = model.Fullname;
            us.Email = model.Email;
            us.Password = model.Password;
            us.isActive = true;
            us.CreatedDate = DateTime.Now;
        
            us.AcountType = model.AccountTypeid;
            db.Entry(us).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();


            return us;
        }
        //todo get all users
        public List<Sp_GetAllUsers_Result> GetAllUsers() {

            return db.Sp_GetAllUsers().ToList();
        }
        //todo get all system users only
        public List<Sp_systemusers_Result> Getsystemusers()
        {

            return db.Sp_systemusers().ToList();
        }



        //todo get all system users only by id 
        public List<Sp_systemusersbyid_Result> systemusersbyid(int userid)
        {

            return db.Sp_systemusersbyid(userid).ToList();
        }

        //todo get usernamebyemail
        public string getUserNameByemail(string email)
        {
            var role = db.user.Where(x => x.Email == email).First().Fullname;

            return role;
        }
        //todo get usrdetail by id
        public List<Sp_GetAllUsersByUserId_Result> GetAllUserbyid(int userid)
        {

            return db.Sp_GetAllUsersByUserId(userid).ToList();
        }

        //get role by id
        public string getRoleName(int roleID)
        {
            //edit by eu
            if (roleID == 0) {
                roleID = 3;
            }
            var role = db.Roles.Where(x => x.RoleId == roleID).FirstOrDefault().RoleDesc;
            return role;
        }

        //todo add vendor region details
        public UserRegion AddUserRegion(PC_UserRegion model)
        {
            UserRegion us = new UserRegion();
            us.Regionid = model.Regionid;
            us.IP = model.IP;
            us.Country = model.Country;
            us.Userid = model.Userid;
            us.State = model.State;
            us.City = model.City;
            us.Isblock = model.Isblock;
            db.Entry(us).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();


            return us;
        }

        //todo this method will update password 
        public user updateDatabyemail(string email, string password) {


            var data = db.user.Where(x => x.Email == email).FirstOrDefault();
            if (data != null)
            {
                data.Password = password;

                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return data;
            }
            return data;
        }
        //todo this method will update document status of vendor 
        public Vendor updatedocumentstatus(PC_Vendor model)
        {


            var data = db.Vendor.Where(x => x.Userid == model.UserId).FirstOrDefault();
            if (data != null)
            {
                data.DocumentSign = model.DocumentSigned;

                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return data;
            }
            return data;
        }


        //todo this method will update password 
        public user UpdateUserData(PC_User Model)
        {


            var data = db.user.Where(x => x.Userid == Model.Userid).FirstOrDefault();
            if (data != null)
            {
                if (Model.Password!=null){
                    data.Password = Model.Password;
                }
                data.Fullname = Model.Fullname;
                data.AcountType = Model.AccountTypeid;
                data.ModifiedDate = DateTime.Now;

                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return data;
            }
            return data;
        }

        //this method set new password to email
        public user SetPassword(string email, string password)
        {


            var data = db.user.Where(x => x.Email == email).FirstOrDefault();
            if (data != null)
            {
                data.Password = password;

                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return data;
            }
            return data;
        }
        public Vendor UpdateVendor(PC_Vendor Model)
        {
           
      var data=   db.Vendor.Where(x => x.Vendorid == Model.Vendorid).FirstOrDefault();
            if (data != null)
            {
                data.VendorName = Model.VendorName;
                data.Contactname = Model.Contactname;
                data.Street = Model.Street;
                data.State = Model.State;
                data.City = Model.City;
                data.Phone = Model.Phone;
                data.ZipCode = Model.ZipCode;

                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
               
            }
            return data;
        }
   

        //add vendor details
        public Vendor AddVendor(PC_Vendor model)
        {
            Vendor vendor = new Vendor();
            vendor.Contactname = model.Contactname;
            vendor.VendorName = model.VendorName;
            vendor.City = model.City;
            vendor.upccode = model.upccode;
            vendor.Phone = model.Phone;
            vendor.Street = model.Street;
            vendor.DocumentSign = model.DocumentSigned;
            vendor.ZipCode = model.ZipCode;
            vendor.Email = model.Email;
            vendor.State = model.State;
            vendor.ContractDate = DateTime.Now;
            vendor.IsActive = model.IsActive;
            vendor.Approvedby = model.Approvedby;
            vendor.CreatedDate = DateTime.Now;
           vendor.Userid = model.UserId;

            db.Entry(vendor).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();


            return vendor;
        }
        // todo this method will get user standing setup
        public Boolean checkisuserBlocked(string Sessionid)
        {
            Boolean returnval = false;
            try { int? a = db.sp_checkisuserblocked(Sessionid).FirstOrDefault();
                returnval = Convert.ToBoolean(Convert.ToByte(a.ToString()));
                return returnval;
            }
            catch (Exception e) {

                returnval = false;
            } 

            return false;
        }


        //todo this method will get u
        public string getuserProfileStatus(int? userid)
        {
            return db.sp_userProfileStatus(userid).SingleOrDefault();

        }
        //todo this method wiill is upccode is valid and block user after 3 attemps
        public dynamic CheckUpcVerification(string Code, int? Atempt,string sessionid)
        {

            //var result = new System.Data.Entity.Core.Objects.ObjectParameter("Result", 0);
         var a =    db.sp_CheckUpcVerfication(Code, Atempt, sessionid,"").ToList();
            return a;
            

        }

        //save pdf

        public terms saveterms(byte[] file) {
            terms TERM = new terms();
            TERM.datecreated = DateTime.Now;
            TERM.isactive = false;
            TERM.pdfsrc = file;
            TERM.fileName = "TermsAndConditions_V";
            db.Entry(TERM).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return TERM;


        }


        //get terms

        public List<Sp_gettermsandconditions_Result> getterms()
        {
         return   db.Sp_gettermsandconditions().ToList();
        }


        //get terms

        public List<Sp_gettermsandconditionsfiles_Result> gettermswithfile()
        {
            return db.Sp_gettermsandconditionsfiles().ToList();
        }
        //todo this method will update terms and replace updated on data folder and is available for users 
        public terms updateterms(int versionid, Boolean isactive)
        {

            //this will update all active document to false then we will proceed further process
            var data = db.terms.Where(x => x.isactive== true).FirstOrDefault();
            if (data != null)
            {
                data.isactive = false;

                db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
               
            }
            //this will update document  with desired document visiblity
            var data1 = db.terms.Where(x => x.versionid == versionid).FirstOrDefault();
            if (data1 != null)
            {
                data1.isactive = isactive;

                db.Entry(data1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }

            return data1;

        }

        //public LoginAttempt BlockUserIp()
        //{
        //    LoginAttempt us = new LoginAttempt();
        //    us.IP = Utility.GetIPAddresses();
        //    us.AttemptDate = DateTime.Now;

        //    db.Entry(us).State = System.Data.Entity.EntityState.Added;
        //    db.SaveChanges();


        //    return us;
        //}

    }
}


