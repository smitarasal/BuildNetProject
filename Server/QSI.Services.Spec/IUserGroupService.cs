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
    public interface IUserGroupService
    {
        [OperationContract]
        //[WebGet(UriTemplate = "/GetUserGroup/{Id}", ResponseFormat = WebMessageFormat.Json)]
        [WebInvoke(UriTemplate = "/GetUserGroup",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        UserGroupResponse GetAuthorizedUserGroups(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "/GetAllUserGroups", ResponseFormat = WebMessageFormat.Json)]
        UserGroupResponse GetAllUserGroups();

        [OperationContract]
        //[WebGet(UriTemplate = "/SaveUserGroup", ResponseFormat = WebMessageFormat.Json)]
        [WebInvoke(UriTemplate = "/SaveUserGroup",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        UserGroupResponse SaveUserGroup(UserGroupDto userGroupDto);

        [OperationContract]
        //[WebGet(UriTemplate = "/DeleteUserGroup/{Id}", ResponseFormat = WebMessageFormat.Json)]
        [WebInvoke(UriTemplate = "/DeleteUserGroup",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        UserGroupResponse DeleteUserGroup(string Id);

        [OperationContract]
        //[WebGet(UriTemplate = "/UpdateUserGroup/{Id}", ResponseFormat = WebMessageFormat.Json)]
        [WebInvoke(UriTemplate = "/UpdateUserGroup",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        UserGroupResponse UpdateUserGroup(UserGroupDto userGroupDto);
    }
}
