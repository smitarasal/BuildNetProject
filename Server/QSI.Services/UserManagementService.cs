using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QSI.Data;
using QSI.Data.Spec;
using QSI.Domain;
using QSI.Domain.Spec;
using QSI.Services.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QSI.Services
{
    [ServiceBehavior]
    public class UserManagementService : IUserManagementService
    {
        IUserManagementRepository _userManagementRepository;
        IUserPreferencesRepository _userPreferenceRepository;
        IUserDataRepository  _userDataRepository;
        IUserLoggingRepository _userLoggingRepository;
        IWorkOrderRepository _workOrderRepository;
        IClientRepository _clientRepository;
        IUserGroupRepository _userGroupRepository;
        ISharingRepository _sharingRepository;


        public UserManagementService()
        {
            if (_userManagementRepository == null)
                _userManagementRepository = new UserManagementRepository();

            if (_userPreferenceRepository == null)
                _userPreferenceRepository = new UserPreferencesRepository();

            if(_userDataRepository == null)
                _userDataRepository= new UserDataRepository();

            if (_userLoggingRepository == null)
                _userLoggingRepository = new UserLoggingRepository();

            if (_workOrderRepository == null)
                _workOrderRepository = new WorkOrderRepository();

            if (_clientRepository == null)
                _clientRepository = new ClientRepository();

            if (_userGroupRepository == null)
                _userGroupRepository = new UserGroupRepository();

            if (_sharingRepository == null)
                _sharingRepository = new SharingRepository();
        }


        public UserManagementService(IUserManagementRepository userManagementRepository, 
            IUserPreferencesRepository userPreferenceRepository, 
            IUserDataRepository userDataRepository, 
            IUserLoggingRepository userLoggingRepository,
            IWorkOrderRepository workOrderRepository,
            IClientRepository clientRepository,
            IUserGroupRepository userGroupRepository,
            ISharingRepository sharingRepository)
        {
            _userManagementRepository = userManagementRepository;
            _userPreferenceRepository = userPreferenceRepository;
            _userDataRepository = userDataRepository;
            _userLoggingRepository = userLoggingRepository;
            _workOrderRepository = workOrderRepository;
            _clientRepository = clientRepository;
            _userGroupRepository = userGroupRepository;
            _sharingRepository = sharingRepository;
        }

        public UserResponse GetAuthorizedUsers(string EmailAddress, string Password, string Latitude, string Longitude, string UserId)
        {
            UserResponse response = new UserResponse();
            try{

                User validUser = new User();
                if (string.IsNullOrEmpty(UserId))
                {
                    validUser = _userManagementRepository.GetWhere(m => m.EmailAddress == EmailAddress && m.Password == Password).FirstOrDefault();
                }
                else
                {
                    Guid userId = Guid.Parse(UserId);
                    validUser = _userManagementRepository.GetWhere(m => m.Id == userId).FirstOrDefault();
                }

           if (validUser != null)
            {
               if(validUser.ExpirationDate==null)
               {
                   if (!validUser.IsActive || validUser.EffectiveDate > DateTime.Now)
                   {
                       ErrorObject obj = new ErrorObject { Message = "Invalid User.", Status = "Failed" };
                       response.UserDetails = JsonConvert.SerializeObject(obj);
                       return response;
                   }
               }
               else if (!validUser.IsActive || validUser.EffectiveDate > DateTime.Now || validUser.ExpirationDate < DateTime.Now)
                {
                    ErrorObject obj = new ErrorObject { Message = "Invalid User.", Status = "Failed" };
                    response.UserDetails = JsonConvert.SerializeObject(obj);
                    return response;
                }

            
                //Assign unique Token to each User
                validUser.Token = Guid.NewGuid();
                _userManagementRepository.Update(validUser);
                _userManagementRepository.Save();
                
                UsersDto usersDto = Mapper.Map<User, UsersDto>(validUser);

                //Save User Logging Details
                UserLogging userLoggingDetails = new UserLogging();
                userLoggingDetails.Id = Guid.NewGuid();
                userLoggingDetails.UserId = new Guid(usersDto.Id.ToString());
                userLoggingDetails.LoginDateTime = System.DateTime.Now;
                userLoggingDetails.LogoutDateTime = System.DateTime.Now;
                userLoggingDetails.IsLoggedIn = true;
                userLoggingDetails.Lattitude = Latitude;
                userLoggingDetails.Longitude = Longitude;

                //Create unique Token in UserLogging. As there can be multiple records against a single User in
                //UserLogging table, we need an uniueidentifier to update the LogoutDateTime with which the User has LoggedIn.
                userLoggingDetails.Token = validUser.Token;

                var userLogging = _userLoggingRepository.Insert(userLoggingDetails);
                _userLoggingRepository.Save();


                //Assign UserPrefeerences in UsersDto
                var userPreferenceLst = _userPreferenceRepository.GetWhere(m => m.UserId == usersDto.Id);
                var userPreference = userPreferenceLst.ToList();
                if (userPreference.Count() > 0)
                {
                    List<UserPreferenceDto> userPreferenceDto = Mapper.Map<List<UserPreference>, List<UserPreferenceDto>>(userPreference);
                    usersDto.UserPreference = userPreferenceDto;
                }

                //Assign UserData in UsersDto
                var userDataLst = _userDataRepository.GetWhere(m=>m.UserId==usersDto.Id);
                var userData = userDataLst.ToList();
                if (userDataLst != null && userData.Count() > 0)
                {
                    List<UserDataDto> userDataDto = Mapper.Map<List<UserData>, List<UserDataDto>>(userData.ToList());
                    usersDto.UserData = userDataDto;
                }

                List<string> LstKeys = new List<string>();


                usersDto.ShareWidgets = new Dictionary<string, List<string>>();
                //Assign ShareWidgets to UsersDto
                List<string> values = new List<string>();
                if (validUser.ShareWidgets1.Count > 0)
                {
                    foreach (var item in validUser.ShareWidgets1)
                    {

                        if (!LstKeys.Contains(item.Keys))
                        {
                            LstKeys.Add(item.Keys);
                        }
                    }
                    foreach (var item in LstKeys)
                    {
                        var value = validUser.ShareWidgets1.Where(m => m.Keys == item).ToList();

                        foreach (var val in value)
                        {
                            values.Add(val.Value);
                        }
                        usersDto.ShareWidgets.Add(item, values);
                        values = new List<string>();
                    }

                }

                response.UserDetails = JsonConvert.SerializeObject(usersDto);
             
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Invalid User Name Password.", Status = "Failed" };
                response.UserDetails = JsonConvert.SerializeObject(obj);


            }
        }
                catch (Exception ex)
            {
                ErrorObject obj = new ErrorObject { Message = ex.InnerException.ToString(), Status = "Failed" };
                response.UserDetails = JsonConvert.SerializeObject(obj);
            }
            return response;

        }

        public UserResponse GetUserDetails(string UserId)
        {
            UserResponse response = new UserResponse();

            Guid userId = Guid.Parse(UserId);
            var validUser = _userManagementRepository.GetWhere(m => m.Id == userId).FirstOrDefault();

            if (validUser != null)
            {
                response = GetAuthorizedUsers(validUser.EmailAddress, validUser.Password, null, null, null);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.UserDetails = JsonConvert.SerializeObject(obj);
            }

            return response;
        }


        public UserResponse GetValidUser(string UserId)
        {
            UserResponse response = new UserResponse();

            Guid userId = Guid.Parse(UserId);
            var validUser = _userManagementRepository.GetWhere(m => m.Id == userId).FirstOrDefault();

            if (validUser != null)
            {
                validUser.Token = Guid.NewGuid();
                _userManagementRepository.Update(validUser);
                _userManagementRepository.Save();

                UsersDto usersDto = Mapper.Map<User, UsersDto>(validUser);
                usersDto.UserPreference = null;
                usersDto.UserData = null;
                usersDto.WorkOrders = null;
                usersDto.UserGroup = null;
                usersDto.Client = null;
                response.UserDetails = JsonConvert.SerializeObject(usersDto);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                response.UserDetails = JsonConvert.SerializeObject(obj);
            }

            return response;
        }


        public UserResponse GetAllUsers()
        {

            List<UsersDto> LstUserDto = new List<UsersDto>();
            UserResponse response = new UserResponse();
            var usersLst = _userManagementRepository.GetAll();
            var users = usersLst.ToList();

            if (users.Count() > 0)
            {
                foreach (var item in users)
                {
                    UsersDto usersDto = Mapper.Map<User, UsersDto>(item);
                    usersDto.UserPreference = null;
                    usersDto.UserData = null;
                    usersDto.WorkOrders = null;
                    usersDto.UserGroup = null;
                    usersDto.Client = null;
                    LstUserDto.Add(usersDto);
                }

                response.UserDetails = JsonConvert.SerializeObject(LstUserDto);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "No User data found.", Status = "Failed" };
                response.UserDetails = JsonConvert.SerializeObject(obj);
            }
            return response;
        }


        public UserResponse SaveUser(UsersDto userDto)
        {
            JObject content = JObject.Parse(userDto.EffectiveFrom);
            string effectiveFromDate = content.GetValue("Date").ToString();
            string effectiveFromMonth = content.GetValue("Month").ToString();
            string effectiveFromYear = content.GetValue("Year").ToString();

            userDto.EffectiveDate = new DateTime(Convert.ToInt32(effectiveFromYear), Convert.ToInt32(effectiveFromMonth), Convert.ToInt32(effectiveFromDate));

            if (string.IsNullOrEmpty(userDto.EffectiveTo))
            {
                userDto.ExpirationDate = null;
            }
            else
            {
                content = JObject.Parse(userDto.EffectiveTo);
                string effectiveToDate = content.GetValue("Date").ToString();
                string effectiveToMonth = content.GetValue("Month").ToString();
                string effectiveToYear = content.GetValue("Year").ToString();
                userDto.ExpirationDate = new DateTime(Convert.ToInt32(effectiveToYear), Convert.ToInt32(effectiveToMonth), Convert.ToInt32(effectiveToDate));
            }
       //  userDto.EffectiveDate = Convert.ToDateTime(strDate);

            UserResponse response = new UserResponse();

            try
            {
                if(userDto.clientId==null)
                {
                userDto.clientId=    _userGroupRepository.GetWhere(m => m.Id == userDto.UserGroupId).FirstOrDefault().ClientId;

                }
                userDto.Id = Guid.NewGuid();
                userDto.CreatedAt = System.DateTime.Now;
                userDto.ModifiedAt = System.DateTime.Now;

                User newUser = Mapper.Map<UsersDto, User>(userDto);
                //newUser.Password = PasswordHash.HashPassword(newUser.Password);
                var user = _userManagementRepository.Insert(newUser);
                _userManagementRepository.Save();
                if (user == null)
                {
                    ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                    response.UserDetails = JsonConvert.SerializeObject(obj);
                }
                else
                {

                    ErrorObject obj = new ErrorObject { Message = "Information Saved Successfully", Status = "Success" };
                    response.UserDetails = JsonConvert.SerializeObject(userDto.Id);
                }
            }
            catch (Exception ex)
            {
                ErrorObject obj = new ErrorObject { Message = ex.Message, Status = "Failed" };
                response.UserDetails = JsonConvert.SerializeObject(obj);
            }
            return response;
        }


           public UserResponse DeleteUser(string UserId)
           {
               UserResponse response = new UserResponse();
               try
               {
                   var userId = Guid.Parse(UserId);
                   var userLst = _userManagementRepository.GetWhere(m => m.Id == userId);
                   var user = userLst.FirstOrDefault();

                   if (user != null)
                   {
                       user.IsActive = false;
                       _userManagementRepository.Update(user);
                       _userManagementRepository.Save();
                       ErrorObject obj = new ErrorObject { Message = "User Deleted Successfully.", Status = "Success" };
                       response.UserDetails = JsonConvert.SerializeObject(obj);
                   }
                   else
                   {
                       ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                       response.UserDetails = JsonConvert.SerializeObject(obj);
                   }

               }
               catch (Exception ex)
               {
                   
                  ErrorObject obj = new ErrorObject { Message = ex.Message, Status = "Failed" };
                   response.UserDetails = JsonConvert.SerializeObject(obj);
               }
               return response;
           }

           public UserResponse UpdateUser(UsersDto userDto)
           {
               UserResponse response = new UserResponse();

               try
               {
               
                   //var user = _userManagementRepository.GetWhere(m => m.Id == userId).FirstOrDefault();
                   //user.Password = PasswordHash.HashPassword(user.Password);
                   if (userDto.clientId == null)
                   {
                       userDto.clientId = _userGroupRepository.GetWhere(m => m.Id == userDto.UserGroupId).FirstOrDefault().ClientId;
                   }
                   var user = Mapper.Map<UsersDto, User>(userDto);
                   user.ModifiedAt = System.DateTime.Now;
                   var result = _userManagementRepository.Update(user);
                   _userManagementRepository.Save();

                   if (result == null)
                   {
                       ErrorObject obj = new ErrorObject { Message = "Error in processing request.", Status = "Failed" };
                       response.UserDetails = JsonConvert.SerializeObject(obj);
                   }
                   else
                   {
                       ErrorObject obj = new ErrorObject { Message = "User updated Successfully.", Status = "Success" };
                       response.UserDetails = JsonConvert.SerializeObject(obj);
                   }
               }
               catch(Exception ex)
               {
                   ErrorObject obj = new ErrorObject { Message = ex.Message, Status = "Failed" };
                   response.UserDetails = JsonConvert.SerializeObject(obj);
               }
               return response;
           }


           public UserResponse GetValidMobileUser(string EmailAddress, string Password, string Latitude, string Longitude)
           {
               var validMobileUserLst = _userManagementRepository.GetWhere(m => m.EmailAddress == EmailAddress && m.Password == Password);
               var validMobileUser = validMobileUserLst.FirstOrDefault();

               //var validMobileUserLst = _userManagementRepository.GetWhere(m => m.EmailAddress == EmailAddress && true);
               //var validMobileUser = validMobileUserLst.FirstOrDefault();
               //var IsPasswordValid = PasswordHash.ValidatePassword(Password, validMobileUser.Password);


               UserResponse response = new UserResponse();
               string CompleteJson = string.Empty;
               if (validMobileUser != null)
               {
                   if (validMobileUser.ExpirationDate == null)
                   {
                        if (!validMobileUser.IsActive || validMobileUser.EffectiveDate > DateTime.Now)
                        {
                            ErrorObject obj = new ErrorObject { Message = "Invalid User.", Status = "Failed" };
                            response.UserDetails = JsonConvert.SerializeObject(obj);
                            return response;
                        }
                   }
                   else if (!validMobileUser.IsActive || validMobileUser.EffectiveDate > DateTime.Now || validMobileUser.ExpirationDate < DateTime.Now)
                   {
                       ErrorObject obj = new ErrorObject { Message = "Invalid User.", Status = "Failed" };
                       response.UserDetails = JsonConvert.SerializeObject(obj);
                       return response;
                   }

                    validMobileUser.Token = Guid.NewGuid();
                   _userManagementRepository.Update(validMobileUser);
                   _userManagementRepository.Save();
                   UsersDto usersDto = Mapper.Map<User, UsersDto>(validMobileUser);

                   //Save WorkOrder in UsersDto
                   var workOrderLst = _workOrderRepository.GetWhere(m => m.UserId == usersDto.Id);
                   var workOrder = workOrderLst.ToList();
                   if (workOrder.Count() > 0)
                   {
                       List<WorkOrderDto> workOrderDto = Mapper.Map<List<WorkOrder>, List<WorkOrderDto>>(workOrder);
                       usersDto.WorkOrders = workOrderDto;
                   }


                   response.UserDetails = JsonConvert.SerializeObject(usersDto);

               }
               else
               {
                   ErrorObject obj = new ErrorObject { Message = "Invalid User Name Password.", Status = "Failed" };
                   response.UserDetails = JsonConvert.SerializeObject(obj);


               }
               return response;
           }



           public UserResponse LogOut(string UserId, string Token)
        {
            UserResponse response = new UserResponse();
            var userId = Guid.Parse(UserId);
            var token = Guid.Parse(Token);

            var user = _userManagementRepository.GetWhere(m => m.Id == userId).FirstOrDefault();
            if (user != null)
            {
                user.Token = Guid.Empty;
                _userManagementRepository.Update(user);
                _userManagementRepository.Save();
            }

            var userLogging = _userLoggingRepository.GetWhere(m => m.UserId == userId && m.Token == token).FirstOrDefault();
            if (userLogging != null)
            {
                userLogging.LogoutDateTime = System.DateTime.Now;
                userLogging.IsLoggedIn = false;
                userLogging.Token = Guid.Empty;
                _userLoggingRepository.Update(userLogging);
                _userLoggingRepository.Save();
            }

            ErrorObject obj = new ErrorObject { Message = "User Token deallocated Succesfully", Status = "Success" };
            response.UserDetails = JsonConvert.SerializeObject(obj);
            return response;
        }


           public ClientResponse GetClientsByUserId(string UserId)
           {
               ClientResponse response = new ClientResponse();
               List<Client> validClientsList = new List<Client>();

               List<ClientDetails> clientDetailsList = new List<ClientDetails>();

               var userId = Guid.Parse(UserId);
               var validUserLst = _userManagementRepository.GetWhere(m => m.Id == userId);
               var validUser = validUserLst.FirstOrDefault();
               if (validUser != null)
               {
                   Guid userGroupId = validUser.UsergroupId;
                   var userTypeLst = _userGroupRepository.GetWhere(m => m.Id == userGroupId);
                   var userType = userTypeLst.FirstOrDefault().UserType;
                   if (userType == "Super Admin")
                       validClientsList = _clientRepository.GetAll().ToList();
                   else
                       validClientsList = _clientRepository.GetWhere(m => m.Id == validUser.clientId).ToList();

                   validClientsList = validClientsList.OrderBy(m => m.Name).ToList();

                   if (validClientsList != null && validClientsList.Count > 0)
                   {
                       foreach (var validClient in validClientsList)
                       {
                           ClientDetails client = new ClientDetails();
                           client.ClientName = validClient.Name;
                           client.ClientId = validClient.Id;
                           clientDetailsList.Add(client);
                       }

                       response.UserId = UserId;
                       response.ClientDetails = JsonConvert.SerializeObject(clientDetailsList);
                   }
                   else
                   {
                       ErrorObject obj = new ErrorObject { Message = "No client data found.", Status = "Failed" };
                       response.ClientDetails = JsonConvert.SerializeObject(obj);
                   }
               }
               else
               {
                   ErrorObject obj = new ErrorObject { Message = "Invalid UserId.", Status = "Failed" };
                   response.ClientDetails = JsonConvert.SerializeObject(obj);
               }

               return response;
           }


        public UserGroupResponse GetUserGroupsByClientId(string ClientId)
        {
            UserGroupResponse response = new UserGroupResponse();

            List<UserGroupDetails> userGroupDetailsList = new List<UserGroupDetails>();

            var clientId = Guid.Parse(ClientId);
            var validUserGroupsList = _userGroupRepository.GetWhere(m => m.ClientId == clientId).ToList();
            validUserGroupsList = validUserGroupsList.OrderBy(m => m.Name).ToList();
            
            if (validUserGroupsList != null && validUserGroupsList.Count > 0)
            {
                foreach(var validUserGroup in validUserGroupsList)
                {
                    UserGroupDetails userGroup = new UserGroupDetails();
                    userGroup.UserGroupName = validUserGroup.Name;
                    userGroup.UserGroupId = validUserGroup.Id;
                    userGroupDetailsList.Add(userGroup);
                }

                response.ClientId = ClientId;
                response.UserGroupDetails = JsonConvert.SerializeObject(userGroupDetailsList);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Invalid ClientId.", Status = "Failed" };
                response.UserGroupDetails = JsonConvert.SerializeObject(obj);
            }

            return response;
        }


        public UserResponse GetUsersByUserGroupId(string UserGroupId)
        {
            UserResponse response = new UserResponse();

            List<UserDetails> userDetailsList = new List<UserDetails>();

            var userGroupId = Guid.Parse(UserGroupId);
            var validUsersList = _userManagementRepository.GetWhere(m => m.UsergroupId == userGroupId).ToList();
            validUsersList = validUsersList.OrderBy(m => m.Name).ToList();

            if (validUsersList != null && validUsersList.Count > 0)
            {
                foreach(var validUser in validUsersList)
                {
                    UserDetails user = new UserDetails();
                    user.UserName = validUser.Name;
                    user.UserId = validUser.Id;
                    userDetailsList.Add(user);
                }

                response.UserGroupId = UserGroupId;
                response.UserDetails = JsonConvert.SerializeObject(userDetailsList);
            }
            else
            {
                ErrorObject obj = new ErrorObject { Message = "Invalid UserGroupId.", Status = "Failed" };
                response.UserDetails = JsonConvert.SerializeObject(obj);
            }

            return response;
        }

     
    }
}
