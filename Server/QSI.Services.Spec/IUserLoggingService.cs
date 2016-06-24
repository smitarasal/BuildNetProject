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
    public interface IUserLoggingService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/GetUserLoggingDetails",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        UserLoggingResponse GetUserLoggingDetails(string UserId);

    }
}
