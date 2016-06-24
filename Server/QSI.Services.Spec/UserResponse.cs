using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
    [DataContract]
    public class UserResponse
    {
        [DataMember]
        public string UserGroupId { get; set; }
        [DataMember]
        public string UserDetails { get; set; }
    }
}
