using System;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using ServiceStack.Text;
using DevisBack.Api.Account.AccountRequest;
using DevisBack.Api.Account.Models;
using DevisBack.Account.Api.AccountResponse;
using ServiceStack.ServiceInterface;

namespace DevisBack.Api.Account.Services
{
	public class UserService: Service
	{
		public object Get(UserRequestModel request)
		{
            
            if(request.Id != null)
            {
                List<UserModel> userList = Db.Select<UserModel>("Id = @Id", new { Id = request.Id});
                List<UserResponse> userResponseList = new List<UserResponse>();
                foreach(UserModel userTmp in userList)
                {
                    userResponseList.Add(new UserResponse
                    {
                        FirstName = userTmp.FirstName,
                        LastName = userTmp.LastName,
                        nameProfil = userTmp.Profil.Name,
                        Token = userTmp.Auth.Token
                    });
                }
                return userResponseList;
            }

            else if(request.FirstName != null && request.LastName != null)
            {
                List<UserModel> userList = Db.Select<UserModel>("Id = @Id AND FirstName = @FirstName AND LastName = @LastName", 
                                                                new
                                                                {
                                                                    Id = request.Id,
                                                                    FirstName = request.FirstName,
                                                                    LastName =request.LastName
                                                                });

                List<UserResponse> userResponseList = new List<UserResponse>();
                foreach (UserModel userTmp in userList)
                {
                    userResponseList.Add(new UserResponse
                    {
                        FirstName = userTmp.FirstName,
                        LastName = userTmp.LastName,
                        nameProfil = userTmp.Profil.Name,
                        Token = userTmp.Auth.Token
                    });
                }
                return userResponseList;
            }

            return Db.Select<UserModel>();

        }


        public object Post(UserRequestModel request)
        {
            AuthModel authTemp = Db.SingleWhere<AuthModel>("Token", request.Token);
            var user = new UserModel
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Auth = authTemp,
                Profil = new ProfilModel
                            {
                                Name = request.NameProfil
                            }
            };
            Db.Save(user);

			UserResponse userResponseModel = new UserResponse ();
            
            return userResponseModel;
        }
    }
}
