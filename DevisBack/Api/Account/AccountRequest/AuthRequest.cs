using ServiceStack;
using DevisBack.Api.Account.AccountResponse;

namespace DevisBack.Api.Account.AccountRequest
{
	//TODO: Implement register authentication

	public class AuthRequest
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string Token { get; set; }
		public bool IsvalidEmail()
		{
			try
			{
				var addr = new System.Net.Mail.MailAddress(Email);
				return addr.Address == Email;
			}
			catch
			{
				return false;
			}
		}
	}
}