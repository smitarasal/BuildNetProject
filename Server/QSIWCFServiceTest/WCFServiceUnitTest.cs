using System;
using NUnit;
using NUnit.Framework;
using Rhino.Mocks;
using QSI.Domain;
using QSI.Services;
using QSI.Data.Spec;
using QSI.Data;
using QSI.Services.Spec;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace QSIWCFServiceTest
{
    [TestFixture]
    public class WCFServiceUnitTest
    {
        MockRepository _mockRepository = new MockRepository();

        IUserManagementRepository _userRepository;
        UserManagementService _userMangementService;

        IPasswordHash _passwordHash;
        IUserDataRepository _userDataRepository;
        UserDataService _userDataService;
        IUserLoggingRepository _userLoggingRepository;
        UserLoggingService _userLoggingService;


        IClientRepository _clientRepository;
        ClientService _clientService;

        IUserGroupRepository _userGroupRepository;
        UserGroupService _userGroupService;

        IUserPreferencesRepository _userPreferenceRepository;
        UserPreferencesService _userPreferenceService;

        IWorkOrderRepository _workOrderRepository;
        WorkOrderService _workOrderService;

        IClientServersRepository _clientServersRepository;

        PDFGenerator _pdfGenerator;
        DocumentService documentService;

        ISharingRepository _sharingRepository;

        [SetUp]
        public void SetUp()
        {
            AutoMapBootStrap.InitializeMap();

            //UserManagementService SetUp
            _userRepository = _mockRepository.StrictMock<IUserManagementRepository>();
            _userPreferenceRepository = _mockRepository.StrictMock<IUserPreferencesRepository>();
            _userDataRepository = _mockRepository.StrictMock<IUserDataRepository>();
            _userLoggingRepository = _mockRepository.StrictMock<IUserLoggingRepository>();
            _workOrderRepository = _mockRepository.StrictMock<IWorkOrderRepository>();
           _clientRepository = _mockRepository.StrictMock<IClientRepository>();
           _userGroupRepository = _mockRepository.StrictMock<IUserGroupRepository>();
           _clientServersRepository = _mockRepository.StrictMock<IClientServersRepository>();
           _sharingRepository = _mockRepository.StrictMock<ISharingRepository>();

           _userMangementService = new UserManagementService(_userRepository, _userPreferenceRepository, _userDataRepository, _userLoggingRepository, _workOrderRepository, _clientRepository, _userGroupRepository, _sharingRepository);
            _passwordHash = _mockRepository.StrictMock<IPasswordHash>();
            InjectValidatePassword(_passwordHash);
            InjectHashPassword(_passwordHash);

            getUserInstance();
            getUsersDtoInstance();
            getUserLoggingInstance();
            getUserDataInstance();

            //ClientService SetUp
           // _clientRepository = _mockRepository.StrictMock<IClientRepository>();
            _clientService = new ClientService(_clientRepository, _clientServersRepository);

            getClientInstance();
            getClientDtoInstance();

            //UserGroupService SetUp
           // _userGroupRepository = _mockRepository.StrictMock<IUserGroupRepository>();
            _userGroupService = new UserGroupService(_userGroupRepository);

            getUserGroupInstance();
            getUserGroupDtoInstance();


            //UserPreferenceService SetUp
            _userPreferenceService = new UserPreferencesService(_userPreferenceRepository);

            getUserPreferenceInstance();
            getUserPreferenceDtoInstance();


            //UserDataService SetUp
            _userDataService = new UserDataService(_userDataRepository);
            getUserDataDtoInstance();

            //UserLoggingService SetUp
            _userLoggingService = new UserLoggingService(_userLoggingRepository);

            //WorkOrderService SetUp
            _workOrderService = new WorkOrderService(_workOrderRepository);
            getWorkOrderInstance();
            getWorkOrderDtoInstance();

            //Generate PDF SetUp
            _pdfGenerator = _mockRepository.PartialMock<PDFGenerator>();
            documentService = new DocumentService(_pdfGenerator);
        }

        #region UserManagementService Unit Tests

        string emailAddress = "asd";
        string password = "sdf";
        string userId = "1627AEA5-8E0A-4371-9022-9B504344E724";
        Guid superAdminUserId = Guid.Parse("A4A60EB3-CCB0-47C8-B891-42731EE4186A");
        string token = "425F5842-C6A1-4F41-931F-1F5CA06FD8B7";
       
        IEnumerable<User> lstUsers = new List<User>();
        List<User> lstUserItems = new List<User>();
        User usr = new User();
        User logoutUser = new User();
        UsersDto userDto = new UsersDto();


        IEnumerable<UserLogging> lstUserLogging = new List<UserLogging>();
        List<UserLogging> lstUserLoggingItems = new List<UserLogging>();
        UserLogging userLoggingDetails = new UserLogging();

        IEnumerable<UserData> lstUserData = new List<UserData>();
        List<UserData> lstUserDataItems = new List<UserData>();
        UserData userData = new UserData();

        public void getUserInstance()
        {
            usr.Id = new Guid("1627AEA5-8E0A-4371-9022-9B504344E724");
            usr.Name = "kunal";
            usr.AddressLine1 = "USA";
            usr.AddressLine2 = "New York";
            usr.ContactNumber = "1234567890";
            usr.EmailAddress = "k@k.com";
            usr.Password = "asd";
            usr.UsergroupId = superAdminUserGroupId;
            usr.City = "New York";
            usr.Country = "USA";
            usr.EffectiveDate = System.DateTime.Now;
            usr.ExpirationDate = System.DateTime.Now;
            usr.IsActive = true;
            usr.Token = Guid.NewGuid();
            usr.CreatedAt = System.DateTime.Now;
            usr.ModifiedAt = System.DateTime.Now;
            usr.CreatedBy = "kunalk";
            usr.ModifiedBy = "Kunalk";
            usr.Client = new Client
            {
                Id = new Guid("8235687E-6961-4E3D-B306-4A41C72CC15C"),
                Name = "LKGEU",
                AddressLine1 = "US",
                AddressLine2 = "US",
                PhoneNumber = "11111",
                EmailAddress = "Lkgeu@Lkgeu.com",
                CreatedBy = "Kunalk",
                ModifiedBy = "Kunalk",
                CreatedAt = System.DateTime.Now,
                ModifiedAt = System.DateTime.Now,
            };
            usr.clientId = new Guid("8235687E-6961-4E3D-B306-4A41C72CC15C");
            usr.UserGroup = new UserGroup
            {
                Id = new Guid("1627aea5-8e0a-4371-9022-9b504344e724"),
                ClientId = new Guid("8235687E-6961-4E3D-B306-4A41C72CC15C"),
                MapPermissions = "TBD",
                ModulePermissions = "TBD",
                Name = "Admin",
                UserType = "Admin",
                CreatedBy = "Tom",
                ModifiedBy = "Tom",
                CreatedAt = System.DateTime.Now,
                ModifiedAt = System.DateTime.Now,
            };
            lstUserItems.Add(usr);
            lstUsers = lstUserItems;


            logoutUser.Id = usr.Id;
            logoutUser.Token = new Guid();
        }

        public void getUsersDtoInstance()
        {
            userDto.Id = usr.Id;
            userDto.Name = usr.Name;
        }

        public void getUserLoggingInstance()
        {
            userLoggingDetails.Id = Guid.NewGuid();
            userLoggingDetails.UserId = new Guid("1627AEA5-8E0A-4371-9022-9B504344E724");
            userLoggingDetails.LoginDateTime = System.DateTime.Now;
            userLoggingDetails.LogoutDateTime = System.DateTime.Now;
            userLoggingDetails.IsLoggedIn = true;
            userLoggingDetails.Lattitude = "40.0000";
            userLoggingDetails.Longitude = "100.0000";            
            userLoggingDetails.Token = Guid.NewGuid();

            lstUserLoggingItems.Add(userLoggingDetails);
            lstUserLogging = lstUserLoggingItems;
        }

        public void getUserDataInstance()
        {
            userData.Id = Guid.NewGuid();
            userData.UserId = new Guid("1627AEA5-8E0A-4371-9022-9B504344E724");
            userData.Keys = "Layers";
            userData.Value = "Layer Data";
            userData.CreatedBy = "Kunalk";
            userData.CreatedOn = System.DateTime.Now;
            userData.ModifiedBy = "Kunalk";
            userData.ModifiedOn = System.DateTime.Now;

            lstUserDataItems.Add(userData);
            lstUserData = lstUserDataItems;
        }

        private void InjectValidatePassword(IPasswordHash passwordHash)
        {
            typeof(PasswordHash)
               .GetProperty("PasswordValidation", BindingFlags.NonPublic | BindingFlags.Static)
               .SetValue(null, passwordHash, null);
        }

        private void InjectHashPassword(IPasswordHash passwordHash)
        {
            typeof(PasswordHash)
               .GetProperty("EncryptPassword", BindingFlags.NonPublic | BindingFlags.Static)
               .SetValue(null, passwordHash, null);
        }

        [Test]
        public void GetAuthorizedUsers()
        {
            var usrId = Guid.Parse(userId);

            _mockRepository.Record();
            Expect.Call(_userRepository.GetWhere(m => m.EmailAddress == emailAddress && m.Password == password)).Return(lstUsers).IgnoreArguments().Repeat.Once();
            //Expect.Call(_passwordHash.ValidatePassword(password, password)).Return(true).IgnoreArguments();
            Expect.Call(_userRepository.Update(usr)).Return(usr).IgnoreArguments().Repeat.Once();
            Expect.Call(_userRepository.Save).IgnoreArguments().Repeat.Once();

            Expect.Call(_userLoggingRepository.Insert(userLoggingDetails)).Return(userLoggingDetails).IgnoreArguments().Repeat.Once();
            Expect.Call(_userLoggingRepository.Save).IgnoreArguments().Repeat.Once();

            Expect.Call(_userPreferenceRepository.GetWhere(m => m.UserId == userDto.Id)).Return(lstUserPreference).IgnoreArguments().Repeat.Once();

            Expect.Call(_userDataRepository.GetWhere(m => m.UserId == userDto.Id)).IgnoreArguments().Repeat.Once().Return(lstUserData);

            _mockRepository.ReplayAll();
            _userMangementService.GetAuthorizedUsers("asd", "asd","40","40",null);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void GetValidUser()
        {
            var usrId = Guid.Parse(userId);

            _mockRepository.Record();
            Expect.Call(_userRepository.GetWhere(m => m.Id == usrId)).Return(lstUsers).IgnoreArguments().Repeat.Once();
            Expect.Call(_userRepository.Update(usr)).Return(usr).IgnoreArguments().Repeat.Once();
            Expect.Call(_userRepository.Save).IgnoreArguments().Repeat.Once();

            _mockRepository.ReplayAll();
            _userMangementService.GetValidUser(userId);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void GetAllUsers()
        {

            IQueryable<User> listUsers = null;
            listUsers = lstUsers.AsQueryable();


            _mockRepository.Record();
            Expect.Call(_userRepository.GetAll()).Return(listUsers).Repeat.Once();

            _mockRepository.ReplayAll();
            _userMangementService.GetAllUsers();
            _mockRepository.VerifyAll();
        }

        [Test]
        public void SaveUser()
        {
            _mockRepository.Record();
            //Expect.Call(_passwordHash.HashPassword(password)).IgnoreArguments().Return(encryptedPassword);
            Expect.Call(_userRepository.Insert(usr)).Return(usr).IgnoreArguments().Repeat.Once();
            Expect.Call(_userRepository.Save).IgnoreArguments().Repeat.Once();

            _mockRepository.ReplayAll();
            _userMangementService.SaveUser(userDto);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void DeleteUser()
        {
            var usrId = Guid.Parse(userId);

            _mockRepository.Record();
            Expect.Call(_userRepository.GetWhere(m => m.Id == usrId)).IgnoreArguments().Return(lstUsers).Repeat.Once();
            Expect.Call(() => _userRepository.Delete(usr)).IgnoreArguments().Repeat.Once();
            Expect.Call(_userRepository.Save).Repeat.Once();

            _mockRepository.ReplayAll();
            _userMangementService.DeleteUser(userId);
            _mockRepository.VerifyAll();
        }

        [Test]
        public void UpdateUser()
        {
            //var usrId = Guid.Parse(userId);

            _mockRepository.Record();
            //Expect.Call(_userRepository.GetWhere(m => m.Id == usrId)).IgnoreArguments().Return(lstUsers).Repeat.Any();
            Expect.Call(_userRepository.Update(usr)).Return(usr).IgnoreArguments().Repeat.Once();
            Expect.Call(_userRepository.Save).Repeat.Once();

            _mockRepository.ReplayAll();
            _userMangementService.UpdateUser(userDto);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void GetValidMobileUser()
        {
            var usrId = Guid.Parse(userId);

            _mockRepository.Record();
            Expect.Call(_userRepository.GetWhere(m => m.EmailAddress == emailAddress && m.Password == password)).IgnoreArguments().Return(lstUsers).Repeat.Once();
            //Expect.Call(_passwordHash.ValidatePassword(password, password)).Return(true).IgnoreArguments();
            Expect.Call(_userRepository.Update(usr)).Return(usr).IgnoreArguments().Repeat.Once();
            Expect.Call(_userRepository.Save).IgnoreArguments().Repeat.Once();
            Expect.Call(_workOrderRepository.GetWhere(m => m.UserId == userDto.Id)).IgnoreArguments().Return(lstWorkOrder).Repeat.Once();



            _mockRepository.ReplayAll();
            _userMangementService.GetValidMobileUser("asd", "asd","","");
            _mockRepository.VerifyAll();
        }


        [Test]
        public void LogOut()
        {
            var usrId = Guid.Parse(userId);
            var Token = Guid.Parse(token);

            _mockRepository.Record();
            Expect.Call(_userRepository.GetWhere(m => m.Id == usrId)).IgnoreArguments().Return(lstUsers).Repeat.Once();
            Expect.Call(_userRepository.Update(logoutUser)).Return(logoutUser).IgnoreArguments().Repeat.Once();
            Expect.Call(_userRepository.Save).Repeat.Once();

            Expect.Call(_userLoggingRepository.GetWhere(m => m.Id == usrId && m.Token == Token)).IgnoreArguments().Return(lstUserLogging).Repeat.Once();
            Expect.Call(_userLoggingRepository.Update(userLoggingDetails)).Return(userLoggingDetails).IgnoreArguments().Repeat.Once();
            Expect.Call(_userLoggingRepository.Save).IgnoreArguments().Repeat.Once();

            _mockRepository.ReplayAll();
            _userMangementService.LogOut(userId, token);
            _mockRepository.VerifyAll();
        }

        [Test]
        public void GetClientsByUserId_forNonSuperAdmin()
        {
            _mockRepository.Record();
            Expect.Call(_userRepository.GetWhere(m => m.Id == superAdminUserId)).IgnoreArguments().Return(lstUsers).Repeat.Once();
            Expect.Call(_userGroupRepository.GetWhere(m => m.Id == superAdminUserGroupId)).IgnoreArguments().Return(lstUserGroups).Repeat.Once();
            Expect.Call(_clientRepository.GetWhere(m => m.Id == clientGuid)).IgnoreArguments().Return(lstClients).Repeat.Once();
           
            _mockRepository.ReplayAll();
            _userMangementService.GetClientsByUserId(userId);
            _mockRepository.VerifyAll();
        }

        [Test]
        public void GetClientsByUserId_forSuperAdmin()
        {
            userGroup.UserType = "Super Admin";
            lstUserGroupItems.Add(userGroup);
            lstUserGroups = lstUserGroupItems;

            IQueryable<Client> listClients = lstClients.AsQueryable();

            _mockRepository.Record();
            Expect.Call(_userRepository.GetWhere(m => m.Id == superAdminUserId)).IgnoreArguments().Return(lstUsers).Repeat.Once();
            Expect.Call(_userGroupRepository.GetWhere(m => m.Id == superAdminUserGroupId)).IgnoreArguments().Return(lstUserGroups).Repeat.Once();
            Expect.Call(_clientRepository.GetAll()).Return(listClients).Repeat.Once();

            _mockRepository.ReplayAll();
            _userMangementService.GetClientsByUserId(superAdminUserId.ToString());
            _mockRepository.VerifyAll();
        }


        [Test]
        public void GetUserGroupsByClientId()
        {
            Guid ClientId = Guid.Parse(clientId);

            _mockRepository.Record();
            Expect.Call(_userGroupRepository.GetWhere(m => m.ClientId == ClientId)).IgnoreArguments().Return(lstUserGroups).Repeat.Once();

            _mockRepository.ReplayAll();
            _userMangementService.GetUserGroupsByClientId(clientId);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void GetUsersByUserGroupId()
        {
            Guid UserGroupId = Guid.Parse(userGroupId);

            _mockRepository.Record();
            Expect.Call(_userRepository.GetWhere(m => m.UsergroupId == UserGroupId)).IgnoreArguments().Return(lstUsers).Repeat.Once();

            _mockRepository.ReplayAll();
            _userMangementService.GetUsersByUserGroupId(userGroupId);
            _mockRepository.VerifyAll();
        }


        #endregion


        #region ClientService Unit Tests

      static  string clientId = "39B558E7-593E-43A9-A900-785D57C29C7A";
        Guid clientGuid = Guid.Parse(clientId);
        IEnumerable<Client> lstClients = new List<Client>();
        List<Client> lstClientItems = new List<Client>();
        Client client = new Client();
        ClientDto clientDto = new ClientDto();

        public void getClientInstance()
        {
            client.Id = new Guid("39B558E7-593E-43A9-A900-785D57C29C7A");
            client.Name = "LoneStar";
            client.AddressLine1 = "US";
            client.AddressLine2 = "US";
            client.PhoneNumber = "222222";
            client.EmailAddress = "Lonestar@lonestar.com";
            client.CreatedAt = System.DateTime.Now;
            client.ModifiedAt = System.DateTime.Now;
            client.CreatedBy = "Kunalk";
            client.ModifiedBy = "Kunalk";
            client.title = "XYZ Corporations ltd";
            client.subTitle = "Power by QSI";
            client.logo = "ImagePath/Imagename";
            client.geometryService = "ServiceUrl";
            lstClientItems.Add(client);
            lstClients = lstClientItems;
        }


        public void getClientDtoInstance()
        {
            clientDto.Id = client.Id;
            clientDto.Name = client.Name;
            clientDto.AddressLine1 = client.AddressLine1;
            clientDto.AddressLine2 = client.AddressLine2;
            clientDto.PhoneNumber = client.PhoneNumber;
            clientDto.EmailAddress = client.EmailAddress;
            clientDto.CreatedAt = client.CreatedAt;
            clientDto.ModifiedAt = client.ModifiedAt;
            clientDto.CreatedBy = client.CreatedBy;
            clientDto.ModifiedBy = client.ModifiedBy;
            clientDto.title = client.title;
            clientDto.subTitle = client.subTitle;
            clientDto.logo = client.logo;
            clientDto.geometryService = client.geometryService;
        }



        [Test]
        public void GetAuthorizedClients()
        {
            var Id = Guid.Parse(clientId);

            _mockRepository.Record();
            Expect.Call(_clientRepository.GetWhere(m => m.Id == Id)).Return(lstClients).IgnoreArguments().Repeat.Once();

            _mockRepository.ReplayAll();
            _clientService.GetAuthorizedClients(clientId);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void GetAllClients()
        {
            IQueryable<Client> listClients = null;
            listClients = lstClients.AsQueryable();

            _mockRepository.Record();
            Expect.Call(_clientRepository.GetAll()).Return(listClients).Repeat.Once();

            _mockRepository.ReplayAll();
            _clientService.GetAllClients();
            _mockRepository.VerifyAll();
        }


        [Test]
        public void SaveClient()
        {
            _mockRepository.Record();
            Expect.Call(_clientRepository.Insert(client)).Return(client).IgnoreArguments().Repeat.Once();
            Expect.Call(_clientRepository.Save).IgnoreArguments().Repeat.Once();

            _mockRepository.ReplayAll();
            _clientService.SaveClient(clientDto);
            _mockRepository.VerifyAll();
        }

        [Test]
        public void DeleteClient()
        {
            var Id = Guid.Parse(clientId);

            _mockRepository.Record();
            Expect.Call(_clientRepository.GetWhere(m => m.Id == Id)).Return(lstClients).IgnoreArguments().Repeat.Once();
            Expect.Call(() => _clientRepository.Delete(client)).IgnoreArguments().Repeat.Once();
            Expect.Call(_clientRepository.Save).Repeat.Once();

            _mockRepository.ReplayAll();
            _clientService.DeleteClient(clientId);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void UpdateClient()
        {
            //var Id = Guid.Parse(clientId);

            _mockRepository.Record();
            //Expect.Call(_clientRepository.GetWhere(m => m.Id == Id)).IgnoreArguments().Repeat.Any().Return(lstClients);
            Expect.Call(_clientRepository.Update(client)).Return(client).IgnoreArguments().Repeat.Once();
            Expect.Call(_clientRepository.Save).Repeat.Once();

            _mockRepository.ReplayAll();
            _clientService.UpdateClient(clientDto);
            _mockRepository.VerifyAll();
        }

        #endregion


        #region UserGroupService Unit Tests

        string userGroupId = "FF4EE69E-8E55-4918-ADDC-7B5B0726D539";
        Guid superAdminUserGroupId = Guid.Parse("8392C3FB-029E-4877-9921-CD7650CF3B9F");
        IEnumerable<UserGroup> lstUserGroups = new List<UserGroup>();
        List<UserGroup> lstUserGroupItems = new List<UserGroup>();
        UserGroup userGroup = new UserGroup();
        UserGroupDto userGroupDto = new UserGroupDto();

        public void getUserGroupInstance()
        {
            userGroup.Id = new Guid("FF4EE69E-8E55-4918-ADDC-7B5B0726D539");
            userGroup.Name = "Inspector";
            userGroup.ClientId = new Guid("39B558E7-593E-43A9-A900-785D57C29C7A");
            userGroup.ModulePermissions = "TBD";
            userGroup.MapPermissions = "TBD";
            userGroup.UserType = "Other User";
            userGroup.CreatedAt = System.DateTime.Now;
            userGroup.ModifiedAt = System.DateTime.Now;
            userGroup.CreatedBy = "Kunalk";
            userGroup.ModifiedBy = "Kunalk";

            lstUserGroupItems.Add(userGroup);
            lstUserGroups = lstUserGroupItems;
        }


        public void getUserGroupDtoInstance()
        {
            userGroupDto.Id = userGroup.Id;
            userGroupDto.Name = userGroup.Name;
            userGroupDto.ClientId = userGroup.ClientId;
            userGroupDto.ModulePermissions = userGroup.ModulePermissions;
            userGroupDto.MapPermissions = userGroup.MapPermissions;
            userGroupDto.UserType = userGroup.UserType;
            userGroupDto.CreatedAt = userGroup.CreatedAt;
            userGroupDto.ModifiedAt = userGroup.ModifiedAt;
            userGroupDto.CreatedBy = userGroup.CreatedBy;
            userGroupDto.ModifiedBy = userGroup.ModifiedBy;
        }



        [Test]
        public void GetAuthorizedUserGroups()
        {
            var Id = Guid.Parse(userGroupId);

            _mockRepository.Record();
            Expect.Call(_userGroupRepository.GetWhere(m => m.Id == Id)).Return(lstUserGroups).IgnoreArguments().Repeat.Once();

            _mockRepository.ReplayAll();
            _userGroupService.GetAuthorizedUserGroups(userGroupId);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void GetAllUserGroups()
        {
            IQueryable<UserGroup> listUserGroups = null;
            listUserGroups = lstUserGroups.AsQueryable();

            _mockRepository.Record();
            Expect.Call(_userGroupRepository.GetAll()).Return(listUserGroups).Repeat.Once();

            _mockRepository.ReplayAll();
            _userGroupService.GetAllUserGroups();
            _mockRepository.VerifyAll();
        }


        [Test]
        public void SaveUserGroup()
        {
            _mockRepository.Record();
            Expect.Call(_userGroupRepository.Insert(userGroup)).Return(userGroup).IgnoreArguments().Repeat.Once();
            Expect.Call(_userGroupRepository.Save).IgnoreArguments().Repeat.Once();

            _mockRepository.ReplayAll();
            _userGroupService.SaveUserGroup(userGroupDto);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void DeleteUserGroup()
        {
            var Id = Guid.Parse(userGroupId);

            _mockRepository.Record();
            Expect.Call(_userGroupRepository.GetWhere(m => m.Id == Id)).Return(lstUserGroups).IgnoreArguments().Repeat.Once();
            Expect.Call(() => _userGroupRepository.Delete(userGroup)).IgnoreArguments().Repeat.Once();
            Expect.Call(_userGroupRepository.Save).Repeat.Once();

            _mockRepository.ReplayAll();
            _userGroupService.DeleteUserGroup(userGroupId);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void UpdateUserGroup()
        {
            //var Id = Guid.Parse(userGroupId);

            _mockRepository.Record();
            //Expect.Call(_userGroupRepository.GetWhere(m => m.Id == Id)).IgnoreArguments().Repeat.Any().Return(lstUserGroups);
            Expect.Call(_userGroupRepository.Update(userGroup)).Return(userGroup).IgnoreArguments().Repeat.Once();
            Expect.Call(_userGroupRepository.Save).Repeat.Once();

            _mockRepository.ReplayAll();
            _userGroupService.UpdateUserGroup(userGroupDto);
            _mockRepository.VerifyAll();
        }

        #endregion


        #region UserPreferencesService Unit Tests

        IEnumerable<UserPreference> lstUserPreference = new List<UserPreference>();
        List<UserPreference> lstUserPreferenceItems = new List<UserPreference>();
        UserPreference userPreference = new UserPreference();
        UserPreferenceDto userPreferenceDto = new UserPreferenceDto();


        public void getUserPreferenceInstance()
        {
            userPreference.Id = new Guid("72F069D1-9544-45E4-933A-F2252F82ABE7");
            userPreference.UserId = new Guid("94B0C75A-3B95-41A3-9038-87A07602971A");
            userPreference.PreferencesJson = "JSON string";
            userPreference.CreatedAt = System.DateTime.Now;
            userPreference.CreatedBy = "Kunalk";
            userPreference.UpdatedAt = System.DateTime.Now;
            userPreference.UpdatedBy = "Kunalk";
            userPreference.PreferenceName = "Preference";

            lstUserPreferenceItems.Add(userPreference);
            lstUserPreference = lstUserPreferenceItems;
        }


        public void getUserPreferenceDtoInstance()
        {
            userPreferenceDto.Id = userPreference.Id;
            userPreferenceDto.UserId = userPreference.UserId;
            userPreferenceDto.PreferencesJson = userPreference.PreferencesJson;
            userPreferenceDto.CreatedAt = userPreference.CreatedAt;
            userPreferenceDto.CreatedBy = userPreference.CreatedBy;
            userPreferenceDto.UpdatedAt = userPreference.UpdatedAt;
            userPreferenceDto.UpdatedBy = userPreference.UpdatedBy;
            userPreferenceDto.PreferenceName = userPreference.PreferenceName;
        }

        [Test]
        public void GetUserPreferenceById()
        {
            var usrId = Guid.Parse(userId);

            _mockRepository.Record();
            Expect.Call(_userPreferenceRepository.GetWhere(m => m.UserId == usrId)).Return(lstUserPreference).IgnoreArguments().Repeat.Once();

            _mockRepository.ReplayAll();
            _userPreferenceService.GetUserPreferenceById(userId);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void SaveUserPreferences()
        {
            _mockRepository.Record();
            Expect.Call(_userPreferenceRepository.Insert(userPreference)).Return(userPreference).IgnoreArguments().Repeat.Once();
            Expect.Call(_userPreferenceRepository.Save).IgnoreArguments().Repeat.Once();

            _mockRepository.ReplayAll();
            _userPreferenceService.SaveUserPreferences(userPreferenceDto);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void UpdateUserPreferences()
        {
            _mockRepository.Record();
            //Expect.Call(_userPreferenceRepository.GetWhere(m => m.Id == Id)).IgnoreArguments().Repeat.Any().Return(lstUserPreference);
            Expect.Call(_userPreferenceRepository.Update(userPreference)).Return(userPreference).IgnoreArguments().Repeat.Once();
            Expect.Call(_userPreferenceRepository.Save).Repeat.Once();

            _mockRepository.ReplayAll();
            _userPreferenceService.UpdateUserPreferences(userPreferenceDto);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void DeleteUserPreferences()
        {
            var usrId = Guid.Parse(userId);

            _mockRepository.Record();
            Expect.Call(_userPreferenceRepository.GetWhere(m => m.UserId == usrId)).Return(lstUserPreference).IgnoreArguments().Repeat.Once();
            Expect.Call(() => _userPreferenceRepository.Delete(userPreference)).IgnoreArguments().Repeat.Once();
            Expect.Call(_userPreferenceRepository.Save).Repeat.Once();

            _mockRepository.ReplayAll();
            _userPreferenceService.DeleteUserPreferences(userId);
            _mockRepository.VerifyAll();
        }

        #endregion


        #region UserDataService Unit Tests

        UserDataDto userDataDto = new UserDataDto();

        public void getUserDataDtoInstance()
        {
            userDataDto.Id = userData.Id;
            userDataDto.UserId = userData.UserId;
            userDataDto.Keys = userData.Keys;
            userDataDto.Value = userData.Value;
            userDataDto.CreatedBy = userData.CreatedBy;
            userDataDto.CreatedOn = System.DateTime.Now;
            userDataDto.ModifiedBy = userData.ModifiedBy;
            userDataDto.ModifiedOn = System.DateTime.Now;
        }

        [Test]
        public void SaveUserData_KeyDoesNotExist()
        {
            var usrId = Guid.Parse(userId);

            _mockRepository.Record();
            Expect.Call(_userDataRepository.GetWhere(m => m.UserId == userDataDto.UserId && m.Keys == userDataDto.Keys)).Return(null).IgnoreArguments().Repeat.Once();
            Expect.Call(_userDataRepository.Insert(userData)).Return(userData).IgnoreArguments().Repeat.Once();
            Expect.Call(_userDataRepository.Save).IgnoreArguments();

            _mockRepository.ReplayAll();
            _userDataService.SaveUserData(userDataDto);
            _mockRepository.VerifyAll();
        }

        [Test]
        public void SaveUserData_KeyAlreadyExists()
        {
            var usrId = Guid.Parse(userId);

            _mockRepository.Record();
            Expect.Call(_userDataRepository.GetWhere(m => m.UserId == userDataDto.UserId && m.Keys == userDataDto.Keys)).Return(lstUserData).IgnoreArguments().Repeat.Once();

            _mockRepository.ReplayAll();
            _userDataService.SaveUserData(userDataDto);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void UpdateUserData()
        {
            _mockRepository.Record();
            Expect.Call(_userDataRepository.Update(userData)).Return(userData).IgnoreArguments().Repeat.Once();
            Expect.Call(_userDataRepository.Save).Repeat.Once();

            _mockRepository.ReplayAll();
            _userDataService.UpdateUserData(userDataDto);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void DeleteUserDataByKey()
        {
            _mockRepository.Record();
            Expect.Call(_userDataRepository.GetWhere(m => m.UserId == userDataDto.UserId)).Return(lstUserData).IgnoreArguments().Repeat.Once();
            Expect.Call(() => _userDataRepository.Delete(userData)).IgnoreArguments().Repeat.Once();
            Expect.Call(_userDataRepository.Save).Repeat.Once();

            _mockRepository.ReplayAll();
            _userDataService.DeleteUserDataByKey(userDataDto);
            _mockRepository.VerifyAll();
        }

        #endregion


        #region UserLoggingService Unit Tests

        [Test]
        public void GetUserLoggingDetails()
        {
            var usrId = Guid.Parse(userId);

            _mockRepository.Record();
            Expect.Call(_userLoggingRepository.GetWhere(m => m.UserId == usrId)).Return(lstUserLogging).IgnoreArguments().Repeat.Once();

            _mockRepository.ReplayAll();
            var result =_userLoggingService.GetUserLoggingDetails(userId);
            _mockRepository.VerifyAll();
            
        }

        #endregion


        #region WorkOrderService Unit Tests

        IEnumerable<WorkOrder> lstWorkOrder = new List<WorkOrder>();
        List<WorkOrder> lstWorkOrderItems = new List<WorkOrder>();
        string workOrderId = "1627AEA5-8E0A-4371-9022-9B504344E527";
        WorkOrder workOrder = new WorkOrder();
        WorkOrderDto workOrderDto = new WorkOrderDto();

        public void getWorkOrderInstance()
        {
            workOrder.Id = new Guid("1627AEA5-8E0A-4371-9022-9B504344E527");
            workOrder.WorkOrderName = "PineGrove_1101";
            workOrder.CreatedOn = DateTime.Now;
            workOrder.AssignedById = new Guid("62345FA5-C526-4059-84C6-C61F857AF9B7");
            workOrder.AssignedByName = "QSI Admin User";
            workOrder.Status = "Assigned";
            workOrder.DetectionUrl = "URL_1";
            workOrder.UserId = new Guid("94B0C75A-3B95-41A3-9038-87A07602971A");
            workOrder.UserName = "Test Inspector";

            lstWorkOrderItems.Add(workOrder);
            lstWorkOrder = lstWorkOrderItems;
        }

        public void getWorkOrderDtoInstance()
        {
            workOrderDto.Id = workOrder.Id;
            workOrderDto.WorkOrderName = workOrder.WorkOrderName;
            workOrderDto.CreatedOn = workOrder.CreatedOn;
            workOrderDto.AssignedById = workOrder.AssignedById;
            workOrderDto.AssignedByName = workOrder.AssignedByName;
            workOrderDto.Status = workOrder.Status;
            workOrderDto.DetectionUrl = workOrder.DetectionUrl;
            workOrderDto.UserId = workOrder.UserId;
            workOrderDto.UserName = workOrder.UserName;
        }

        [Test]
        public void GetWorkOrderByUserID()
        {
            var usrId = Guid.Parse(userId);

            _mockRepository.Record();
            Expect.Call(_workOrderRepository.GetWhere(m => m.UserId == usrId)).Return(lstWorkOrder).IgnoreArguments().Repeat.Once();

            _mockRepository.ReplayAll();
            _workOrderService.GetWorkOrderByUserID(userId);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void SaveWorkOrder()
        {
            _mockRepository.Record();
            Expect.Call(_workOrderRepository.Insert(workOrder)).Return(workOrder).IgnoreArguments().Repeat.Once();
            Expect.Call(_workOrderRepository.Save).IgnoreArguments();

            _mockRepository.ReplayAll();
            _workOrderService.SaveWorkOrder(workOrderDto);
            _mockRepository.VerifyAll();
        }


        [Test]
        public void UpdateWorkOrder()
        {
            _mockRepository.Record();
            Expect.Call(_workOrderRepository.Update(workOrder)).Return(workOrder).IgnoreArguments().Repeat.Once();
            Expect.Call(_workOrderRepository.Save).Repeat.Once();

            _mockRepository.ReplayAll();
            _workOrderService.UpdateWorkOrder(workOrderDto);
            _mockRepository.VerifyAll();
        }

        [Test]
        public void DeleteWorkOrder()
        {
            _mockRepository.Record();
            Expect.Call(_workOrderRepository.GetWhere(m => m.Id == workOrderDto.Id)).Return(lstWorkOrder).IgnoreArguments().Repeat.Once();
            Expect.Call(() => _workOrderRepository.Delete(workOrder)).IgnoreArguments().Repeat.Once();
            Expect.Call(_workOrderRepository.Save).Repeat.Once();

            _mockRepository.ReplayAll();
            _workOrderService.DeleteWorkOrder(workOrderId);
            _mockRepository.VerifyAll();
        }

        #endregion


        #region GeneratePDF Unit Tests

        [Test]
        public void GenerateSearchPDF()
        {
            string searchString = "JSON string";
            
            _mockRepository.Record();
            Expect.Call(_pdfGenerator.GenerateReport(searchString)).IgnoreArguments().Return(null).Repeat.Once();

            _mockRepository.ReplayAll();
            documentService.GenerateSearchPDF(searchString);
            _mockRepository.VerifyAll();
        }

        #endregion

    }
}
