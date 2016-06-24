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
    public interface IOrderService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/GetOrderTotal/{OrderID}",
            ResponseFormat = WebMessageFormat.Json)]
        string GetOrderTotal(string OrderID);

        [OperationContract]
        [WebGet(UriTemplate = "/GetOrderDetails/{OrderID}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        OrderContract GetOrderDetails(string OrderID);

        [OperationContract]
        [WebGet(UriTemplate = "/GetAllOrderDetails",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<OrderContract> GetAllOrderDetails();


        [OperationContract]
        [WebInvoke(UriTemplate = "/PlaceOrder",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        bool PlaceOrder(OrderContract order);


        [OperationContract]
        [WebInvoke(UriTemplate = "/UpdateOrder/{OrderID}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "PUT")]
        OrderContract UpdateOrder(string OrderID);


    }
}
