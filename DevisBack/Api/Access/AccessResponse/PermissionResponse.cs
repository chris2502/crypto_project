using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevisBack.Api.Access.Models;

namespace DevisBack.Api.Access.AccessReponse
{
    public class PermissionResponse
    {
		public PermissionResponse()
		{
			Permission = new List<PermissionModel>();
		}
		public List<PermissionModel> Permission{ get; set; }
        public int Code { get; set; }
    }
}