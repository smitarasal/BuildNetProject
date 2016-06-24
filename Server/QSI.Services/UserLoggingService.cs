using AutoMapper;
using Newtonsoft.Json;
using QSI.Data;
using QSI.Data.Spec;
using QSI.Domain;
using QSI.Services.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services
{
    [ServiceBehavior]
    public class UserLoggingService : IUserLoggingService
    {
        IUserLoggingRepository _userLoggingRepository;

        public UserLoggingService()
        {
            if (_userLoggingRepository == null)
                _userLoggingRepository = new UserLoggingRepository();
        }

        public UserLoggingService(IUserLoggingRepository userLoggingRepository)
        {
            _userLoggingRepository = userLoggingRepository;
        }

        public UserLoggingResponse GetUserLoggingDetails(string UserId)
        {
            UserLoggingResponse response = new UserLoggingResponse();
            var userId = Guid.Parse(UserId);

            var userLogging = _userLoggingRepository.GetWhere(m => m.UserId == userId).FirstOrDefault();

            if (userLogging != null)
            {
                UserLoggingDto userLoggingDto = Mapper.Map<UserLogging, UserLoggingDto>(userLogging);
                response.UserLoggingDetails = JsonConvert.SerializeObject(userLoggingDto);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Invalid User Id.", Status = "Failed" };
                response.UserLoggingDetails = JsonConvert.SerializeObject(obj);
            }

            return response;
        }

    }
}
