using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPaperSBT.Models.BAL
{

    public static class Messages
    {
        public static Boolean Return_True = true;
        public static Boolean Return_False = false;

        public static string SignUpSuccessTitle ="" ;
        public static string SignUpSuccessDescription = "Vendor Registered Successfully";
        public static string UPCErrorTitle = "Invalid UPC";
        public static string UPCErrorDescription = "Please enter valid UPC";
        public static string SignUpErrorTitle = "Email already Exists";
        public static string SignUpErrorDescription = "Email already Exists";
        public static string OTPExpireDescription = "Your OTP have has expired";
        public static string OTPResendSucceesDescription = "OTP has been sent on your email.";

        public static string SignUpInValidDataTitle = "";
        public static string SignUpInValidDataDescription = "Invalid Data has been provided";

  

        public static string VerificationSuccessTitle = "Verification Code is valid";
        public static string VerificationSuccessDescription = "Verification Code is valid";
        public static string VerificationInValidErrorTitle = "Their has been some error";
        public static string VerificationErrorDescription = "Their has been some error";

        public static string EmailExistSuccessTitle = "Email Send Your Mail";
        public static string EmailExistSuccessDescription = "Email has been provided";
        public static string EmailExistErrorTitle = "Email Already Exist !";
        public static string EmailExistErrorDescription = "";

        public static string EmailVerificationSuccessTitle = "Email has been provided";
        public static string EmailVerificationSuccessDescription = "Email has been provided";
        public static string EmailVerificationErrorTitle = "Email Does not Exist !";
        public static string EmailVerificationErrorDescription = "Their has been some error";

        public static string CodeVerificationSuccessTitle = "Verification Code has been provided";
        public static string CodeVerificationSuccessDescription = "Verification Code has been provided";
        public static string CodeVerificationErrorTitle = "Verification code is not correct";
        public static string CodeVerificationErrorDescription = "Their has been some error";

        public static string PasswordUpdateSuccessTitle = "";
        public static string PasswordUpdateSuccessDescription = "Password Update successfully";
        public static string PasswordUpdateErrorTitle = "";
        public static string PasswordUpdateErrorDescription = "Their has been some error";

        public static string PasswordResetVerificationDataTitle = "Invalid Data has been provided";
        public static string PasswordResetVerificationDescription = "Invalid Data has been provided";


        public static string UserUpdateSuccessTitle = "User Detail up has been updated successful";
        public static string UserUpdateSuccessDescription = "User Detail up has been updated successful";
        public static string UserUpdateErrorTitle = "Their has been some error";
        public static string UserUpdateErrorDescription = "Their has been some error";

        public static string UserUpdateDataTitle = "Invalid Data has been provided";
        public static string UserUpdateDescription = "Invalid Data has been provided";

        public static string PaymentDoneSuccessTitle = "Payment Done has been successful";
        public static string PaymentDoneSuccessDescription = "Payment Done has been successful";
        public static string PaymentDoneErrorTitle = "Their has been some error";
        public static string PaymentDoneErrorDescription = "Their has been some error";



        public static string AddSuccessTitle = "Sign up has been successful";
        public static string AddSuccessDescription = "Sign up has been successful";
        public static string AddErrorTitle = "Their has been some error";
        public static string AddErrorDescription = "Their has been some error";
        public static string AddInValidDataTitle = "Invalid Data has been provided";
        public static string AddInValidDataDescription = "Invalid Data has been provided";



        public static string updateSuccessTitle = "Succeed";
        public static string updateSuccessDescription = "Data has been Update successful";
        public static string updateErrorTitle = "Their has been some error";
        public static string updateErrorDescription = "Their has been some error";
        public static string updateInValidDataTitle = "Invalid Data has been provided";
        public static string updateInValidDataDescription = "unsuccessfull Update";

        public static string DeleteSuccessTitle = "Delete has been successful";
        public static string DeleteSuccessDescription = "Delete has been successful";
        public static string DeleteErrorTitle = "Their has been some error";
        public static string DeleteErrorDescription = "Their has been some error";
        public static string DeleteInValidDataTitle = "Invalid Data has been provided";
        public static string DeleteInValidDataDescription = "Invalid Data has been provided";
    }
}