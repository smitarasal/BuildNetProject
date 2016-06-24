using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Domain.Spec
{
   public  interface IClients
    {

         Guid Id { get; set; }

         string Name { get; set; }

         string AddressLine1 { get; set; }

         string AddressLine2 { get; set; }

         string PhoneNumber { get; set; }

         string EmailAddress { get; set; }

    }
}
