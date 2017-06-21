using System;

namespace DevisBack.Api.Access.AccessRequest
{
	public class AccessUnitRequest : HeadRequest
	{
		public int? Id { get; set;}
		public int? FieldNameModelId { get; set;}
		public string NameField{ get; set;}
		public string NameClassAssociate{ get; set;}
		public bool Readable{ get; set;}
		public bool Writable{ get; set;}

	}
}

