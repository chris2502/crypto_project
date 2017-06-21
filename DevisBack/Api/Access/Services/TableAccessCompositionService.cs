using System;
using ServiceStack;
using DevisBack.Api.Access.AccessReponse;
using DevisBack.Api.Access.AccessRequest;
using DevisBack.Tools.StaticTools;
using DevisBack.Api.Access.Models;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using System.Linq;
using ServiceStack.ServiceInterface;
using System.Data;
using MySql.Data.MySqlClient;

namespace DevisBack.Api.Access.Services
{
	public class TableAccessCompositionService : Service
	{

       
        public List<TableAccessCompositionResponse> GET(TableAccessCompositionRequest request)
        {
            List<TableAccessCompositionModel> listTableAccessModel = null;
            List<TableAccessCompositionResponse> resultTableAccessResponse = null;

            if (request.CompositeId != null)
                listTableAccessModel = Db.Query<TableAccessCompositionModel>("SelfId=@SelfId", new { SelfId = request.CompositeId});
            else if (request.LeafId != null)
                listTableAccessModel = Db.Query<TableAccessCompositionModel>("SelfAssociateId=@SelfAssociateId", new { SelfAssociateId = request.LeafId });
            else
                listTableAccessModel = Db.Select<TableAccessCompositionModel>();
            resultTableAccessResponse = new List<TableAccessCompositionResponse>();
            foreach (TableAccessCompositionModel tableAccess in listTableAccessModel)
            {
                TableAccessCompositionResponse tmpAccess = new TableAccessCompositionResponse();
                AccessGroupModel accessGroup = Db.Query<AccessGroupModel>("Id=@Id", new { Id = tableAccess.SelfId }).FirstOrDefault();
                if(accessGroup != null)
                {
                    PermissionModel permissionGroup = Db.Query<PermissionModel>("Id=@Id", new { Id = accessGroup.PermissionId }).FirstOrDefault();
                    tmpAccess.AccessParent = accessGroup;
                    List<AccessGroupModel> listTmpAccessGroup = Db.Query<AccessGroupModel>("Id=@Id", new { Id = tableAccess.SelfAssociateId });
                    if (listTmpAccessGroup.Count == 0)
                    {
                        AccessUnitModel tmpAccessUnit = Db.Query<AccessUnitModel>("Id=@Id", new { Id = tableAccess.SelfAssociateId }).FirstOrDefault();
                        PermissionModel permissionUnit = Db.Query<PermissionModel>("Id=@Id", new { Id = tmpAccessUnit.PermissionId }).FirstOrDefault();
                        FieldNameModel FieldName = Db.Query<FieldNameModel>("Id=@Id", new { Id = tmpAccessUnit.FieldNameId }).FirstOrDefault();
                        tmpAccess.AccessChild = tmpAccessUnit;
                        tmpAccess.AccessChild.Permission = permissionUnit;
                        tmpAccess.AccessChild.FieldName = FieldName;

                    }
                    else
                    {
                        AccessGroupModel tmpAccessGroup = Db.Query<AccessGroupModel>("Id=@Id", new { Id = tableAccess.SelfAssociateId }).FirstOrDefault();
                        tmpAccess.AccessParentChild = tmpAccessGroup;
                        tmpAccess.AccessParentChild.Permission = permissionGroup;
                    }
                    resultTableAccessResponse.Add(tmpAccess);
                }

            }

            return resultTableAccessResponse;
        }

        private bool SaveAccess(TableAccessCompositionRequest request, int? recLastGroupId=null)
        {
            int? lastGroupId = null;
            
            Db.Save(new TableAccessCompositionModel());
            lastGroupId = (int)Db.GetLastInsertId();
            if (recLastGroupId != null)
            {
                Db.Save(new TableAccessCompositionModel
                {
                    SelfId = recLastGroupId,
                    SelfAssociateId = lastGroupId
                });
                //PermissionModel model
            }
            request.Permission = Db.Query<PermissionModel>("Readable=@Readable AND Writable=@Writable",
                new { Readable = request.Permission.Readable, Writable = request.Permission.Writable }).FirstOrDefault();
            Db.Save(new AccessGroupModel
            {
                Id = lastGroupId,
                Name = request.Name,
                PermissionId = request.Permission.Id,
                Permission = request.Permission
            });

            // each accessunit is saved using accessGroup Associated 
            foreach (AccessUnitModel accessUnit in request.ListAccessUnit)
            {
                accessUnit.Permission = Db.Query<PermissionModel>("Readable=@Readable AND Writable=@Writable",
                    new { Readable = accessUnit.Permission.Readable, Writable = accessUnit.Permission.Writable }).FirstOrDefault();

                accessUnit.FieldName = Db.Query<FieldNameModel>("Name=@Name AND NameClassAssociate=@NameClassAssociate",
                    new { Name = accessUnit.FieldName.Name, NameClassAssociate = accessUnit.FieldName.NameClassAssociate }).FirstOrDefault();
                //save TableAcessCompositionModel To create and get id which uses to save inherit accessUnit
                Db.Save(new TableAccessCompositionModel());
                int lastUnitId = (int)Db.GetLastInsertId();
                //association of accessGroup and accessUnit via tableaccessCompositionModel
                Db.Update(new TableAccessCompositionModel
                {
                    Id = lastGroupId,
                    SelfId = lastGroupId,
                    SelfAssociateId = (int)Db.GetLastInsertId(),
                });
                Db.Save(new AccessUnitModel
                {
                    Id = lastUnitId,
                    PermissionId = accessUnit.Permission.Id, 
                    Permission = accessUnit.Permission,
                    FieldNameId = accessUnit.FieldName.Id,
                    FieldName = accessUnit.FieldName
                });
             }

            if(request.ChildGroup != null)
            {
                return SaveAccess(request.ChildGroup, lastGroupId);
            }
            return true;
        }


		public TableAccessCompositionResponse Post(TableAccessCompositionRequest request)
		{
			// Convert the JSON payload to DTO format
			ComplexeRequest.PopulateRequest(ref request, Request);
            using(IDbTransaction trans = Db.OpenTransaction())
            {
                try
                {
                    //save TableAcessCompositionModel To create and get id which uses to save inherit accessGroup
                    SaveAccess(request);
                    trans.Commit();
                }
                catch(MySqlException mse)
                {
                    trans.Rollback();
                    if(mse.Number == 1062)
                    {
                        return new TableAccessCompositionResponse
                        {
                            Code = CodeHttp.INTERNAL_SERVER_ERROR,
                            MessageError = mse.Message
                        };
                    }
                    Console.WriteLine(mse);
                    return new TableAccessCompositionResponse
                    {
                        Code = CodeHttp.INTERNAL_SERVER_ERROR
                    };
                }
            }

            List<PermissionModel> listPermission = Db.Select<PermissionModel> ();

            return new TableAccessCompositionResponse
            {
                Code = CodeHttp.OK
            };
		}

        public TableAccessCompositionResponse Put(TableAccessCompositionRequest request)
        {
            TableAccessCompositionResponse response = null;
            ComplexeRequest.PopulateRequest(ref request, Request);
            if(request.Id != null)
            {
                using(IDbTransaction trans = Db.OpenTransaction())
                {
                    try
                    {
                        Db.Update(new TableAccessCompositionModel
                        {
                            Id = request.Id,
                            SelfId = request.SelfId,
                            SelfAssociateId = request.SelfAssociateId
                        });
                    }
                    catch(MySqlException mse)
                    {
                        trans.Rollback();
                        return new TableAccessCompositionResponse
                        {
                            Code = CodeHttp.INTERNAL_SERVER_ERROR,
                            MessageError = mse.Message
                        };
                    }
                    trans.Commit();
                }
            }
            return new TableAccessCompositionResponse
            {
                Code = CodeHttp.OK
            };
        }
        
        public TableAccessCompositionResponse Delete(TableAccessCompositionRequest request)
        {
            
            if (request.Id != null)
            {
                using (IDbTransaction trans = Db.OpenTransaction())
                {
                    try
                    {
                        Db.Delete(new TableAccessCompositionModel
                        {
                            Id = request.Id
                        });
                        trans.Commit();
                    }
                    catch (MySqlException mse)
                    {
                        trans.Rollback();
                        return new TableAccessCompositionResponse
                        {
                            Code = CodeHttp.INTERNAL_SERVER_ERROR,
                            MessageError = mse.Message
                        };
                    }
                }
            }
            return new TableAccessCompositionResponse
            {
                Code = CodeHttp.OK
            };
        }


	}
}

