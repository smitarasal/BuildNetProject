using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
   [ServiceContract]
   public  interface IUserManagementService
    {
         [OperationContract]
         //[WebGet(UriTemplate = "/GetUser/{EmailAddress}/{Password}", ResponseFormat = WebMessageFormat.Json)]
         [WebInvoke(UriTemplate = "/GetUser",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
         UserResponse GetAuthorizedUsers(string EmailAddress, string Password,string Latitude, string Longitude, string UserId);


         [WebInvoke(UriTemplate = "/GetUserDetails",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
         UserResponse GetUserDetails(string UserId);

         [WebInvoke(UriTemplate = "/GetValidUser",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
         UserResponse GetValidUser(string UserId);

         [OperationContract]
         [WebGet(UriTemplate = "/GetAllUsers/", ResponseFormat = WebMessageFormat.Json)]
         UserResponse GetAllUsers();

         [OperationContract]
         //[WebInvoke(UriTemplate = "/SaveUser/", ResponseFormat = WebMessageFormat.Json)]
         //UserResponse SaveUser();
         [WebInvoke(UriTemplate = "/AddUser",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json, Method = "POST")]
         UserResponse SaveUser(UsersDto userDto);

         [OperationContract]
         //[WebGet(UriTemplate = "/DeleteUser/{Id}", ResponseFormat = WebMessageFormat.Json)]
         [WebInvoke(UriTemplate = "/DeleteUser",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
         UserResponse DeleteUser(string UserId);

         [OperationContract]
         //[WebGet(UriTemplate = "/UpdateUser/{Id}", ResponseFormat = WebMessageFormat.Json)]
         [WebInvoke(UriTemplate = "/UpdateUser",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
         UserResponse UpdateUser(UsersDto userDto);

         [OperationContract]
         //[WebGet(UriTemplate = "/GetValidMobileUser/{EmailAddress}/{Password}", ResponseFormat = WebMessageFormat.Json)]
         [WebInvoke(UriTemplate = "/GetValidMobileUser",
            RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
         UserResponse GetValidMobileUser(string EmailAddress, string Password,string Latitude, string Longitude);

         [OperationContract]
         //[WebGet(UriTemplate = "/LogOut/{Id}", ResponseFormat = WebMessageFormat.Json)]
         [WebInvoke(UriTemplate = "/LogOut",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
         UserResponse LogOut(string UserId, string Token);


         [WebInvoke(UriTemplate = "/GetClientsByUserId",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
         ClientResponse GetClientsByUserId(string UserId);


         [WebInvoke(UriTemplate = "/GetUserGroupsByClientId",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
         UserGroupResponse GetUserGroupsByClientId(string ClientId);


         [WebInvoke(UriTemplate = "/GetUsersByUserGroupId",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
         UserResponse GetUsersByUserGroupId(string UserGroupId);

    }
}
