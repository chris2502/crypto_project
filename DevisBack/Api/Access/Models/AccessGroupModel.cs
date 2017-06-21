using ServiceStack.DataAnnotations;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using DevisBack.Tools.DevisBackAnnotation;

namespace DevisBack.Api.Access.Models
{
    [CompositeIndex("Name", "PermissionId", Unique = true)]
    public class AccessGroupModel: AbstractAccessAppliModel
	{


		[ForeignKey(typeof(TableAccessCompositionModel), OnDelete ="CASCADE", OnUpdate = "CASCADE")]
		public int? Id{ get; set; }

		public string Name{ get; set; } 

		[OneToMany]
		public List<AbstractAccessAppliModel> ListAccessAppli = new List<AbstractAccessAppliModel> ();


		public void addAccessAppli(AbstractAccessAppliModel accessAppliModel)
		{
			ListAccessAppli.Add(accessAppliModel);
		}

		public void addRangeAccessAppli(List<AccessUnitModel> listAccessUnit)
		{
			ListAccessAppli.AddRange(listAccessUnit);
		}

		public int removeAccessAppli(AbstractAccessAppliModel accessApplicModel)
		{
			if (accessApplicModel != null)
			{
				ListAccessAppli.Remove(accessApplicModel);
				return 0;
			}
			return -1;
		}
	}
}
