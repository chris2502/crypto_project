using System;
using DevisBack.Api.Access.Models;

namespace DevisBack.Api.Access.AccessReponse
{
	public class AbstractAccessResponse
	{
		public int Id;
		public int FieldNameModelId { get; set; }
		public int PermissionModelId{ get; set; }

		public PermissionModel PermissionModel { get; set;}
	}
}

