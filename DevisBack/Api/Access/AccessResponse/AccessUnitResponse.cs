using System;
using System.Collections.Generic;
using DevisBack.Api.Access.Models;

namespace DevisBack.Api.Access.AccessReponse
{
	public class AccessUnitResponse
	{
		public AccessUnitResponse()
		{
			ListAccessUnit = new List<AccessUnitModel> ();
		}
		public List<AccessUnitModel> ListAccessUnit{ get; set; }
		public int Code{ get; set; }
	}
}

