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
    public class UserPreferencesService : IUserPreferencesService
    {
        IUserPreferencesRepository _userPreferencesRepository;

        public UserPreferencesService()
        {
            if (_userPreferencesRepository == null)
                _userPreferencesRepository = new UserPreferencesRepository();
        }

        public UserPreferencesService(IUserPreferencesRepository userPreferencesRepository)
        {
            _userPreferencesRepository = userPreferencesRepository;
        }


        public UserPreferencesResponse GetUserPreferenceById(string UserId)
        {
            var userId = Guid.Parse(UserId);
            var validUserPreference = _userPreferencesRepository.GetWhere(m => m.UserId == userId).ToList();

            UserPreferencesResponse response = new UserPreferencesResponse();

            if (validUserPreference != null)
            {
                List<UserPreferenceDto> userPreferenceDto = Mapper.Map<List<UserPreference>, List<UserPreferenceDto>>(validUserPreference);
                response.UserPreferenceDetails = JsonConvert.SerializeObject(userPreferenceDto);

            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Invalid UserId. Data not found.", Status = "Failed" };
                response.UserPreferenceDetails = JsonConvert.SerializeObject(obj);


            }
            return response;
        }



        public UserPreferencesResponse SaveUserPreferences(UserPreferenceDto userPreferenceDto)
        {
            userPreferenceDto.Id = Guid.NewGuid();
            userPreferenceDto.CreatedAt = System.DateTime.Now;
            userPreferenceDto.UpdatedAt = System.DateTime.Now;

            UserPreferencesResponse response = new UserPreferencesResponse();

            UserPreference newUserPreference = Mapper.Map<UserPreferenceDto, UserPreference>(userPreferenceDto);
            var userPreference = _userPreferencesRepository.Insert(newUserPreference);
            _userPreferencesRepository.Save();

            if (userPreference == null)
            {
                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.UserPreferenceDetails = JsonConvert.SerializeObject(obj);
            }
            else
            {

                ErrorObject obj = new ErrorObject { Message = "Information Saved Successfully", Status = "Success" };
                response.UserPreferenceDetails = JsonConvert.SerializeObject(userPreferenceDto.Id);
            }
            return response;
        }


        public UserPreferencesResponse UpdateUserPreferences(UserPreferenceDto userPreferenceDto)
        {
            //var userId = Guid.Parse(UserId);
            UserPreferencesResponse response = new UserPreferencesResponse();

            UserPreference newUserPreference = Mapper.Map<UserPreferenceDto, UserPreference>(userPreferenceDto);
            //var oldUserPreference = _userPreferencesRepository.GetWhere(m => m.UserId == newUserPreference.UserId).FirstOrDefault();

            newUserPreference.UpdatedAt = System.DateTime.Now;
            var result = _userPreferencesRepository.Update(newUserPreference);
            _userPreferencesRepository.Save();
            if (result == null)
            {

                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.UserPreferenceDetails = JsonConvert.SerializeObject(obj);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "User Preferences Updated Successfully", Status = "Success" };
                response.UserPreferenceDetails = JsonConvert.SerializeObject(obj);
            }


            return response;
        }

        public UserPreferencesResponse DeleteUserPreferences(string UserId)
        {
            UserPreferencesResponse response = new UserPreferencesResponse();
            var userId = Guid.Parse(UserId);
            var userPreference = _userPreferencesRepository.GetWhere(m => m.UserId == userId).FirstOrDefault();
            if (userPreference != null)
            {
                _userPreferencesRepository.Delete(userPreference);
                _userPreferencesRepository.Save();
                ErrorObject obj = new ErrorObject { Message = "User Preferences Deleted Successfully", Status = "Success" };
                response.UserPreferenceDetails = JsonConvert.SerializeObject(obj);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.UserPreferenceDetails = JsonConvert.SerializeObject(obj);
            }
            return response;
        }
    }
}
