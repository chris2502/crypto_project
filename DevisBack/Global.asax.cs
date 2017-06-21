
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
using DevisBack.Api.Access.Services;

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

				// This method scans the assembly for validators
				container.RegisterValidators(typeof(AccessUnitService).Assembly);


                //register any dependencies your services use, e.g:
                //container.Register<ICacheClient>(new MemoryCacheClient());
                Routes
                    .Add(typeof(AccessGroupRequest), "/AccessGroup/{Token}", "POST, GET", "Test", "Test group")

                    .Add(typeof(AccessUnitRequest), "/AccesChamps/{Token}", "GET")
                    .Add(typeof(AccessUnitRequest), "/AccesChamps/{Token}/{Id}", "GET")
                    .Add(typeof(AccessUnitRequest), "/AccesChamps/{Token}/{Name}/{NameClassAssociate}", "GET")

                    .Add(typeof(AccessUnitRequest),
                        "/AccesChamps/{Token}/{NameField}/{NameClassAssociate}/{Readable}/{Writable}", "POST")


                    .Add(typeof(AuthRequest), "/Auths/{Email}", "GET", "Authentication",
                                                "this get is used when the user forgot his password and he want to change")

                    .Add(typeof(AuthRequest), "/Auths/{Token}/{Email}/{Password}", "POST", "Authentication",
                                                "When verb used, is POST, it's for sign up. else it's for connect to app" +
                                                "When you used POST, you need token Admin")

                    .Add(typeof(AuthRequest), "/Auths/{Email}/{Password}", "GET", "Authentication",
                                                "When verb used, is POST, it's for sign up. else it's for connect to app" +
                                                "When you used POST, you need token Admin")
                    .Add(typeof(AuthRequest), "/Auths/{Token}/{Password}", "PUT", "Authentication",
                                                "It's used to change password")

                    .Add(typeof(AuthRequest), "/Auths/{Token}", "PUT, DELETE, GET", "Authentication",
                                                "Verb PUT is uused to disconnect to app. Delete is used to  delete user." +
                                                 "Get is used to list info's authentication")









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


                    .Add(typeof(ProfilRequest), "/Profils", "GET,POST", "Profil", "List of profils")
                    .Add(typeof(ProfilRequest), "/Profils/{Id}", "GET", "Profil", "Get profil by Id")
                    .Add(typeof(ProfilRequest), "/Profils/{Name}", "GET", "Profil", "Get profil by Name")

                    .Add(typeof(ProfilRequest), "/Profils", "POST", "Write Profil", "Save profil with Json data")

                    .Add<UserRequest>("/Users/{Token}", "GET")
                    .Add<UserRequest>("/Users/{Token}/{NameProfil}/{FirstName}/{LastName}", "GET")
                    .Add<UserRequest>("/Users/{Token}/{Id}", "GET")

                    .Add<UserRequest>("/User/{Token}/{FirstName}/{LastName}", "POST")

                    .Add(typeof(TableAccessCompositionRequest), "/Access", "GET, POST")

                    .Add(typeof(TableAccessCompositionRequest), "/Access/ByComposite/{CompositeId}", "GET", "Read Access", "return list of Access got by CompositeId")
                    .Add(typeof(TableAccessCompositionRequest), "/Access/ByLeaf/{LeafId}", "GET", "Read Access", "Return list of Access Got by LeafId")

                    .Add(typeof(TableAccessCompositionRequest), "/Access/{Id}", "DELETE", "Access", "Delete access");


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
                            db.CreateTableIfNotExists<ProfilModel>();
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
