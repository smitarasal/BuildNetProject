using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
    [DataContract]
    public class SharingDto
    {
        [DataMember]
        public System.Guid Id { get; set; }
        [DataMember]
        public System.Guid UserId { get; set; }
        [DataMember]
        public EmailAddressCollection EmailAddresses { get; set; }
        [DataMember]
        public string Keys { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public string ModifiedBy { get; set; }
        [DataMember]
        public System.DateTime CreatedAt { get; set; }
        [DataMember]
        public System.DateTime ModifiedAt { get; set; }
    }
}
