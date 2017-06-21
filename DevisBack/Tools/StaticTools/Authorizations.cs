using System;
using System.Data;
using System.Reflection;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Runtime.Remoting;
using DevisBack.Tools.DevisBackAnnotation;
using DevisBack.Api.Account.Models;
using MySql.Data.MySqlClient;
using System.Linq;

namespace DevisBack.Tools.StaticTools
{
	public static class Authorizations
	{

        public static bool IsAdmin(IDbConnection Db, string token)
        {
            

            ProfilModel profil = Db.Query<ProfilModel>("Name=@Name", 
                                                        new { Name = "Admin" })
                                                        .FirstOrDefault();
            

            AuthModel auth = Db.Query<AuthModel>("Token=@Token",
                                                        new {Token = token })
                                                        .FirstOrDefault();
            if (profil == null || auth == null)
                return false;
            UserModel user = Db.Query<UserModel>("ProfilModelId=@ProfilModelId AND AuthmodelId=@AuthModelId",
                                            new { ProfilModelId = profil.Id, AuthModelId = auth.Id })
                                            .FirstOrDefault();
            if (user != null)
                return true; 
            return false;
        }

        public static bool IsConnect(IDbConnection Db, string token)
        {
            AuthModel auth = Db.SingleWhere<AuthModel>("Token", token);
            if (auth != null)
                return true;
            return false;
        }
		//Check and return true, if the class itself is marked by annotation AuthorizationsAttribute
		//else return false
		public static bool IsAuthorizationClassAttribute(MemberInfo member)
		{
			if (Attribute.IsDefined (member, typeof(AuthorizationsAttribute)))
				return true;
			return false;
		}

		//check and return true, if member's class(MemberInfo) is marked by annotation AuthorizationsAttribute
		//else return false;
		public static bool IsAuthorrizationsMemberAttribute(MemberInfo member)
		{
			foreach (object attribute in member.GetCustomAttributes(true))
			{
				if (attribute is AuthorizationsAttribute)
					return true;				
			}
			return false;
		}
		//Check and return true, if member's class(MemberInfo) is marked by annotation IgnoreAuthorizationdAttribute
		//else return false
		public static bool IsIgnoreAuthorizationdAttribute(MemberInfo member)
		{
			foreach (object attribute in member.GetCustomAttributes(true))
			{
				if (attribute is IgnoreAuthorizationAttribute)
					return true;				
			}
			return false;
		}



		//Display all member's class that is marked by annotation StoreFieldsAttribute and IgnoreFieldAttribute
		public static void DumpAttributes(MemberInfo member)
		{
			Console.WriteLine("Attributes for : " + member.Name);
			foreach (object attribute in member.GetCustomAttributes(true))
			{
				if(attribute is AuthorizationsAttribute)
					Console.WriteLine("AuthorizationsAttribute {0}", attribute);
				else if(attribute is IgnoreAuthorizationAttribute)
					Console.WriteLine("IgnoreAuthorizationAttribute {0}", attribute);
			}
		}


		public static bool IsReadable(PropertyInfo property)
		{
			//check in database if prperty is readable
			return true;
		}

		public static bool IsWritable(PropertyInfo property)
		{
			//check in database if prperty is readable
			return true;
		}

		public static bool checkAuthorization<T>(IDbConnection db, ref T obj)
		{
			Console.WriteLine ("gettype {0}!", obj.GetType());

			Type nameClass = obj.GetType ();
			foreach (PropertyInfo property in nameClass.GetProperties()) 
			{
				string subsIdTmp = property.Name.Substring(property.Name.Length -2);
				if (!IsIgnoreAuthorizationdAttribute (property) && subsIdTmp.CompareTo ("Id") != 0)
				{
					var val = property.GetValue (obj, null); 
					if ( val != null && !IsWritable(property)) 
					{
						throw new UnauthorizedAccessException ("Vous n'avez pas les droits en écriture sur " + property.Name);
					}
					Console.WriteLine ("Property custum {0}!", property.Name);
					Console.WriteLine ("Property value {0}!", property.GetValue(obj, null));
				}

			}
			return true;
		}
	}
}

