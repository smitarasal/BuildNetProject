using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
      [DataContract]
    public class DocumentResponse
    {
      [DataMember]
          public string DocumentData  { get; set; }
    }
}
