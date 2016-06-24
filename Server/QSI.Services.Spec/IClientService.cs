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
    public interface IClientService
    {
        [OperationContract]
        //[WebGet(UriTemplate = "/GetClient/{Id}", ResponseFormat = WebMessageFormat.Json)]
        [WebInvoke(UriTemplate = "/GetClient",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ClientResponse GetAuthorizedClients(string Id);

        [OperationContract]
        [WebGet(UriTemplate = "/GetAllClients", ResponseFormat = WebMessageFormat.Json)]
        ClientResponse GetAllClients();

        [OperationContract]
        //[WebGet(UriTemplate = "/SaveClient", ResponseFormat = WebMessageFormat.Json)]
        //ClientResponse SaveClient();
        [WebInvoke(UriTemplate = "/SaveClient",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        ClientResponse SaveClient(ClientDto clientDto);

        [OperationContract]
        //[WebGet(UriTemplate = "/DeleteClient/{Id}", ResponseFormat = WebMessageFormat.Json)]
        [WebInvoke(UriTemplate = "/DeleteClient",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ClientResponse DeleteClient(string Id);

        [OperationContract]
        //[WebGet(UriTemplate = "/UpdateClient/{Id}", ResponseFormat = WebMessageFormat.Json)]
        [WebInvoke(UriTemplate = "/UpdateClient",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        ClientResponse UpdateClient(ClientDto clientDto);
    }
}
