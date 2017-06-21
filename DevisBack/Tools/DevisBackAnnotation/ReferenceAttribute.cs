using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevisBack.Tools.DevisBackAnnotation
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple=true)]
    public class ReferenceAttribute : Attribute
    {
        public override string ToString()
        {
            return "[Reference]";
        }
    }
}