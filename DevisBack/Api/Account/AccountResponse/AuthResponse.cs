using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using System.ComponentModel;

namespace DevisBack.Api.Account.AccountResponse
{
	public class AuthResponse: AllResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
		[Description("Value is 0 when all is right. And -1 when there is some problem" +
		             " eg: When Api need to send a mail, if mail isn't sent, Code equals to -1" +
		             " NB: If client need a authentication, but there is no account which match" +
		             " Code is also equals to 0. This case don't mean a problem."
		            )
		]

		public override bool Equals(object obj)
		{
			var item = obj as AuthResponse;

			if (item == null)
			{
				return false;
			}
			return Email.Equals(item.Email) && Token.Equals(item.Token);
		}

		public override int GetHashCode()
		{
			return Email.GetHashCode() + Token.GetHashCode();
		}
    }


}