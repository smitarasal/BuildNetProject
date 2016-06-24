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
    public interface IDocumentService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/GenerateSearchPDF",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        DocumentResponse GenerateSearchPDF(string searchString);

        [OperationContract]
        [WebInvoke(UriTemplate = "/GenerateSearchPDFFile",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        void GenerateSearchPDFFile(string searchString, string name, string path);
    }
}
