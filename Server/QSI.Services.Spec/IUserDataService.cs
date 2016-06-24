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
    public interface IUserDataService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/SaveUserData",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        UserDataResponse SaveUserData(UserDataDto userDataDto);

        [WebInvoke(UriTemplate = "/GetUserData",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        UserDataResponse GetUserData(string UserId);

        [WebInvoke(UriTemplate = "/UpdateUserData",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        UserDataResponse UpdateUserData(UserDataDto userDataDto);

        [WebInvoke(UriTemplate = "/DeleteUserDataByKey",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        UserDataResponse DeleteUserDataByKey(UserDataDto userDataDto);

  
    }
}
