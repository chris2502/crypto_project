using System;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using MySql.Data.MySqlClient;
using DevisBack.Tools.DevisBackAnnotation;
using DevisBack.Api.Access.Models;


namespace DevisBack.Tools.StaticTools
{
	public static class StoreFieldToDatabase
	{
		//Check and return true, if the class itself is marked by annotation StoreFieldAttribute
		//else return false
		public static bool IsStoreFieldsClassAttribute(MemberInfo member)
		{
			if (Attribute.IsDefined (member, typeof(StoreFieldsAttribute)))
				return true;
			return false;
		}

		//check and return true, if member's class(MemberInfo) is marked by annotation StoreFieldsAttribute
		//else return false;
		public static bool IsStoreFieldsMemberAttribute(MemberInfo member)
		{
			foreach (object attribute in member.GetCustomAttributes(true))
			{
				if (attribute is StoreFieldsAttribute)
					return true;				
			}
			return false;
		}
		//Check and return true, if member's class(MemberInfo) is marked by annotation IgnoreFieldAttribute
		//else return false
		public static bool IsIgnoreFieldAttribute(MemberInfo member)
		{
			foreach (object attribute in member.GetCustomAttributes(true))
			{
				if (attribute is IgnoreFieldAttribute)
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
				if(attribute is StoreFieldsAttribute)
					Console.WriteLine("StoreFieldsAttribute {0}", attribute);
				else if(attribute is IgnoreFieldAttribute)
					Console.WriteLine("IgnoreFieldsAttribute {0}", attribute);
			}
		}

		//Return list's fields that is marked by an annotation (StoreFieldAttribute)
		// If it's only the class itself is marked by annotation StoreFieldAttribute, all properties's class is return in list
		//except fields or properties marked by annotation IgnoreFieldAttribute and property Id
		public static SortedSet<string> AddField(Type nameClass, ref SortedSet<string> listFieldToIgnore)
		{
			if(listFieldToIgnore == null)
				listFieldToIgnore = new SortedSet<string>();
			
			SortedSet<string> listField = new SortedSet<string>();
			if (IsStoreFieldsClassAttribute(nameClass)) 
			{
				foreach (PropertyInfo property in nameClass.GetProperties()) 
				{
					string subsIdTmp = property.Name.Substring(property.Name.Length -2);
					if (IsIgnoreFieldAttribute (property) || subsIdTmp.CompareTo("Id") == 0)
						listFieldToIgnore.Add (property.Name);
					else
						listField.Add(property.Name);
					
					Console.WriteLine ("Property custum {0}!", property.Name);
				}
			}
			foreach (PropertyInfo property in nameClass.GetProperties()) {
				string subsIdTmp = property.Name.Substring(property.Name.Length -2);
				if (IsStoreFieldsMemberAttribute(property) && subsIdTmp.CompareTo("Id") != 0) {
					listField.Add (property.Name);
					Console.WriteLine ("Property {0} is Add to list!", property.Name);
				} else {
					Console.WriteLine ("Property {0} is not Add to list!", property.Name);
				}
			}
			listField.ExceptWith(listFieldToIgnore);

			return listField;
		}

		//store properties in table FieldNameModel
		public static bool SaveFields(IDbConnection db, Type nameClass)
		{
			SortedSet<string> listFieldToIgnore = new SortedSet<string>();

			SortedSet<string> listField = AddField(nameClass, ref listFieldToIgnore);

			if (listField.Count == 0 || listField == null)
				return false;
			
			/*string strRequestCreate = "CREATE TABLE IF NOT EXISTS FieldNameModel ( "+
								"Id SMALLINT(3) UNSIGNED AUTO_INCREMENT PRIMARY KEY, "+
								"Field varchar(45) NOT NULL, "+
				"CONSTRAINT UQ_field UNIQUE (Field))";*/
			using (var trans = db.BeginTransaction ()) {
				try {
					db.CreateTableIfNotExists<FieldNameModel>();
					//db.ExecuteSql (strRequestCreate);

					foreach (string field in listField) {
						string strRequestInsert = "INSERT IGNORE INTO FieldNameModel (Name, NameClassAssociate) VALUES ('" + field + "', '"+nameClass.ToString()+"')";
						db.ExecuteSql (strRequestInsert);				
						//db.Insert<FieldNameModel>(new FieldNameModel{ Name = field, NameClassAssociate = nameClass.ToString()});
					}
					//remove all properties are marked by annotation IgnoreFieldAttribute 
					foreach (string fieldToIgnore in listFieldToIgnore) {
						//string strRequestDelete = "Delete From FieldNameModel WHERE Field LIKE '"+fieldToIgnore+"'";
						//db.ExecuteSql (strRequestDelete);
						db.Delete<FieldNameModel>(new FieldNameModel{ Name = fieldToIgnore});
					}
					trans.Commit ();
				} catch (MySqlException e) {
					trans.Rollback ();
					Console.WriteLine ("==> " + e.Message);
				}
			}
			return true;
		}
	}
}

