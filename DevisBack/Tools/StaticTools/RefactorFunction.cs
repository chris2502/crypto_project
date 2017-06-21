using System;
using System.Reflection;

namespace DevisBack
{
	public static class RefactorFunction
	{
		//Check and return true, if the class itself is marked by annotation typeAttribute
		//else return false
		public static bool IsClassAttribute(MemberInfo member, Type typeAttribute)
		{
			if (Attribute.IsDefined (member, typeAttribute))
				return true;
			return false;
		}

		//check and return true, if member's class(MemberInfo) is marked by annotation Attribute T
		//else return false;
		public static bool IsMemberAttribute<T>(MemberInfo member)
		{
			foreach (object attribute in member.GetCustomAttributes(true))
			{
				if (attribute is T)
					return true;				
			}
			return false;
		}

		//Display all member's class that is marked by annotation Attribute T
		public static void DumpAttributes<T>(MemberInfo member, T typeAttribute)
		{
			Console.WriteLine("Attributes for : " + member.Name);
			foreach (object attribute in member.GetCustomAttributes(true))
			{
				if(attribute is T)
					Console.WriteLine("{0} : {1}", typeAttribute, attribute);
			}
		}

		//Display all member's class that is marked by annotation Attribute T
		public static void DumpAttributes<T, P>(MemberInfo member, T typeAttribute1, P typeAttribute2)
		{
			Console.WriteLine("Attributes for : " + member.Name);
			foreach (object attribute in member.GetCustomAttributes(true))
			{
				if(attribute is T)
					Console.WriteLine("{0} : {1}", typeAttribute1, attribute);
				else if(attribute is T)
					Console.WriteLine("{0} : {1}", typeAttribute2, attribute);
			}
		}
	}
}

