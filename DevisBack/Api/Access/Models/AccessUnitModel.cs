using System;
using ServiceStack.DataAnnotations;
using System.Collections.Generic;
using DevisBack.Tools.DevisBackAnnotation;
using ServiceStack.OrmLite;

namespace DevisBack.Api.Access.Models
{
	//This class represent a set of field which can be belong to a category
    [CompositeIndex("FieldNameId", "PermissionId", Unique = true)]
	public class AccessUnitModel : AbstractAccessAppliModel
	{
        [ForeignKey(typeof(TableAccessCompositionModel), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int? Id{ get; set; }

		[ForeignKey(typeof(FieldNameModel), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
		public int? FieldNameId { get; set; }
        [Ignore]
        //[Reference]
		public FieldNameModel FieldName { get; set; }

	}
}
