using System;
using System.Collections.Generic;
using DevisBack.Api.Access.Models;

namespace DevisBack.Api.Access.AccessReponse
{
	public class AccessGroupResponse
	{
        public int? Id { get; set; }
        public string NameGroup { get; set; }
        public List<AbstractAccessAppliModel> ListAccessAppli { get; set; }
        public int Code { get; set; }
    }
}
