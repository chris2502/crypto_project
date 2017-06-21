using System;
using System.Reflection;
using System.Collections;

namespace DevisBack.Tools.DevisBackAnnotation
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple=false)]
	public class StoreFieldsAttribute : Attribute
	{
		public override string ToString ()
		{
			return string.Format ("[StoreField]");
		}

	}
}

