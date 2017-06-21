using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceStack.OrmLite;
using DevisBack.Api.Access.AccessReponse;
using DevisBack.Api.Access.Models;
using ServiceStack.ServiceInterface;

namespace DevisBack.Api.Access.Services
{
    public class AbstractAccessAppliService: Service
    {

        public object Get(AbstractAccessAppliModel request)
        {

          /*  if (request.Id != null)
            {
                return Db.SingleWhere<AbstractAccessAppliModel>("Id", request.Id);
            }
*/
            return Db.Select<AbstractAccessAppliModel>();
        }

        /*public object Post(AbstractAccessAppliModel request)
        {
            AccessUnitModel accessUnit = new AccessUnitModel
            {
               // FieldName = request.FieldName,
            };
  /*          bool check = Db.Save(accessUnit);

            AccessUnitResponse accessUnitResponseModel = new AccessUnitResponse
            {
                Code = check ? 0 : -1
            }; 

            return accessUnitResponseModel;
        }*/
    }
}