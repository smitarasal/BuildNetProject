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
    public interface IEmailService
    {

         [OperationContract]
         [WebInvoke(UriTemplate = "/EmailBookMark",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
         EmailReponse EmailBookMark(string bookMarkData, string ToAddress, string FromAddress);
    }
}
