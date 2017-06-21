using System.Collections.Generic;
using DevisBack.Api.Access.Models;
using DevisBack.Tools.DevisBackAnnotation;

namespace DevisBack.Api.Access.AccessRequest
{
	
	
    public class TableAccessCompositionRequest
    {
        /** use in service to populate abstractAccessAppli, accessGroup, accessunit by id **/
        public int? CompositeId { get; set; }
        public int[] LeafId { get; set; }
		public int? PermissionModelId { get; set; }

		/********************** Or **************************/

		/************* Used in service to populate abstractAccessAppli ***********/
		public string Name{ get; set;}
		public PermissionModel Permission { get; set; }

        public TableAccessCompositionRequest ChildGroup { get; set; }

		/* Used in service to Populate AccessGroupModel by populating accessunit(List<abstractAccessAppliModel>) */
		public List<AccessUnitModel> ListAccessUnit{ get; set; }

        public List<AccessGroupModel> ListAccessGroup { get; set; }

        public AccessGroupModel makeAccessGroup()
        {
            TableAccessCompositionModel tableAccess = new TableAccessCompositionModel();
            //while(ch)
            return null;
        }
    }
}