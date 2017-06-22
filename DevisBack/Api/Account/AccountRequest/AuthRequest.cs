using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using ServiceStack;
using DevisBack.Api.Account.AccountResponse;
using NUnit.Framework;

namespace DevisBack.Api.Account.AccountRequest
{
	//TODO: Implement register authentication

	public class AuthRequest
	{
		public string Email { get; set; }
		
	    private byte[] password;
		
		public string Token { get; set; }

        public string DoubleAuthenticate { get; set; }
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

	    private string tempPassword;

	    public string Password
	    {
	        get
	        {
	            if (password != null && String.IsNullOrEmpty(tempPassword))
                {
                    byte[] result = CBC.dechiffrer(new byte[] { 55, 66, 77, 88 }, password);
                    string strResult = "";
                    foreach (byte myByte in result)
                    {
                        if (myByte == 0)
                        {
                            break;
                        }
                        strResult += Convert.ToChar(myByte);
                    }
                    tempPassword = strResult;
                }
	            return tempPassword;
	        }
	        set
	        {
	            if (password == null)
                {
                    List<byte> result = new List<byte>();
                    foreach (string strByte in value.Split(','))
                    {
                        result.Add(Convert.ToByte(strByte));
                    }
                    password = result.ToArray();
                }
	        }
	    }
	}
}