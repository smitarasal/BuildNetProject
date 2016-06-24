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
   public  interface IImageService
    {
         [OperationContract]
         [WebInvoke(UriTemplate = "/SaveAttributesPicture",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json, Method = "POST")]
         void SaveAttributesPicture(AttributesPictureDto attributesPictureDto);
    }
}
