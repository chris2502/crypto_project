using System;
using System.Reflection;
using System.Collections;

namespace DevisBack
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple=true)]
	public class OneToManyAttribute: Attribute
	{
		
	}
}

