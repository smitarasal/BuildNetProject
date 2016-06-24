//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QSI.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Client
    {
        public Client()
        {
            this.ClientServers = new HashSet<ClientServer>();
            this.Users = new HashSet<User>();
            this.UserGroups = new HashSet<UserGroup>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime ModifiedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string title { get; set; }
        public string subTitle { get; set; }
        public string logo { get; set; }
        public string geometryService { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonPhoneNumber { get; set; }
    
        public virtual ICollection<ClientServer> ClientServers { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
