using DevisBack.Api.Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevisBack.Api.Access.AccessRequest
{
    public class AccessGroupRequest: HeadRequest
    {
        /** use in service to populate abstractAccessAppli, accessGroup, accessunit by id **/
        public int? CompositeId { get; set; }
        public int? PermissionModelId { get; set; }

        /********************** Or **************************/

        /************* Used in service to populate abstractAccessAppli ***********/
        public string Name { get; set; }
        public PermissionModel Permission { get; set; }

        /* Used in service to Populate AccessGroupModel by populating accessunit(List<abstractAccessAppliModel>) */
        public Dictionary<string, List<AccessUnitModel>> DicAccess { get; set; }
        public List<AbstractAccessAppliModel> ListAccess { get; set; }
    }
}