using ServiceStack;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using DevisBack.Api.Access.Models;
using DevisBack.Api.Access.AccessReponse;
using ServiceStack.ServiceInterface;
using DevisBack.Tools.StaticTools;
using DevisBack.Api.Access.AccessRequest;
using DevisBack.Api.DevisException;

namespace DevisBack.Api.Access.Servicess
{
    public class AccessGroupService: Service
    {
        public AccessGroupResponse Get(AccessGroupRequest request)
        {
            if (!Authorizations.IsAdmin(Db, request.Token))
                throw new AuthorizationException("Permission denied: You are not admin", CodeHttp.BAD_REQUEST);
         
            if (request.Id != null)
            {
                return new AccessGroupResponse
                {
                    ListAccessGroup = Db.Where<AccessGroupModel>("Id", request.Id)
                };
            }

            return new AccessGroupResponse
            {
                ListAccessGroup = Db.Select<AccessGroupModel>()
            }; 
        }

        public AccessGroupResponse Post(AccessGroupRequest request)
        {
            ComplexeRequest.PopulateRequest(ref request, Request);
            if (!Authorizations.IsAdmin(Db, request.Token))
            {
                if (!Authorizations.IsAdmin(Db, request.Token))
                    throw new AuthorizationException("Permission denied: You are not admin", CodeHttp.BAD_REQUEST);
            }
            AccessGroupModel accessGroup = new AccessGroupModel
            {
            //    ListAccessAppli = request.ListAccessAppli,
                Permission = request.Permission
            };
            Db.Save(accessGroup);

            return new AccessGroupResponse
            {
                Code = CodeHttp.OK
            };
        }
    }
}