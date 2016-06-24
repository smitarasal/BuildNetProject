using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
    [DataContract]
    public class WorkOrderDto
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string WorkOrderName { get; set; }
        [DataMember]
        public System.DateTime CreatedOn { get; set; }
        [DataMember]
        public System.Guid AssignedById { get; set; }
        [DataMember]
        public string AssignedByName { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string DetectionUrl { get; set; }
        [DataMember]
        public System.Guid UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }

        public UsersDto User { get; set; }
        public UsersDto User1 { get; set; }
    }
}
