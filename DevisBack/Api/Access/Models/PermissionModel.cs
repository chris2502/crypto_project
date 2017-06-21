using ServiceStack.DataAnnotations;
using System.Collections.Generic;

namespace DevisBack.Api.Access.Models
{
    [CompositeIndex("Readable", "Writable", Unique = true)]
    public class PermissionModel
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public bool? Readable { get; set; }
		public bool? Writable { get; set; }

		public override string ToString()
		{
			return "Permissions: " +
				"; Readable: " + Readable.ToString() +
				  "; Writable: " + Writable.ToString();
		}
	}
}
