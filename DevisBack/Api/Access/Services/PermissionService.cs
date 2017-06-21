using ServiceStack;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using DevisBack.Api.Access.Models;
using DevisBack.Tools.StaticTools;
using DevisBack.Api.Access.AccessRequest;
using DevisBack.Api.Access.AccessReponse;
using ServiceStack.ServiceInterface;

namespace DevisBack.Api.Access.Services
{
	public class PermissionService: Service
	{
		public PermissionResponse Get(PermissionRequest request)
		{
			PermissionResponse permissionResponse = new PermissionResponse();
			List<PermissionModel> permissionList = null;

			if (request.Readable != null && request.Writable != null) 
			{
				permissionList = Db.Select<PermissionModel> ("Readable = @Readable AND Wrtiable = @Writable", 
					new{Readable = request.Readable, Writable = request.Writable});
			} 
			else if (request.Readable != null && request.Writable == null) 
			{
				permissionList = Db.Select<PermissionModel> ("Readable = @Readable", 
					new{Readable = request.Readable});
			} 
			else if (request.Readable == null && request.Writable != null) 
			{
				permissionList = Db.Select<PermissionModel> ("Writable = @Writable", 
					new{Writable = request.Writable});
			} 
			else 
			{
				permissionList = Db.Select<PermissionModel>();
			}

			foreach(PermissionModel permissionTmp in permissionList)
			{
				permissionResponse.Permission.Add (permissionTmp);
				permissionResponse.Code = CodeHttp.OK;
			}
			return permissionResponse;
		}

		public PermissionResponse Post(PermissionRequest request)
        {
            PermissionModel user = new PermissionModel
            {
                Readable = request.Readable,
                Writable = request.Writable
            };
			bool check = true;
			Db.Save(user);

            PermissionResponse userResponseModel = new PermissionResponse
            {
				Code = check ? CodeHttp.OK : CodeHttp.INTERNAL_SERVER_ERROR
            };

            return userResponseModel;
        }
    }
}
