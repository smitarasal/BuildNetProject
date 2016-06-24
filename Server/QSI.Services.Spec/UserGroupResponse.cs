using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
    [DataContract]
    public class UserGroupResponse
    {
        [DataMember]
        public string ClientId { get; set; }
        [DataMember]
        public string UserGroupDetails { get; set; }
    }
}
