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
    public interface IWorkOrderService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/GetWorkOrderByUserID",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        WorkOrderResponse GetWorkOrderByUserID(string UserId);

        [WebInvoke(UriTemplate = "/SaveWorkOrder",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        WorkOrderResponse SaveWorkOrder(WorkOrderDto workOrderDto);

        [WebInvoke(UriTemplate = "/UpdateWorkOrder",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        WorkOrderResponse UpdateWorkOrder(WorkOrderDto workOrderDto);

        [WebInvoke(UriTemplate = "/DeleteWorkOrder",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        WorkOrderResponse DeleteWorkOrder(string Id);
    }
}
