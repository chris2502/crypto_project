using ServiceStack.DataAnnotations;

namespace DevisBack.Api.Access.Models
{
	[CompositeIndex(true, "Name", "NameClassAssociate")]
    public class FieldNameModel
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string Name { get; set; }
		public string NameClassAssociate{ get; set;}
    }
}