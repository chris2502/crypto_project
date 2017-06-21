using System;

namespace DevisBack.Tools.DevisBackAnnotation
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
	public class IgnoreFieldAttribute: Attribute
	{
		public override string ToString ()
		{
			return string.Format ("[IgnoreField]");
		}
	}
}

