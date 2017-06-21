using System;

namespace DevisBack.Tools.DevisBackAnnotation
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
	public class IgnoreAuthorizationAttribute : Attribute
	{
		public override string ToString ()
		{
			return string.Format ("[IgnoreAuthorizationAttribute]");
		}
	}
}

