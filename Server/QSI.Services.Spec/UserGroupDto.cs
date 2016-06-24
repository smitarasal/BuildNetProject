using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
    [DataContract]
    public class UserGroupDto
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Guid ClientId { get; set; }
         [DataMember]
        public string ModulePermissions { get; set; }
         [DataMember]
         public string MapPermissions { get; set; }
        [DataMember]
        public string UserType { get; set; }
        [DataMember]
        public System.DateTime CreatedAt { get; set; }
        [DataMember]
        public System.DateTime ModifiedAt { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public string ModifiedBy { get; set; }

        public ClientDto Client { get; set; }
        public ICollection<UsersDto> Users { get; set; }
    }
}
