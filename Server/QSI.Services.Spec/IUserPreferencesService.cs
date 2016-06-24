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
    public interface IUserPreferencesService
    {
        [OperationContract]
        //[WebGet(UriTemplate = "/GetUserPreferences/{UserId}", ResponseFormat = WebMessageFormat.Json)]
        [WebInvoke(UriTemplate = "/GetUserPreference",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        UserPreferencesResponse GetUserPreferenceById(string UserId);

        [OperationContract]
        //[WebGet(UriTemplate = "/SaveUserPreferences", ResponseFormat = WebMessageFormat.Json)]
        //UserPreferencesResponse SaveUserPreferences();
        [WebInvoke(UriTemplate = "/SaveUserPreference",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        UserPreferencesResponse SaveUserPreferences(UserPreferenceDto userPreferenceDto);

        [OperationContract]
        //[WebGet(UriTemplate = "/UpdateUserPreferences/{Id}", ResponseFormat = WebMessageFormat.Json)]
        [WebInvoke(UriTemplate = "/UpdateUserPreference",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        UserPreferencesResponse UpdateUserPreferences(UserPreferenceDto userPreferenceDto);


        [OperationContract]
        //[WebGet(UriTemplate = "/DeleteUserPreferences/{Id}", ResponseFormat = WebMessageFormat.Json)]
        [WebInvoke(UriTemplate = "/DeleteUserPreference",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        UserPreferencesResponse DeleteUserPreferences(string UserId);

    }
}
