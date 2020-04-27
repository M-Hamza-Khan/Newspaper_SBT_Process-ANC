using NewsPaperSBT.Models.Core;
using NewsPaperSBT.Models.DAL;
using NewsPaperSBT.Models.PartialClasses;
using NewsPaperSBT.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace NewsPaperSBT.Models.BAL
{
    public class BAL_User
    {
        Encryption objencrp = new Encryption();
        BAL_Email objEmail = new BAL_Email();
        DAL.DAL dal = new DAL.DAL();


        // todo this method add userIp details
        public dynamic adduserRegion(PC_UserRegion Model) {

            try { 
            if (Model.IP != "" && Model.Country != "" && Model.City != "")
            {

                var data = dal.AddUserRegion(Model);
                return data;

            }
            else {


                    return shared.returnMessageJSON(Messages.SignUpInValidDataTitle, Messages.SignUpInValidDataDescription, true);
            }

        }
            catch (Exception E) {

                return shared.returnMessageJSON(E.Data.ToString(), E.Message.ToString(), true);

            }
}

        // to do this method is used for band user to login
        public string BandUserToLogin(int Userid, bool isActive)
        {
            string result = "Success";
            if (Userid == NewspaperSBTSession.CurrentUser.Userid) return "You cannot ban to yourself!";
            if (isActive == true)
            {
                dal.BandUserToLogin(Userid, false);
            }
            else
            {
                dal.BandUserToLogin(Userid, true);
            }
            return result;
        }
        // todo this method will return All users
        public List<Sp_GetAllUsers_Result> GetAllUsers() {

            return dal.GetAllUsers();
        }
        // todo this method will return system users
        public List<Sp_systemusers_Result> GetSystemusers()
        {
            return dal.Getsystemusers();
        }

        // todo this method will return system users
        public List<Sp_systemusersbyid_Result> GetSystemusersbyid(int userid)
        {
            return dal.systemusersbyid(userid);
        }

        //todo this method will return all users by id
        public List<Sp_GetAllUsersByUserId_Result> GetUsersByid(int userid)
        {

            return dal.GetAllUserbyid(userid);
        }

        //todo this method will generate user and send details to coresponding user by email
        public dynamic GenerateUser(PC_Userdetail Model)
        {
            string Password = GenerateRandomPassword(13).ToString();
       
            string EncryptedPassword = Encryption.Encrypt(Password);
            Model.user.Password = EncryptedPassword;
            var data = dal.CreateUser(Model.user);
            if (data != null)
            {
                //send email
                Model.user.Password = Password;
                objEmail.GetEmailTemplate("Sign Up", Model.user);

                //add userregion
                Model.userregion.Userid = data.Userid;
                var xregion = adduserRegion(Model.userregion);
                if (xregion != null)
                {
                    NewspaperSBTSession.CurrentUser = data;
                    return data;
                }
            }
            else
            {
                return false;
            }

            return false;
        }


        //todo this method will create user on given details and send details to coresponding user by email
        public dynamic Createuser(PC_User Model)
        {
            var password = Model.Password;
            var encryptedpassword = Encryption.Encrypt(Model.Password);
            Model.Password = encryptedpassword;

            if (dal.EmailExists(Model.Email) == false)
            {
                var data = dal.CreateUser(Model);
                if (data != null)
                {
                    //send email
                    Model.Password = password;
                    objEmail.GetEmailTemplate("Sign Up", Model);
                    //
                    return shared.returnMessageJSON("User Created Successfully.", "", false);

                }
                return shared.returnMessageJSON("Invalid Data", "", true);
            }
            return shared.returnMessageJSON(Messages.EmailExistErrorTitle, Messages.EmailExistSuccessDescription, true);

        }


        //todo this method will create user on given details and send details to coresponding user by email
        public dynamic Updateuser(PC_User Model)
        {
            var password = Model.Password;
            if (password != null)
            {
                var encryptedpassword = Encryption.Encrypt(Model.Password);
                Model.Password = encryptedpassword;

            }
           
                var data = dal.UpdateUserData(Model);
                if (data != null)
                {
                    //send email
                    Model.Password = password;
                    objEmail.GetEmailTemplate("Sign Up", Model);
                    //
                    return shared.returnMessageJSON("User Updated Successfully.", "", false);

                }
                return shared.returnMessageJSON("Invalid Data", "", true);
                }
        //todo this method will reset password and send to email
        public dynamic passwordReset(string email) {
            PC_User obj = new PC_User();
            
            string password = GenerateRandomPassword(13);

        var data=    dal.updateDatabyemail(email,Encryption.Encrypt(password));
            if (data!=null) {
                obj.Password = password;
                obj.Email = email;
                obj.Fullname = data.Fullname;
                objEmail.GetEmailTemplate("ResetPassword", obj);
                return true;
            }
            return false;
        }
        public dynamic SetPassword(string email,string Password)
        {
            PC_User obj = new PC_User();

            string password = Password;
            string Email = email;
           
          
            var data = dal.SetPassword(email, Encryption.Encrypt(password));
            if (data!=null)
            {
                obj.Password = password;
                obj.Email = email;
                obj.Fullname = data.Fullname;

                objEmail.GetEmailTemplate("PasswordChanged", obj);
                return shared.returnMessageJSON(Messages.PasswordUpdateSuccessTitle, Messages.PasswordUpdateSuccessDescription, false);
                
            }
            return shared.returnMessageJSON(Messages.PasswordUpdateErrorTitle, Messages.PasswordUpdateErrorDescription, false);

        }

        //todo GeneratRandomPassword BeforSavingPassword
        public string GenerateRandomPassword(int length) {


       string  GeneratedPass=  objencrp.CreatePassword(13);

            return GeneratedPass;
        }


        //todo validation befor Saving User
        public bool isDataValid(PC_User model)
        {
            if (model.Email != null && model.Fullname != "" )
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}