using System;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using DevisBack.Tools.DevisBackAnnotation;

namespace DevisBack.Api.Access.Models
{
	//Here, we have a pattern composite.Component: AbstractAccessAppliModel; 
	//Composite: AccessGroupModel; Leaf: AccesssUnitModel
	// a composite represents a classic clean, or roofing clean by example.
	//a composite or a category can be set of sub-category 
	//or can be field or a set of fields
	//Each field, option, category need permission on reading or writing.
	//Those permissions are permissionModel class
	public abstract class AbstractAccessAppliModel
	{
		

        [ForeignKey(typeof(PermissionModel), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public virtual int? PermissionId { get; set; }
        [Ignore]
        //[Reference]
        public virtual PermissionModel Permission { get; set; }
    }
}