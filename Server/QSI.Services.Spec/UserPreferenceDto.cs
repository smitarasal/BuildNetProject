using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
    [DataContract]
    public class UserPreferenceDto
    {
        [DataMember]
        public System.Guid Id { get; set; }
        [DataMember]
        public System.Guid UserId { get; set; }
        [DataMember]
        public string PreferencesJson { get; set; }
        [DataMember]
        public System.DateTime CreatedAt { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public System.DateTime UpdatedAt { get; set; }
        [DataMember]
        public string UpdatedBy { get; set; }
        [DataMember]
        public string PreferenceName { get; set; }

    }
}
