using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
    [DataContract]
    public class UserLoggingDto
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public System.DateTime LoginDateTime { get; set; }
        [DataMember]
        public System.DateTime LogoutDateTime { get; set; }
        [DataMember]
        public bool IsLoggedIn { get; set; }
        [DataMember]
        public Nullable<System.Guid> Token { get; set; }
        [DataMember]
        public string Lattitude { get; set; }
        [DataMember]
        public string Longitude { get; set; }

        public UsersDto User { get; set; }
    }
}
