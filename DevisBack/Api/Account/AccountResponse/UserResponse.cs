using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevisBack.Api;
using DevisBack.Api.Account.Models;

namespace DevisBack.Account.Api.AccountResponse
{
	public class UserResponse
    {
        public int ErrorCode { get; set; }
        public List<UserModel> ListUser { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public void makeToken(string password)
        {
            Random rand = new Random();

            int numRand = rand.Next(1, 26);
            char c = 'A';
            string tmp = "";
            for (int i = 0; i < 100; i++)
            {
                tmp = tmp + (c + (rand.Next(1, 26)).ToString());
            }
            Token = numRand + password;
        }
    }
}