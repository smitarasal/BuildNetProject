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
    public class UserDataService : IUserDataService
    {

        IUserDataRepository _userDataRepository;

        public UserDataService()
        {
            if (_userDataRepository == null)
                _userDataRepository = new UserDataRepository();
        }

        public UserDataService(IUserDataRepository userDataRepository)
        {
            _userDataRepository = userDataRepository;
        }


        public UserDataResponse SaveUserData(UserDataDto userDataDto)
        {

            userDataDto.Id = Guid.NewGuid();
            userDataDto.CreatedOn = DateTime.Now;
            userDataDto.ModifiedOn = DateTime.Now;
            UserDataResponse response = new UserDataResponse();
            var serverUserData = _userDataRepository.GetWhere(m => m.UserId == userDataDto.UserId && m.Keys==userDataDto.Keys);
            if (serverUserData == null || serverUserData.Count() == 0)
            {
                UserData newUserData = Mapper.Map<UserDataDto, UserData>(userDataDto);

                var userData = _userDataRepository.Insert(newUserData);
                _userDataRepository.Save();
                if (userData == null)
                {
                    ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                    response.UserData = JsonConvert.SerializeObject(obj);
                }
                else
                {

                    ErrorObject obj = new ErrorObject { Message = "Information Saved Successfully", Status = "Success" };
                    response.UserData = JsonConvert.SerializeObject(userDataDto.Id);
                }
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Error in processing request.The Key already exists.", Status = "Failed" };
                response.UserData = JsonConvert.SerializeObject(obj);
            }

            return response;
        }


        public UserDataResponse GetUserData(string UserId)
        {
            throw new NotImplementedException();
        }




        public UserDataResponse UpdateUserData(UserDataDto userDataDto)
        {
            userDataDto.CreatedOn = DateTime.Now;
            userDataDto.ModifiedOn = DateTime.Now;
            UserDataResponse response = new UserDataResponse();

            UserData newUserData = Mapper.Map<UserDataDto, UserData>(userDataDto);

            var userData = _userDataRepository.Update(newUserData);
            _userDataRepository.Save();
            if (userData == null)
            {
                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.UserData = JsonConvert.SerializeObject(obj);
            }
            else
            {

                ErrorObject obj = new ErrorObject { Message = "Information Modified Successfully", Status = "Success" };
                response.UserData = JsonConvert.SerializeObject(obj);
            }

            return response;
        }


        public UserDataResponse DeleteUserDataByKey(UserDataDto userDataDto)
        {
            UserDataResponse response = new UserDataResponse();

            var userDataLst = _userDataRepository.GetWhere(m => m.UserId == userDataDto.UserId && m.Keys == userDataDto.Keys);
            var userData = userDataLst.FirstOrDefault();

            if (userData != null)
            {
                _userDataRepository.Delete(userData);
                _userDataRepository.Save();
                ErrorObject obj = new ErrorObject { Message = "User Data Deleted Successfully.", Status = "Success" };
                response.UserData = JsonConvert.SerializeObject(obj);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.UserData = JsonConvert.SerializeObject(obj);
            }



            return response;
        }
    }
}
