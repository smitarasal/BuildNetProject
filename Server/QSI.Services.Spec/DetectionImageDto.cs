using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
    [DataContract]
    public class DetectionImageDto
    {
        [DataMember]
        public System.Guid Id { get; set; }
             [DataMember]
        public Guid DetectionId { get; set; }
           [DataMember]
        public string Images { get; set; }
           [DataMember]
        public DateTime CreatedAt { get; set; }
           [DataMember]
        public DateTime ModifiedAt { get; set; }
           [DataMember]
        public string CreatedBy { get; set; }
           [DataMember]
        public string ModifiedBy { get; set; }
    
    }
}
