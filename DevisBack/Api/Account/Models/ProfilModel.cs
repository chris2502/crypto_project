using ServiceStack.DataAnnotations;
using DevisBack.Api.Access.Models;
using System.ComponentModel.DataAnnotations;
using DevisBack.Tools.DevisBackAnnotation;
using ServiceStack.OrmLite;

namespace DevisBack.Api.Account.Models
{
	public class ProfilModel
	{
        [AutoIncrement]
		public int? Id { get; set; }

        [Index(Unique = true)]
		[StringLength(20)]
        public string Name { get; set; }

        [ForeignKey(typeof(AccessGroupModel), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int? AccessGroupModelId { get; set; }

        [Ignore]
        public AccessGroupModel Access { get; set; }

	}
}
