using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using DevisBack.Account.Api.AccountResponse;
using DevisBack.Api.Account.Models;

namespace DevisBack.Api.Account.AccountRequest
{
	public class UserRequest : HeadRequest
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NameProfil { get; set; }

        public int? AuthId { get; set; }
        public int? ProfilId { get; set; }

        public AuthModel Auth { get; set; }
        public ProfilModel Profil { get; set; }
    }
}