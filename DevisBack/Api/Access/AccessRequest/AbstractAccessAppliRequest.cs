using System;

namespace DevisBack.Api.Access.AccessRequest
{
	public class AbstractAccessAppliRequest
	{
		public int Id{ get; set;}
		public int FieldNameModelId { get; set;}
		public int PermissionModelId {get; set;}
	}
}

