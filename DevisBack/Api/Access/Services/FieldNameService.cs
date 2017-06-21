using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.ServiceInterface;
using DevisBack.Api.Access.AccessRequest;
using DevisBack.Api.Access.AccessReponse;
using DevisBack.Api.Account.Models;

namespace DevisBack.Api.Access.Services
{
    public class FieldNameService: Service
    {/*
        public List<FieldNameResponse> Get(FieldNameRequest request)
        {
            if (request.Token != null)
            {
                List<AuthModel> tmp = Db.Select(
                            Db.From<AuthModel>()
                                .Join<UserModel>((Auth, User) => Auth.UserModelId == User.Id)
                                .Join<UserModel, ProfilModel>((User, Profil) => User.Id == Profil.UserModelId)
                                .Join<ProfilModel, AbstractAccessAppliModel>((Profil, AbstractAccess) => Profil.Id == AbstractAccess.ProfilModelId)
                                .Join<AbstractAccessAppliModel, AccessUnitModel>((AbstractAccess, AccessUnit) => AbstractAccess.Id == AccessUnit.Id)
                                .Join<AccessUnitModel, FieldNameModel>((AccessUnit, FieldName) => FieldName.AccessUnitModelId == AccessUnit.Id)                                
                                .Where(Auth => Auth.Token == request.Token)
                                );
                if (tmp.IsNullOrEmpty())
                {
                    throw new ServiceResponseException("Vous devez être connecté pour utiliser l'api Champs");
                }
                
            }
            List<FieldNameModel> fieldList = null;
            List<FieldNameResponseModel> fieldResponseList = new List<FieldNameResponseModel>();

            if (request.Id != null)
            {
                fieldList = (request.AccessUnitModelId != null)
                            ?Db.Select<FieldNameModel>("Id = @Id AND AccessUnitModelId= @AccessUnitModelId ",
                                                    new { Id = request.Id, AccessUnitModelId = request.AccessUnitModelId })
                            : Db.Select<FieldNameModel>("Id = @Id", new { Id = request.Id });
                
                foreach (FieldNameModel fieldTmp in fieldList)
                {
                    fieldResponseList.Add(new FieldNameResponseModel
                    {
                        Name = fieldTmp.Name,
                        AccessUnitModelId = fieldTmp.AccessUnitModelId
                    });
                }
                return fieldResponseList;

            }

            else if (request.Name != null)
            {
                fieldList = Db.Select<FieldNameModel>("Name = @Name",
                                                                new
                                                                {
                                                                    Name = request.Name,
                                                                    
                                                                });

            }
            else
            {
                fieldList = Db.Select<FieldNameModel>();
            }

            foreach (FieldNameModel fieldTmp in fieldList)
            {
                fieldResponseList.Add(new FieldNameResponseModel
                {
                    Name = fieldTmp.Name,

                });
            }
            return fieldResponseList;
        }

        public FieldNameResponse Post(FieldNameRequest request)
        {
            FieldNameResponse fieldResponseModel = null;
           // request.Name = request.strArrayName.Split(';');
            foreach (string nameField in request.Name)
            {
                var field = new FieldNameModel
                {
                    Name = nameField
                };
                bool check = Db.Save(field, references: true);

                fieldResponseModel = new FieldNameResponseModel
                {
                    Code = check ? 0 : -1
                };
            }

            return fieldResponseModel;
        }*/
    }
}