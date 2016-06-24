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
    public class UserGroupService : IUserGroupService
    {
        IUserGroupRepository _userGroupRepository;

        public UserGroupService()
        {
            if (_userGroupRepository == null)
                _userGroupRepository = new UserGroupRepository();                                            
        }

        public UserGroupService(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }



        public UserGroupResponse GetAuthorizedUserGroups(string Id)
        {
            var userGroupId = Guid.Parse(Id);
            var validUserGroupLst = _userGroupRepository.GetWhere(m => m.Id == userGroupId);
            var validUserGroup = validUserGroupLst.FirstOrDefault();

            UserGroupResponse response = new UserGroupResponse();

            if (validUserGroup != null)
            {
                UserGroupDto userGroupsDto = Mapper.Map<UserGroup, UserGroupDto>(validUserGroup);
                response.UserGroupDetails = JsonConvert.SerializeObject(userGroupsDto);

            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Invalid UserGroup Id.", Status = "Failed" };
                response.UserGroupDetails = JsonConvert.SerializeObject(obj);
            }
            return response;
        }


        public UserGroupResponse GetAllUserGroups()
        {
            List<UserGroupDto> LstUserGroupDto = new List<UserGroupDto>();
            UserGroupResponse response = new UserGroupResponse();
            var userGroups = _userGroupRepository.GetAll().ToList();

            if (userGroups.Count() > 0)
            {
                foreach (var item in userGroups)
                {
                    UserGroupDto userGroupsDto = Mapper.Map<UserGroup, UserGroupDto>(item);
                    LstUserGroupDto.Add(userGroupsDto);
                }

                response.UserGroupDetails = JsonConvert.SerializeObject(LstUserGroupDto);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "No UserGroup data found.", Status = "Failed" };
                response.UserGroupDetails = JsonConvert.SerializeObject(obj);
            }
            return response;
        }


        public UserGroupResponse SaveUserGroup(UserGroupDto userGroupDto)
        {
            userGroupDto.Id = Guid.NewGuid();
            userGroupDto.CreatedAt = System.DateTime.Now;
            userGroupDto.ModifiedAt = System.DateTime.Now;

            UserGroupResponse response = new UserGroupResponse();

            UserGroup newUserGroup = Mapper.Map<UserGroupDto, UserGroup>(userGroupDto); 
            var userGroup = _userGroupRepository.Insert(newUserGroup);
            _userGroupRepository.Save();
            
            if (userGroup == null)
            {
                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.UserGroupDetails = JsonConvert.SerializeObject(obj);
            }
            else
            {

                ErrorObject obj = new ErrorObject { Message = "Information Saved Successfully", Status = "Success" };
                response.UserGroupDetails = JsonConvert.SerializeObject(userGroupDto.Id);
            }
            return response;

        }


        public UserGroupResponse DeleteUserGroup(string Id)
        {
            UserGroupResponse response = new UserGroupResponse();
            var userGroupId = Guid.Parse(Id);
            var userGroup = _userGroupRepository.GetWhere(m => m.Id == userGroupId).FirstOrDefault();

            if (userGroup != null)
            {
                _userGroupRepository.Delete(userGroup);
                _userGroupRepository.Save();
                ErrorObject obj = new ErrorObject { Message = "UserGroup Deleted Successfully.", Status = "Success" };
                response.UserGroupDetails = JsonConvert.SerializeObject(obj);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.UserGroupDetails = JsonConvert.SerializeObject(obj);
            }
            return response;
        }


        public UserGroupResponse UpdateUserGroup(UserGroupDto userGroupDto)
        {
            UserGroupResponse response = new UserGroupResponse();

            //var userGroupId = Guid.Parse(Id);
            //var userGroup = _userGroupRepository.GetWhere(m => m.Id == userGroupId).FirstOrDefault();

            var userGroup = Mapper.Map<UserGroupDto, UserGroup>(userGroupDto);
            userGroup.ModifiedAt = System.DateTime.Now;
            var result = _userGroupRepository.Update(userGroup);
            _userGroupRepository.Save();

            if (result == null)
            {
                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.UserGroupDetails = JsonConvert.SerializeObject(obj);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "UserGroup updated Successfully", Status = "Success" };
                response.UserGroupDetails = JsonConvert.SerializeObject(obj);
            }

            return response;
        }
    }
}
