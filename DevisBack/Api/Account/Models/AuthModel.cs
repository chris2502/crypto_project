using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;
using ServiceStack.Configuration;
using DevisBack.Tools.DevisBackAnnotation;
using System.Text;

namespace DevisBack.Api.Account.Models
{

    public class AuthModel
    {
        
        public static readonly string EmailChangePasswordBack = "forgotpassword@devis.fr";
        public static readonly string PasswordChangePasswordBack = "devisback2017";

        [AutoIncrement]
        public int? Id { get; set; }


        [Index(Unique = true)]
        public string Email { get; set; }
        public string Password { get; set; }
        [Index(Unique = true)]
        public string Token { get; set; }
        public string DoubleAuthenticate { get; set; }
        //Avoid to disable account
        [Default(1)]
        public bool IsEnable { get; set; }
        
        public int  UserModelId { get; set; }

        public readonly static int timerToken= (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

        public void GenerataDoubleAuthenticate()
        {
            DoubleAuthenticate = System.Guid.NewGuid().ToString().PadLeft(13);
        }
        public void CreateToken(string prefix, bool more_entropy)
        {
            if (string.IsNullOrEmpty(prefix))
                prefix = string.Empty;

            if (!more_entropy)
            {
                Token = (prefix + System.Guid.NewGuid().ToString()).PadLeft(13);
            }
            else
            {
                Token= (prefix + System.Guid.NewGuid().ToString() + System.Guid.NewGuid().ToString()).PadLeft(23);
            }
        }

        public bool isGoodToken(string token)
        {
            int timerTokenLocal = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            if (Token.CompareTo(token) == 0 && timerToken == timerTokenLocal) { return true; }
            return false;
        }


        public bool sendMail(string url)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("cryptongoh@gmail.com", "christianetfaisal");

                MailMessage mm = new MailMessage("sendtomyemail@domain.co.uk", "chrisebongue@hotmail.fr", "test", "<a href='" + url + "' >Se connecter</a>");
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                client.Send(mm);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}