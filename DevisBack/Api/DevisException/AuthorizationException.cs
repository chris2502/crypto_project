using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevisBack.Api.DevisException
{
    public class AuthorizationException: Exception
    {
        public int? Code { get; set; }
        public AuthorizationException(string message, int? code=null) : base(message)
        {
            Code = code;
        }
    }
}