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
    public class BAL_Vendor
    {
        shared objshared = new shared();
        DAL.DAL dal = new DAL.DAL();
        BAL_User objbalUser = new BAL_User();

        //todo this method will add signup details  and validate
        public dynamic AddVendor(PC_VendorDetails model)
        {
          
            try
            {
                if (isDataValid(model))
                {
                    if (dal.EmailExists(model.Vendor.Email)==false) {
                       
                        var user = objbalUser.GenerateUser(model.UserDetails);
                        if (user != null) {
                            model.Vendor.UserId = user.Userid;

                            //Check is upccode special or not 
                            var isRecommanded = dal.checkIsRecommandedUpc(model.Vendor.upccode);
                            if (isRecommanded) { model.Vendor.DocumentSigned = "Document Signed"; }

                            var  vebdor= dal.AddVendor(model.Vendor); 
                            if (vebdor != null) {
                                return shared.returnMessageJSON(Messages.SignUpSuccessTitle, Messages.SignUpSuccessDescription, false);
                            }
                            return shared.returnMessageJSON(Messages.SignUpInValidDataTitle, Messages.SignUpInValidDataDescription, true);

                        }
                        return shared.returnMessageJSON(Messages.SignUpInValidDataTitle, Messages.SignUpInValidDataDescription, true);

                    }
                    return shared.returnMessageJSON(Messages.EmailExistErrorTitle, Messages.EmailExistSuccessDescription, true);



                }
                return shared.returnMessageJSON(Messages.SignUpInValidDataTitle, Messages.SignUpInValidDataDescription, true);
                
              

            }
            catch (Exception E)
            {

                return shared.returnMessageJSON(E.Data.ToString(), E.Message.ToString(), true);

            }




        }
        //todo this method will update vendor on given details
        public dynamic updateVendor(PC_Vendor model) {
            try
            {
                if (validdata(model))
                {
                    var data = dal.UpdateVendor(model);
                    if (data != null)
                    {
                        return shared.returnMessageJSON(Messages.updateSuccessTitle, Messages.updateSuccessDescription, false);
                    }
                    else
                    {
                        return shared.returnMessageJSON(Messages.UserUpdateErrorTitle, Messages.UserUpdateErrorDescription, true);

                    }
                }
                return shared.returnMessageJSON(Messages.updateInValidDataTitle, Messages.updateInValidDataDescription, true);

            }
            catch (Exception e) {
                return shared.returnMessageJSON(Messages.updateInValidDataTitle, Messages.updateInValidDataDescription, true);

            }

        }
//todo this method will valid data for updatevendor
    public Boolean validdata(PC_Vendor model) {

            if (model.Contactname!=null && model.VendorName !=null && model.Vendorid !=0)
            { return true; }
            else { return false; }
        }

        //todo this method check is email already exist
        public Boolean checkisEmailExist(string email) {
            return dal.EmailExists(email);
        }
        public dynamic Updatedocumentstatus(PC_Vendor model)
        {
            return dal.updatedocumentstatus(model);
        }


        //todo this method will check upccode   and also set current upc to session ;if invalid block user for an hour 
        public dynamic CheckUpcVerification(string Code, int Atempt,string sessionid)
        {
            try
            {
                dynamic Data = dal.CheckUpcVerification(Code, Atempt, sessionid);
                //if greater then zer
                if ( int.Parse(Data[0]) == 1 )
                {
                    if (Atempt > 3) { return shared.returnMessageJSON(Messages.UPCErrorTitle, Messages.UPCErrorDescription, false); }
                    NewspaperSBTSession.Upcode = Data.ToString();
                    return shared.returnMessageJSON(Messages.UPCErrorTitle, Messages.UPCErrorDescription, false);

                }

                return shared.returnMessageJSON(Messages.UPCErrorTitle, Messages.UPCErrorDescription, true);

            }
            catch (Exception e ) {
                return shared.returnMessageJSON(Messages.UPCErrorTitle, Messages.UPCErrorDescription, true);
            }

        }
        //todo Validation for Vendor Registration

        //todo validation for Vendor MODEL
        public Boolean isDataValid(PC_VendorDetails Model) {

            if (Model.Vendor.Contactname != null && Model.Vendor.Email != "" & Model.Vendor.upccode != null ) {


                return true;
            }
            else {

                return false;
            }

        }

        //todo this method will check isuserAllowed for sign or he is blocked 
        public Boolean checkisuserblocked(string Sessionid) {

            return dal.checkisuserBlocked(Sessionid);
        }


    }
}