using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DevisBack.Tools.DevisBackAnnotation;
using ServiceStack.OrmLite;

namespace DevisBack.Api.Access.Models
{
	//this table is a mrd from mcd of type reflexive table
    [CompositeIndex("Id", "SelfId", Unique = true)]
    public class TableAccessCompositionModel
    {
		[AutoIncrement]
		public int? Id{ get; set; }
        [ForeignKey(typeof(TableAccessCompositionModel), ForeignKeyName ="ForeignKeySelfId", OnDelete ="SET NULL", OnUpdate ="CASCADE")]
        public int? SelfId { get; set; }
        [ForeignKey(typeof(TableAccessCompositionModel), ForeignKeyName = "ForeignKeySelfAssociateId", OnDelete = "SET NULL", OnUpdate = "CASCADE")]
        public int? SelfAssociateId { get; set; }

    }
}