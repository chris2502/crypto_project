using ServiceStack.DataAnnotations;
using ServiceStack;
using DevisBack.Tools.DevisBackAnnotation;
using ServiceStack.OrmLite;

namespace DevisBack.Api.Account.Models
{
    [CompositeIndex("FirstName", "LastName", Unique = true)]
	public class UserModel
	{
        [AutoIncrement]
        public int? Id { get; set; }
		public string FirstName { get; set; }
		public string LastName{get; set;}

        [ForeignKey(typeof(AuthModel), OnDelete = "CASCADE", OnUpdate = "CASCADE")]       
        public int? AuthModelId { get; set; }
        [ForeignKey(typeof(ProfilModel), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public int? ProfilModelId { get; set; }


        [Ignore]
        public AuthModel Auth { get; set; }

        [Ignore]
        public ProfilModel Profil { get; set; }

        
    }

}
