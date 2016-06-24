using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
    [DataContract]
    public class AttributesPictureDto
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string TID { get; set; }
        [DataMember]
        public string Attribute { get; set; }
        [DataMember]
        public System.DateTime CreatedAt { get; set; }
        [DataMember]
        public System.DateTime ModifiedAt { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public string ModifiedBy { get; set; }

        [DataMember]
        public List<DetectionImageDto>  DetectionImages { get; set; }
    }
}
