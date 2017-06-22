using DevisBack.Api.Account.AccountRequest;
using DevisBack.Api.Account.Models;
using DevisBack.Tools.StaticTools;
using ServiceStack.ServiceInterface;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevisBack.Account.Api.AccountResponse;
using System.Data;
using MySql.Data.MySqlClient;
using DevisBack.Api.Access.Services;
using DevisBack.Api.Access.Models;
using ServiceStack.Common;
using ServiceStack.Common.Extensions;

namespace DevisBack.Api.Account.Services
{
    public class ProfilService: Service
    {
        public List<ProfilModel> Get(ProfilRequest request)
        {
            return Db.Select<ProfilModel>();
        }

        public ProfilResponse Post(ProfilRequest request)
        {
            ComplexeRequest.PopulateRequest(ref request, Request);
            /*ProfilModel profil = new ProfilModel
            {
                Id = request.Id,
                Name = request.Name,
                AccessGroupModelId = request.AccessGroupModelId,
                Access = request.Access
            };
            TableAccessCompositionModel table = Db.Select<TableAccessCompositionModel>(x => x.AccessComposite == profil.Access).FirstOrDefault();
           // profil.AccessGroupModelId = table.AccessComposite.Id;
            int? lastId = null;
            using(IDbTransaction trans = Db.OpenTransaction())
            {
                try
                {
                    ReferenceToDataBase.Save(Db, profil, ref lastId);
                    trans.Commit();
                }catch(MySqlException mse)
                {
                    var innerTrans = mse.InnerException;
                    Console.WriteLine(mse);
                    trans.Rollback();
                    return new ProfilResponse
                    {
                        Code = CodeHttp.INTERNAL_SERVER_ERROR
                    };
                }
            }
            */
            return new ProfilResponse
            {
                Code = CodeHttp.OK
            };
        }
    }
}