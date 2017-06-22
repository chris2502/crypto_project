using DevisBack.Api.Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevisBack.Api.Account.AccountRequest
{
    public class ProfilRequest
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int AccessGroupModelId { get; set; }

        public AccessGroupModel Access { get; set; }
    }
}