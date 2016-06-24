using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Domain.Spec
{
  public interface IUsers
    {
         Guid Id { get; set; }

         string Name { get; set; }

         string AddressLine1 { get; set; }

         string AddressLine2 { get; set; }

         string ContactNumber { get; set; }

         string EmailAddress { get; set; }

         Guid UserGroupId { get; set; }

         string City { get; set; }

         string Country { get; set; }

         Guid Token { get; set; }

         IUserGroups UserGroups { get; set; }

         string ErrorMessage { get; set; }

         string Status { get; set; }

    }
}
