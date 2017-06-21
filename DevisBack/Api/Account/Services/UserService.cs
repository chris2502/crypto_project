using System;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using ServiceStack.Text;
using DevisBack.Api.Account.AccountRequest;
using DevisBack.Api.Account.Models;
using DevisBack.Account.Api.AccountResponse;
using ServiceStack.ServiceInterface;
using DevisBack.Tools.StaticTools;
using MySql.Data.MySqlClient;
using System.Data;
using DevisBack.Api.DevisException;

namespace DevisBack.Api.Account.Services
{
	public class UserService: Service
	{
		public UserResponse Get(UserRequest request)
		{
            
            if(request.Token != null)
            {
                AuthModel auth = Db.SingleWhere<AuthModel>("Token", request.Token);
                UserModel user = Db.SingleWhere<UserModel>("AuthModelId", auth.Id);
                ProfilModel profil = Db.SingleWhere<ProfilModel>("Id", user.ProfilModelId);
                user.Auth = auth;
                user.Profil = profil;
                return new UserResponse
                {
                    ListUser = new List<UserModel>
                    (
                        new UserModel[]
                        {
                            user
                        }
                    )
                };
            }
            return new UserResponse
            {
                ListUser = Db.Select<UserModel>()
            };
        }


        public UserResponse Post(UserRequest request)
        {
           // ComplexeRequest.PopulateRequest(ref request, Request);

            AuthModel authTemp = Db.SingleWhere<AuthModel>("Token", request.Token);
            if (!Authorizations.IsConnect(Db, request.Token))
                throw new AuthorizationException("Permission denied: You do not connect", CodeHttp.BAD_REQUEST);
            ProfilModel profilTemp = null;
            if (request.ProfilId != null)
                profilTemp = Db.SingleWhere<ProfilModel>("ProfilId", request.ProfilId);
            
            UserModel user = new UserModel
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                AuthModelId =authTemp.Id,
                ProfilModelId = (profilTemp != null) ?profilTemp.Id:null  
            };
            using(IDbTransaction trans = Db.OpenTransaction())
            {
                try
                {
                    Db.Save(user);
                    trans.Commit();
                }
                catch (MySqlException mse)
                {

                    return new UserResponse
                    {
                        ErrorCode = CodeHttp.INTERNAL_SERVER_ERROR,
                        Message = mse.Message
                    };
                }
            }

            
            return new UserResponse
            {
                ErrorCode = CodeHttp.OK
            };
        }
    }
}
