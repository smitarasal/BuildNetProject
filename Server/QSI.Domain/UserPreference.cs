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
    
    public partial class UserPreference
    {
        public System.Guid Id { get; set; }
        public System.Guid UserId { get; set; }
        public string PreferencesJson { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string PreferenceName { get; set; }
    
        public virtual User User { get; set; }
    }
}
