using System;
using System.Reflection;
using System.Collections;

namespace DevisBack.Tools.DevisBackAnnotation
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple=true)]
	public class AuthorizationsAttribute : Attribute
	{
		public string Role{ get; set;}
	}
}

