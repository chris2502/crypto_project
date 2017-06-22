using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceStack.OrmLite;
using DevisBack.Api.Account.AccountResponse;
using DevisBack.Api.Account.AccountRequest;
using DevisBack.Api.Account.Models;
using DevisBack.Tools.StaticTools;
using ServiceStack.ServiceInterface;
using MySql.Data.MySqlClient;

namespace DevisBack.Api.Account.Services
{
    public class AuthService : Service
    {
        public AuthResponse Get(AuthRequest request)
        {
            //try{

            /*AuthModel testAuth = new AuthModel{ Email = "sdsd@zeze.fr" };

            Authorizations.checkAuthorization <AuthModel>(Db, ref testAuth);
            }catch(UnauthorizedAccessException uae){
                throw new UnauthorizedAccessException(uae.Message);
            }*/
            if (request.DoubleAuthenticate != null)
            {
                AuthModel auth =
                    Db.Select<AuthModel>(x => x.DoubleAuthenticate == request.DoubleAuthenticate).FirstOrDefault();
                if (auth != null)
                {
                    return new AuthResponse
                    {
                        Email = auth.Email,
                        Token = auth.Token,
                        Code = CodeHttp.OK
                    };
                }
            }
            List<AuthModel> authList = null;
            AuthResponse authResponse = new AuthResponse();
            authResponse.Code = CodeHttp.UNAUTHORIZED;
            if (request.Token != null)
            {
                authList = Db.Select<AuthModel>(x => x.Token == request.Token && x.IsEnable == true);
            }
            else if (request.Email != null && request.Password != null)
            {
                authList =
                    Db.Select<AuthModel>(
                        x => x.Email == request.Email && x.Password == request.Password && x.IsEnable == true);

            }
            /*else if (request.Email != null)
            {

                authList = Db.Select<AuthModel>(x => x.Email == request.Email &&  x.IsEnable == true );
				int code = CodeHttp.INTERNAL_SERVER_ERROR;
                foreach (AuthModel authTmp in authList)
                {
				//	code = (authTmp.sendMail())?CodeHttp.OK:CodeHttp.INTERNAL_SERVER_ERROR;
                }
                return new AuthResponse
                {
                    Code = code
                };

            }*/
            foreach (AuthModel authTmp in authList)
            {
                if (authTmp.Token == null || authTmp.Token == "")
                {
                    authTmp.CreateToken("auth", true);
                    Db.Update<AuthModel>(new {Token = authTmp.Token},
                        Auth => Auth.Email == request.Email && Auth.IsEnable == true);

                }
                if (request.DoubleAuthenticate == null)
                {
                    authTmp.GenerataDoubleAuthenticate();
                    Db.Update(authTmp);
                    authTmp.sendMail("http://localhost:4200/token/" + request.Email + "/" + authTmp.DoubleAuthenticate,
                        request.Email);
                    return new AuthResponse
                    {
                        Code = CodeHttp.OK
                    };
                }


                authResponse = new AuthResponse
                {
                    Email = authTmp.Email,
                    Token = authTmp.Token,
                    Code = CodeHttp.OK
                };

            }
            return authResponse;
        }

        public AuthResponse Post(AuthRequest request)
        {
            if (!request.IsvalidEmail())
            {
                throw new FormatException("L'adresse email n'est pas valide");
            }
            AuthModel Auth = new AuthModel
            {
                Email = request.Email,
                Password = request.Password,
                IsEnable = true

            };
            bool check = true;
            try
            {
                Db.Save(Auth);
            }
            catch (MySqlException ms)
            {
                Console.WriteLine(ms);
                check = false;
            }
            AuthResponse authResponseModel = new AuthResponse
            {
                Code = check ? CodeHttp.OK : CodeHttp.INTERNAL_SERVER_ERROR
            };

            return authResponseModel;
        }

        public AuthResponse Put(AuthRequest request)
        {
            int code = -1;
            //to disconnect user
            if (request.Password == null)
            {
                code = Db.Update<AuthModel>(new {Token = DBNull.Value, DoubleAuthenticate = DBNull.Value},
                    Auth => Auth.Token == request.Token && Auth.IsEnable == true);
            }
            else
            {
                AuthModel tmp = Db.SingleWhere<AuthModel>("Token", request.Token);
                if (tmp != null && tmp.Token != null)
                {
                    AuthModel tmp2 = Db.SingleWhere<AuthModel>("Password", request.Password);

                    if (tmp2 != null && tmp2.Password != null)
                    {
                        return new AuthResponse
                        {
                            Code = CodeHttp.NO_CONTENT
                        };
                    }
                    code = Db.Update<AuthModel>(new {Password = request.Password},
                        Auth => Auth.Token == request.Token && Auth.IsEnable == true);
                }

            }
            return new AuthResponse
            {
                Code = (code == 1) ? CodeHttp.OK : CodeHttp.INTERNAL_SERVER_ERROR
            };
        }

        public AuthResponse Delete(AuthRequest request)
        {
            int code = Db.Update<AuthModel>(new {IsEnable = false}, p => p.Token == request.Token);
            return new AuthResponse
            {
                Code = (code == 1) ? CodeHttp.OK : CodeHttp.INTERNAL_SERVER_ERROR
            };
        }

        public AuthResponse Options(AuthRequest request)
        {
            return new AuthResponse()
            {
                Code = CodeHttp.OK
            };
        }
    }
}