
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using ServiceStack;
using ServiceStack.OrmLite.MySql;
using ServiceStack.OrmLite;
using MySql.Data.MySqlClient;
using ServiceStack.Configuration;
using ServiceStack.Validation;
using DevisBack.Api.Account.Services;
using DevisBack.Api.Account.AccountRequest;
using DevisBack.Api.Account.Models;
using DevisBack.Tools.StaticTools;
using DevisBack.Api.Access.AccessRequest;
using DevisBack.Api.Access.Models;

using ServiceStack.ServiceInterface.Validation;
using ServiceStack.WebHost.Endpoints;

using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace DevisBack
{
	public class Global : System.Web.HttpApplication
	{
		public class AppHost :  AppHostHttpListenerBase
		{
			//Tell ServiceStack the name of your application and where to find your services
			public AppHost() : base("Devis",typeof(AccessGroupModel).Assembly) 
			{
               
	    
            }

            public override void Configure(Funq.Container container)
            {


                //Permit modern browsers (e.g. Firefox) to allow sending of any REST HTTP Method
                SetConfig(new EndpointHostConfig
                {
                    GlobalResponseHeaders =
                    {
                        { "Access-Control-Allow-Origin", "*" },
                        { "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS" },
                        { "Access-Control-Allow-Headers", "Content-Type" },
                    },
                });

				// Enable the validation feature
				Plugins.Add(new ValidationFeature());




                //register any dependencies your services use, e.g:
                //container.Register<ICacheClient>(new MemoryCacheClient());
                Routes
                    .Add(typeof(AccessGroupRequest), "/Acces", "POST", "Test", "Test group")
                    .Add(typeof(AccessUnitRequest), "/AccesChamps/{Token}", "GET")
                    .Add(typeof(AccessUnitRequest), "/AccesChamps/{Token}/{Id}", "GET")
                    .Add(typeof(AccessUnitRequest), "/AccesChamps/{Token}/{Name}/{NameClassAssociate}", "GET")

                    .Add(typeof(AccessUnitRequest),
                        "/AccesChamps/{Token}/{NameField}/{NameClassAssociate}/{Readable}/{Writable}", "POST")
                    //this get is used when the user forgot his password and he want to change
                    .Add(typeof(AuthRequest), "/Auth/Forgot/{Email}", "GET")
                    .Add(typeof(AuthRequest), "/Auth/{Token}", "GET")
                    .Add(typeof(AuthRequest), "/Auth/{Email}/{Password}", "GET")
                    .Add(typeof(AuthRequest), "/Auth/Mail/{Email}/{DoubleAuthenticate}", "GET")

                    .Add(typeof(AuthRequest), "/Auth/Signup/{Email}/{Password}", "POST")

                    .Add(typeof(AuthRequest), "/Auth/Update/{Token}/{Password}", "PUT")
                    .Add(typeof(AuthRequest), "/Auth/Disconnect/{Token}", "PUT")

                    .Add(typeof(AuthRequest), "/Auth/Delete/{Token}", "DELETE")


                    .Add<FieldNameRequest>("/Champs/{Token}", "GET")
                    .Add<FieldNameRequest>("/Champs/{Token}/{Id}", "GET")
                    .Add<FieldNameRequest>("/Champs/{Token}/{Name}", "GET")
                    .Add<FieldNameRequest>("/Champs/{Token}/{AccessUnitModelId}", "GET")
                    .Add<FieldNameRequest>("/Champs/{Token}/{Id}/{AccessUnitModelId}", "GET")

                    .Add<FieldNameRequest>("/Champs/{Token}/{strArrayName}", "POST")

                    //TODO: For create permission add token to route, and check that token is for super root access
                    .Add(typeof(PermissionRequest), "/Permissions", "GET")
                    .Add(typeof(PermissionRequest), "/Permissions/{Readable}/{Writable}", "GET")

                    .Add(typeof(PermissionRequest), "/Permissions/Add/{Readable}/{Writable}", "POST")


                    .Add(typeof(ProfilRequest), "/Profils", "GET", "Profil", "List of profils")
                    .Add(typeof(ProfilRequest), "/Profils/{Id}", "GET", "Profil", "Get profil by Id")
                    .Add(typeof(ProfilRequest), "/Profils/{Name}", "GET", "Profil", "Get profil by Name")

                    .Add(typeof(ProfilRequest), "/Profils", "POST", "Write Profil", "Save profil with Json data")

                    .Add<UserRequestModel>("/User/{Token}", "GET")
                    .Add<UserRequestModel>("/User/{Token}/{NameProfil}/{FirstName}/{LastName}", "GET")
                    .Add<UserRequestModel>("/User/{Token}/{Id}", "GET")

                    .Add<UserRequestModel>("/User/{Token}/{FirstName}/{LastName}", "POST")



                    .Add(typeof(TableAccessCompositionRequest), "/Access", "GET, POST", "Write Access", "Write access with json: childGroup represents a group of access which can have another  Eg:" +
                    "{ \"Name\":\"Composite1\", \"Permission\":{ \"Readable\": \"true\", \"Writable\":\"false\"}, \"ChildGroup\":{\"Name\":\"Composite4\"," +
                    " \"Permission\":{ \"Readable\": \"false\", \"Writable\":\"true\"}," +
                    " \"ListAccessUnit\":[{\"Permission\":{\"Readable\": \"true\", \"Writable\":\"false\"}," +
                    " \"FieldName\":{\"Name\":\"CoefficientUsed\", \"NameClassAssociate\": \"DevisBack.Api.ClassicCleaning.Model.PurchasseEquipmentModel\"}},]}," +
                    "\"ListAccessUnit\":[{\"Permission\":{\"Readable\": \"true\", \"Writable\":\"true\"}," +
                    " \"FieldName\":{\"Name\":\"Address\", \"NameClassAssociate\": \"DevisBack.Api.ClassicCleaning.Model.Client\"}}," +
                    " {\"Permission\":{\"Readable\": \"false\", \"Writable\":\"false\"},\"FieldName\":{\"Name\":\"City\", \"NameClassAssociate\": \"DevisBack.Api.ClassicCleaning.Model.Client\"}" +
                    "}]}")

                    .Add(typeof(TableAccessCompositionRequest), "/Access/ByComposite/{CompositeId}", "GET", "Read Access", "return list of Access got by CompositeId")
                    .Add(typeof(TableAccessCompositionRequest), "/Access/ByLeaf/{LeafId}", "GET", "Read Access", "Return list of Access Got by LeafId")
                    ;

                    

                AppSettings appSettings = new AppSettings();

                container.Register<IDbConnectionFactory>(c => new OrmLiteConnectionFactory(
                 appSettings.GetString("ConnectionString"), MySqlDialect.Provider));
                
                
                    using (var db = container.Resolve<IDbConnectionFactory>().Open())
                    {

                        
                        try
                        {
                           
                            db.CreateTableIfNotExists<PermissionModel>();
                            db.ExecuteSql("INSERT IGNORE INTO PermissionModel (Readable, Writable) VALUES(1, 1)");
                            db.ExecuteSql("INSERT IGNORE INTO PermissionModel (Readable, Writable) VALUES(1, 0)");
                            db.ExecuteSql("INSERT IGNORE INTO PermissionModel (Readable, Writable) VALUES(0, 1)");
                            db.ExecuteSql("INSERT IGNORE INTO PermissionModel (Readable, Writable) VALUES(0, 0)");
                            db.CreateTableIfNotExists<AuthModel>();
                            db.CreateTableIfNotExists<FieldNameModel>();
                            db.CreateTableIfNotExists<TableAccessCompositionModel>();
                            db.CreateTableIfNotExists<AccessGroupModel>();
                            db.CreateTableIfNotExists<AccessUnitModel>();
                            db.CreateTableIfNotExists<ProfilRequest>();
                            db.CreateTableIfNotExists<UserModel>();
                           
           
                        }catch (MySqlException e)
                        {
						Console.WriteLine("==> {0}", e.Message);

                        }
                        

						//TODO: create class that allow to create table(eg:authorization) to associate
						// table listeField with accessUser and permission
						//Create static class(like StoreFieldToDatabase) that check authorization
						// then give or deny access. This class will be use in method on service
						// Like On eg: get(AuthRequestModel request)
						StoreFieldToDatabase.DumpAttributes (typeof(AuthModel));
						
                }

                
               
            }

		   
        }

		//Initialize your application singleton
		protected void Application_Start(object sender, EventArgs e)
		{
			new AppHost().Init();
		}
	}
}
