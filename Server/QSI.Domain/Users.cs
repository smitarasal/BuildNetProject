using QSI.Domain.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Domain
{
    public partial class Users : IUsers
    {

        public Guid Id  { get; set; }

        public string Name { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string  ContactNumber { get; set; }

        public string EmailAddress { get; set; }

        public Guid UserGroupId { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public Guid Token { get; set; }

        public IUserGroups UserGroups { get; set; }

        public string ErrorMessage { get; set; }

        public string Status { get; set; }

     
    }
}
