using DevisBack.Api.Account.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevisBack.Account.Api.AccountResponse
{

    public class ProfilResponse
    {
        public List<ProfilModel> OneProfil { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}