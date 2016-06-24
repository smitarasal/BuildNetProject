using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
    /// <summary>
    /// Serializable class
    /// </summary>
    [DataContract]
    public class UsersDto
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
        public string ContactNumber { get; set; }
        [DataMember]
        public string EmailAddress { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public Guid UserGroupId { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public System.DateTime EffectiveDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        [DataMember]
        public string EffectiveFrom { get; set; }
        [DataMember]
        public string EffectiveTo { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        [DataMember]
        public Guid Token { get; set; }
        [DataMember]
        public System.DateTime CreatedAt { get; set; }
        [DataMember]
        public System.DateTime ModifiedAt { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public string ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.Guid> clientId { get; set; }
        [DataMember]
        public ClientDto Client { get; set; }
        [DataMember]
        public UserGroupDto UserGroup { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public ICollection<UserPreferenceDto> UserPreference { get; set; }
        [DataMember]
        public ICollection<UserDataDto> UserData { get; set; }
        [DataMember]
        public ICollection<WorkOrderDto> WorkOrders { get; set; }
        [DataMember]
        public Dictionary<string, List<string>> ShareWidgets { get; set; }

    }
}
