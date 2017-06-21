using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevisBack.Api;

namespace DevisBack.Account.Api.AccountResponse
{
	public class UserResponse: AllResponse
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Token { get; set; }

        public string nameProfil { get; set; }


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