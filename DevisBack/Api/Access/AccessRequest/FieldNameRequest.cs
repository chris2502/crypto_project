using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevisBack.Api.Access.AccessRequest
{
    public class FieldNameRequest
    {
        public int? Id { get; set; }
        //This name contents value of strArrayName.split
        public string[] Name { get; set; }
        public int? AccessUnitModelId { get; set; }
        [Required]
        public string Token{get; set;}
    }
}