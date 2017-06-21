using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;

namespace DevisBack.Api.Access.AccessRequest
{
	public class PermissionRequest
	{
		
		public bool? Readable { get; set; }
		public bool? Writable { get; set; }
	}
}

