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
using System.ComponentModel;

namespace DevisBack.Api.Account.Models
{

    public class AuthModel
    {
        
        public static readonly string EmailChangePasswordBack = "forgotpassword@devis.fr";
        public static readonly string PasswordChangePasswordBack = "devisback2017";

        [AutoIncrement]
        public int? Id { get; set; }

        [ReadOnly(true)]
        [Index(Unique = true)]
        public string Email { get; set; }
        [ReadOnly(true)]
        public string Password { get; set; }
        [Index(Unique = true)]
        public string Token { get; set; }
        //Avoid to disable account
        [Default(1)]
        public bool IsEnable { get; set; }
        
        public int  UserModelId { get; set; }

        public readonly static int timerToken= (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
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


        public bool sendMail()
        {

			/*var message = new MimeMessage ();
			message.From.Add (new MailboxAddress ("DevisBack", EmailChangePasswordBack));
			message.To.Add (new MailboxAddress ("", Email));
			message.Subject = "Mot de passe perdu";

			message.Body = new TextPart ("plain") {
				Text = @"Il semble que vous avez perdu votre mot de passe. Si c'est le cas veuillez cliquer sur le lien suivant:" +
					"<a href='localhost:50304/"+Token+"/"+Email+">Chagner de mot de passe</a>'" +
					"<br/>Si ce n'est pas vous, veuillez ignorer le message"
			};*/

            return true;
        }
    }
}