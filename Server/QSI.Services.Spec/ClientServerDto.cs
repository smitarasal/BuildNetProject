using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
    [DataContract]
    public class ClientServerDto
    {
        [DataMember]
        public System.Guid Id { get; set; }
        [DataMember]
        public System.Guid ClientId { get; set; }
        [DataMember]
        public string Environment { get; set; }
        [DataMember]
        public string ServerAddress { get; set; }
    }
}
