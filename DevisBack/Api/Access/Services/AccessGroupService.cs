using ServiceStack;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using DevisBack.Api.Access.Models;
using DevisBack.Api.Access.AccessReponse;
using ServiceStack.ServiceInterface;
using DevisBack.Tools.StaticTools;
using DevisBack.Api.Access.AccessRequest;

namespace DevisBack.Api.Access.Servicess
{
    public class AccessGroupService: Service
    {
        public object Get(AccessGroupRequest request)
        {

           /* if (request.Id != null)
            {
                return Db.SingleWhere<AccessGroupModel>("Id", request.Id);
            }*/

            return Db.Select<AccessGroupModel>();
        }

        public object Post(AccessGroupRequest request)
        {
            ComplexeRequest.PopulateRequest(ref request, Request);
            AccessGroupModel accessGroup = new AccessGroupModel
            {
            //    ListAccessAppli = request.ListAccessAppli,
                Permission = request.Permission
            };
            Db.Save(accessGroup);

			AccessGroupResponse accessGroupResponseModel = new AccessGroupResponse ();

            return accessGroupResponseModel;
        }
    }
}