using System;
using System.Collections.Generic;
using DevisBack.Api.Access.Models;

namespace DevisBack.Api.Access.AccessReponse
{
    public class AccessGroupResponse : AllResponse
    {
        public int? Id { get; set; }
        public List<AccessGroupModel> ListAccessGroup{get; set; }
    }
}
