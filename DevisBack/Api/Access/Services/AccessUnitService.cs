using ServiceStack;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using DevisBack.Api.Access.AccessRequest;
using DevisBack.Api.Access.AccessReponse;
using DevisBack.Api.Access.Models;
using DevisBack.Tools.StaticTools;
using ServiceStack.ServiceInterface;
using System.Linq.Expressions;
using System;

namespace DevisBack.Api.Access.Services
{
    public class AccessUnitService: Service
    {
        public AccessUnitResponse Get(AccessUnitRequest request)
        {
			List<AccessUnitModel> listAccessUnit = null;
			AccessUnitResponse accessUnit = new AccessUnitResponse();
			if (request.Id != null) 
			{
                /*listAccessUnit = Db.LoadSelect<AccessUnitModel>(
					Db.From<AccessUnitModel>().
					Where(x => x.Id == request.Id)
					);*/
                Expression<Func<AccessUnitModel, bool>> filter = x => x.Id == request.Id;
                //listAccessUnit = ReferenceToDataBase.select(Db, filter);
			} 
			else if (request.NameField !=null && request.NameClassAssociate != null) 
			{/*
				listAccessUnit = Db.LoadSelect (
					Db.From<AccessUnitModel> ()
					.Join<PermissionModel> ((Access, Permission) => Access.PermissionId == Permission.Id)
					.Join<FieldNameModel> ((Access, FieldName) => Access.FieldNameId == FieldName.Id &&
					FieldName.Name == request.NameField &&
					FieldName.NameClassAssociate == request.NameClassAssociate)                                
				);*/
			} 
			else 
			{/*
				listAccessUnit = Db.LoadSelect<AccessUnitModel>();*/
			}
				
			foreach (AccessUnitModel accessUnitTmp in listAccessUnit) 
			{
				accessUnit.ListAccessUnit.Add (accessUnitTmp);
				accessUnit.Code = CodeHttp.OK;
			}

			return accessUnit;
        }

		public AccessUnitResponse Post(AccessUnitRequest request)
        {

            bool check = false;
			AccessUnitModel accessUnit = new AccessUnitModel();
			if (request.FieldNameModelId != null) 
			{
				accessUnit.FieldName = new FieldNameModel 
				{
					Id = request.FieldNameModelId,
					//PermissionModelId = reques
				};

			} 
			else if (request.NameField == null) 
			{
				accessUnit.FieldName = new FieldNameModel 
				{
					Name = request.NameField,
					NameClassAssociate = request.NameClassAssociate
				};
			}
			accessUnit.Permission = new PermissionModel 
			{
				Readable = request.Readable,
				Writable = request.Writable,
			};


			Db.Save (accessUnit);
			check = true;
         /*   foreach(FieldNameModel field in request.FieldName)
            {
                FieldNameModel fieldNameTmp = Db.SingleWhere<FieldNameModel>("Id", field.Id);
                AccessUnitModel accessUnit = new AccessUnitModel
                {
                    FieldName = request.FieldName
                };
                check = Db.Save(accessUnit);
            }
           */ 

            AccessUnitResponse accessUnitResponseModel = new AccessUnitResponse
            {
                Code = check ? 0 : -1
            };

            return accessUnitResponseModel;
        }
    }
}