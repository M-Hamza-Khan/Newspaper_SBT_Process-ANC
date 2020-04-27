using NewsPaperSBT.Models.Core;
using NewsPaperSBT.Models.DAL;
using NewsPaperSBT.Models.PartialClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace NewsPaperSBT.Models.BAL
{
    public class BAL_Email
    {


            DAL.DAL dal = new DAL.DAL();
        //todo this method get email tempalte by template name
            public void GetEmailTemplate(string TemplateName, PC_User model)
        {
            try
            {
                var result = dal.getEmailTemplate(TemplateName);
                var smtpSettings = dal.EmailSettings();
                SendEmail(model, result, smtpSettings);
            }
            catch (Exception e) {
                throw new Exception("Username or password is invalid");


            }

        }
            //Updated Code


            //todo this method send email with given details 
public string SendEmail(PC_User model, EmailTemplates result, EmailSettings smtpSettings)
{
string msg = "";
try
{

                    
var Fullname = model.Fullname == null ? "" : model.Fullname;
var Password = model.Password == null ? "" : model.Password;
var FirstName = model.Fullname == null ? "" : model.Fullname;
                var EMAIL = model.Email == null ? "" : model.Email;


                var OTP = model.OTP == null ? "" : model.OTP;


                string eBody = result.Body;
eBody = eBody.Replace("{FullName]", Fullname == null ? "" : Fullname);
eBody = eBody.Replace("{Password]", Password == null ? "" : Password);
eBody = eBody.Replace("{Email]", EMAIL == null ? "" : EMAIL);
                eBody = eBody.Replace("{FirstName]", FirstName == null ? "" : FirstName);

                eBody = eBody.Replace("{OTP]", OTP == null ? "" : OTP);




                MailMessage Msg = new MailMessage();
Msg.From = new MailAddress(smtpSettings.EmailAddress);
Msg.To.Add(model.Email);
Msg.Subject = result.Subject;
Msg.IsBodyHtml = true;
Msg.Body = eBody;

//string Url = "http://localhost:39003/SignUp/Verification?Code=" + model.VerificationCode;
//string eBody = result.Body;

//MailMessage Msg = new MailMessage();
//Msg.From = new MailAddress(result.FromEmail);
//Msg.To.Add(model.Email);
//Msg.Subject = result.Subject;
//Msg.IsBodyHtml = true;
//Msg.Body = eBody;

SmtpClient smtpC = new SmtpClient();
smtpC.Host = smtpSettings.SmtpHost;
smtpC.Port = Convert.ToInt32(smtpSettings.SmtpPort);
smtpC.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
smtpC.UseDefaultCredentials = false;
smtpC.Credentials = new System.Net.NetworkCredential(smtpSettings.EmailAddress, Encryption.Decrypt( smtpSettings.Password));
smtpC.EnableSsl = Convert.ToBoolean(smtpSettings.SmtpEnableSsl);
smtpC.Send(Msg);
}
catch (Exception ex)
{

//Err.WriteError(ex.InnerException.ToString());
msg = ex.ToString();
}
return "";
//return PartialView("Index", db.vt_SMTPConfiguration.Where(x => x.IsActive != false).FirstOrDefault());
}



}


}