using System.Collections.Generic;
using ServiceStack.OrmLite;
using DevisBack.Api.Account.Models;
using DevisBack.Account.Api.AccountResponse;
using ServiceStack.ServiceInterface;
using DevisBack.Tools.StaticTools;
using System.Data;
using MySql.Data.MySqlClient;
using DevisBack.Api.Account.AccountRequest;

namespace DevisBack.Api.Account.Services
{
    public class ProfilService : Service
    {
        public ProfilResponse Get(ProfilRequest request)
        {
            ProfilModel tmp = null;
            if (request.Id != null)
            {
                tmp = Db.SingleWhere<ProfilModel>("Id", request.Id);
            }

            else if (request.Name != null)
            {
                tmp = Db.SingleWhere<ProfilModel>("Name", request.Name);

            }

            else
            {
                return new ProfilResponse
                {

                    OneProfil = Db.Select<ProfilModel>(),
                    Code = CodeHttp.OK
                };
            }

            return (tmp != null) ? new ProfilResponse
            {

                OneProfil = new List<ProfilModel>(new ProfilModel[] { tmp }),
                Code = CodeHttp.OK
            } : new ProfilResponse
            {

                Code = CodeHttp.NO_CONTENT
            };
        }


        public ProfilResponse Post(ProfilRequest request)
        {
            ComplexeRequest.PopulateRequest(ref request, Request);

            ProfilModel profil = new ProfilModel
            {
                Name = request.Name,
                AccessGroupModelId = request.AccessGroupModelId
            };
            using (IDbTransaction trans = Db.OpenTransaction())
            {
                try
                {
                    Db.Save(profil);
                    trans.Commit();
                }
                catch (MySqlException mse)
                {
                    trans.Rollback();
                    return new ProfilResponse
                    {
                        Code = CodeHttp.INTERNAL_SERVER_ERROR,
                        Message = mse.Message
                    };
                }
            }
            return new ProfilResponse
            {
                Code = CodeHttp.OK
            };
        }
    }
}