using AutoMapper;
using Newtonsoft.Json;
using QSI.Data;
using QSI.Data.Spec;
using QSI.Domain;
using QSI.Services.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services
{
    [ServiceBehavior]
    public class ValidationService : IValidationService
    {
        IUserManagementRepository _userManagementRepository;

        public ValidationService()
        {
            if (_userManagementRepository == null)
                _userManagementRepository = new UserManagementRepository();
        }

        public ValidationService(IUserManagementRepository userManagementRepository)
        {
            _userManagementRepository = userManagementRepository;
        }

        public void ValidateEmailAddress(string EmailAddress)
        {
            UserResponse resposne = new UserResponse();

            var validUser = _userManagementRepository.GetWhere(m => m.EmailAddress == EmailAddress).ToList();

            if (validUser != null && validUser.Count > 0)
            {
                //UsersDto usersDto = Mapper.Map<User, UsersDto>(validUser);
                //resposne.UserDetails = JsonConvert.SerializeObject(usersDto);

                throw new WebFaultException<string>("Valid EmailAddress", System.Net.HttpStatusCode.Found);
            }
            else
            {
                //ErrorObject obj = new ErrorObject { Message = "Invalid EmailAddress.", Status = "Failed" };
                //resposne.UserDetails = JsonConvert.SerializeObject(obj);
                throw new WebFaultException<string>("In Valid User", System.Net.HttpStatusCode.NotFound);
            }


            //return resposne;
        }
    }
}
