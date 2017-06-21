using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack;
using DevisBack.Api.Account.Models;
using DevisBack.Account.Api.AccountResponse;
using ServiceStack.ServiceInterface;

namespace DevisBack.Api.Access.Services
{
    public class ProfilService: Service
    {
        public object Get(ProfilModel request)
        {
            ProfilModel tmp = null;
            if (request.Id != null)
            {
                tmp = Db.SingleWhere<ProfilModel>("Id", request.Id);
                return (tmp != null) ? new ProfilResponse
                {

                    Id = tmp.Id,
                    Name = tmp.Name,
                    Code = 0
                } : new ProfilResponse
                {

                    Code = -1
                };
            }

            else if (request.Name != null)
            {
                tmp = Db.SingleWhere<ProfilModel>("Name", request.Name);
                return (tmp != null) ? new ProfilResponse
                {

                    Id = tmp.Id,
                    Name = tmp.Name,
                    Code = 0
                }: new ProfilResponse
                {

                    Code = -1
                };
            }
            List<ProfilModel>tmpList = Db.Select<ProfilModel>();
            return tmpList;
        }


        public object Post(ProfilModel request)
        {
            var profil = new ProfilModel
            {
                Name = request.Name
            }; 
			bool check = true; 
			Db.Save(profil);

            ProfilResponse profilResponseModel = new ProfilResponse
            {
                Code = check ? 0 : -1
            };

            return profilResponseModel;
        }
    }
}