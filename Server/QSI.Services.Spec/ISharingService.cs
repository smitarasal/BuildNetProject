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
    public interface ISharingService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/ShareWidgets",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        void ShareWidgets(SharingDto sharingDto);

        [WebInvoke(UriTemplate = "/DeleteWidgets",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        void  DeleteWidgets(string UserId, string Key);
    }
}
