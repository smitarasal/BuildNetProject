using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
    [DataContract]
    public class ClientDto
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string AddressLine1 { get; set; }
        [DataMember]
        public string AddressLine2 { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public System.DateTime CreatedAt { get; set; }
        [DataMember]
        public System.DateTime ModifiedAt { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public string ModifiedBy { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string subTitle { get; set; }
        [DataMember]
        public string logo { get; set; }
        [DataMember]
        public string geometryService { get; set; }
        [DataMember]
        public ICollection<ClientServerDto> ClientServers { get; set; }
        [DataMember]
        public string ContactPerson { get; set; }
        [DataMember]
        public string ContactPersonPhoneNumber { get; set; }

    }
}
