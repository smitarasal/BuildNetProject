using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
    [DataContract]
    public class UserDataDto
    {

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public string Keys { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public System.DateTime CreatedOn { get; set; }
        [DataMember]
        public System.DateTime ModifiedOn { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public string ModifiedBy { get; set; }
    }
}
