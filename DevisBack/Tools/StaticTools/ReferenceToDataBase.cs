using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using DevisBack.Tools.DevisBackAnnotation;
using ServiceStack.OrmLite;
using DevisBack.Api.Access.Models;
using System.Linq.Expressions;

namespace DevisBack.Tools.StaticTools
{
    public static class ReferenceToDataBase
    {

        public static bool Update<T>(IDbConnection db, T obj, bool recursive = false)
        {

            Type nameClass = obj.GetType();
            foreach (FieldInfo field in nameClass.GetFields())
            {
                if (RefactorFunction.IsMemberAttribute<OneToManyAttribute>(field))
                {
                    dynamic oneToMany = field.GetValue(obj);
                    UpdateComplexeProperty<T>(db, oneToMany);
                }
            }
            foreach (PropertyInfo property in nameClass.GetProperties())
            {
                dynamic genericAttribute = property.GetValue(obj, null);
                if (genericAttribute != null)
                {
                    if (RefactorFunction.IsMemberAttribute<ReferenceAttribute>(property))
                    {
                        Update(db, genericAttribute, true);
                        UpdateReference<T>(db, genericAttribute);
                    }

                    if (RefactorFunction.IsMemberAttribute<OneToManyAttribute>(property))
                    {

                        UpdateComplexeProperty<T>(db, genericAttribute);
                    }
                }

            }
            return (!recursive) ? UpdateNormalProperty(db, obj) : true;
        }

        private static bool UpdateNormalProperty<T>(IDbConnection db, T obj)
        {
            try
            {
                MethodInfo generic = typeof(OrmLiteWriteConnectionExtensions).GetMethods()
                     .SingleOrDefault(x => x.Name == "Update")
                     .MakeGenericMethod(typeof(T));
                generic.Invoke(db, new object[] { db, new[] { obj } });

            }
            catch (Exception e)
            {
                var t = e.InnerException;
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        private static bool UpdateReference<T>(IDbConnection db, dynamic genericRef)
        {
            try
            {
                Type typeRef = genericRef.GetType();

                MethodInfo generic = typeof(OrmLiteWriteConnectionExtensions).GetMethods()
                     .SingleOrDefault(x => x.Name == "Update")
                     .MakeGenericMethod(typeof(object));


                generic.Invoke(db, new object[] { db, new[] { genericRef } });
               

            }
            catch (Exception e)
            {
                var t = e.InnerException;
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        private static bool UpdateComplexeProperty<T>(IDbConnection db, dynamic oneToMany)
        {
            List<int> listLastId = new List<int>();
            int? lastId = -1;
            foreach (var item in oneToMany)
            {
                try
                {
                    Update(db, item,  true);
                    Type type = item.GetType();
                    UpdateNormalProperty(db, item);
                }
                catch (TargetInvocationException e)
                {
                    var t = e.InnerException;
                    Console.WriteLine(e.InnerException);
                    return false;
                }
            }
            return true;
        }



        public static List<int> Save<T>(IDbConnection db, T obj, ref int? lastId, bool recursive=false)
		{

            Type nameClass = obj.GetType ();
			List<int> listLastId = new List<int> ();
            Dictionary<string, int?> dicPropertyId = new Dictionary<string, int?>();
			foreach (FieldInfo field in nameClass.GetFields()) 
			{
				if (RefactorFunction.IsMemberAttribute<OneToManyAttribute> (field)) 
				{
					dynamic oneToMany = field.GetValue (obj);	
					Dictionary<string, int?> tmp = SaveComplexeProperty<T>(db, oneToMany);
                    /*foreach(KeyValuePair<string, int?> value in tmp)
                    {
                        dicPropertyId.Add(value.Key, value.Value);
                    }*/
				}
			}
			foreach (PropertyInfo property in nameClass.GetProperties()) 
			{ 
				dynamic genericAttribute = property.GetValue (obj, null);
                if(genericAttribute != null)
                {
                    if (RefactorFunction.IsMemberAttribute<ReferenceAttribute>(property))
                    {
                        Save(db, genericAttribute, ref lastId, true);
                        SaveReference<T>(db, genericAttribute, ref lastId);
                        dicPropertyId.Add(genericAttribute.GetType().Name + "Id", lastId);
                    }

                    if (RefactorFunction.IsMemberAttribute<OneToManyAttribute>(property))
                    {

                        Dictionary<string, int?> tmp = SaveComplexeProperty<T>(db, genericAttribute);
                        /*foreach (KeyValuePair<string, int?> value in tmp)
                        {
                            dicPropertyId.Add(value.Key, value.Value);
                        }*/
                    }
                }

			}
            lastId = (!recursive) ? SaveNormalProperty(db, obj, dicPropertyId):lastId ;
			return listLastId;
		}

        private static int? SaveNormalProperty<T>(IDbConnection db, T obj, Dictionary<string, int?> dicPropertyId=null)
        {
            try
            {
                if(dicPropertyId != null)
                {
                    Type nameClass = obj.GetType();
                    foreach (KeyValuePair<string, int?> entry in dicPropertyId)
                    {
                        nameClass.GetProperty(entry.Key).SetValue(obj, entry.Value);
                    }
                }

                MethodInfo generic = typeof(OrmLiteWriteConnectionExtensions).GetMethods()
                     .SingleOrDefault(x => x.Name == "Save" &&
                                        x.GetParameters().Length == 2 &&
                                        x.GetParameters()[1].Name.CompareTo("obj") == 0)
                     .MakeGenericMethod(typeof(T));
                generic.Invoke(db, new object[] { db, obj});

            }
            catch (Exception e)
            {
                var t = e.InnerException;
                Console.WriteLine(e);
                return null;
            }
            return (int?)db.GetLastInsertId();
        }

        private static Dictionary<string, int?> SaveComplexeProperty<T>(IDbConnection db, dynamic oneToMany)
        {
            Dictionary<string, int?> dicLastId = new Dictionary<string, int?>();
            int? lastId = -1;
            foreach (var item in oneToMany)
            {
                try
                {
                    Save(db, item, ref lastId, true);
                    Type type = item.GetType();
                    SaveNormalProperty(db, item);
                }
                catch (TargetInvocationException e)
                {
                    var t = e.InnerException;
                    Console.WriteLine(e.InnerException);
                    return null;
                }
            }
            return dicLastId;
        }

        private static int SaveReference<T>(IDbConnection db, dynamic genericRef, ref int? lastId)
        {
            try
            {
                Type typeRef = genericRef.GetType();

                MethodInfo generic = typeof(OrmLiteWriteConnectionExtensions).GetMethods()
                     .SingleOrDefault(x => x.Name == "Save" && 
                                        x.GetParameters().Length == 2 && 
                                        x.GetParameters()[1].Name.CompareTo("obj")== 0)
                     .MakeGenericMethod(typeRef);


                generic.Invoke(db, new object[] { db, genericRef});
                lastId = (int?)db.GetLastInsertId();

            }
            catch (Exception e)
            {
                var t = e.InnerException;
                Console.WriteLine(e);
                return -1;
            }
            return 0;
        }

        
        public static List<T> select<T>(IDbConnection db,  Expression<Func<T, bool>> genericExpress)
		{
			
			List<T> listResult = null;
			List<T> listReferenceResult= new List<T>();
			List<T> listOneToManyResult = null;

            listResult = SelectSimple<T>(db, genericExpress);

           
            Type nameClass = typeof(T);

            if (RefactorFunction.IsClassAttribute(typeof(T), typeof(ReferenceAttribute)))
            {
                foreach (PropertyInfo property in nameClass.GetProperties())
                {
                    if (RefactorFunction.IsMemberAttribute<ForeignKeyAttribute>(property))
                    {
                        listReferenceResult.AddRange( SelectReference<T>(db, property, listResult));
                    }
                    if (RefactorFunction.IsMemberAttribute<ReferenceAttribute>(property))
                    {
                    }
                    if (RefactorFunction.IsMemberAttribute<OneToManyAttribute>(property))
                    {

                        //listOneToManyResult = SelectComplexe<T>(db, genericAttribute, genericExpress); ;						
                    }
                }
            }
                       
            
			return listResult;
		}

        private static List<T> SelectSimple<T>(IDbConnection db, Expression<Func<T, bool>> genericExpress)
        {
            List<T> listObjResult = null;
            try
            {
                //db.Select<T>(genericExpress);
                Type type = typeof(T);
                var generics = typeof(OrmLiteReadConnectionExtensions).GetMethods();

                MethodInfo generic = typeof(OrmLiteReadConnectionExtensions).GetMethods()
                    .Single(x => x.Name == "Select" && x.GetParameters().Length == 2)
                    .MakeGenericMethod(type);

                listObjResult = (List<T>)generic.Invoke(db, new object[] { db, genericExpress });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            return listObjResult;
        }
    

       































		

		public static List<T> SelectReference<T>(IDbConnection db, PropertyInfo info, List<T> listObj)
		{
			List<T> listReferenceResult = null;
            try
            {
                string nameType = info.Name.Substring(0, info.Name.Length - 2);
				Type type = Type.GetType(nameType);
                
                MethodInfo generic = typeof(OrmLiteWriteConnectionExtensions).GetMethods()
					.Single(x => x.Name == "Query" && x.GetParameters().Length ==1) 
                    .MakeGenericMethod(type);
                foreach(T obj in listObj)
                {
                    foreach (PropertyInfo objInfo in obj.GetType().GetProperties())
                    {
                        if (info.Name.CompareTo(objInfo.Name) == 0)
                        {
                            dynamic valueId = objInfo.GetValue(obj, null);
                            
                            listReferenceResult = (List<T>)generic.Invoke(db, new object[] { db, "Id="+valueId });
                        }
                    }
                }



            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

			return listReferenceResult;
        }
    }
}