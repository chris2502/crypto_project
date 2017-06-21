using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevisBack.Api.Access.Models;

namespace DevisBack.Api.Access.AccessReponse
{
    public class TableAccessCompositionResponse
    {

        public int? Id { get; set; }
        public int? SelfId { get; set; }
        public int? SelfAssociateId { get; set; }

        /********************** Or **************************/

        /************* Used in service to populate abstractAccessAppli ***********/
        public AccessGroupModel AccessParent { get; set; }
        public AccessGroupModel AccessParentChild { get; set; }
        public AccessUnitModel AccessChild { get; set; }

        public int Code { get; set; }
        public string MessageError { get; set; }
    }
}