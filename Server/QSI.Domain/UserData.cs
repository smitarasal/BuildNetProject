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
    
    public partial class UserData
    {
        public System.Guid Id { get; set; }
        public System.Guid UserId { get; set; }
        public string Keys { get; set; }
        public string Value { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual User User { get; set; }
    }
}
