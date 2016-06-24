using QSI.Domain.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Domain
{
  public partial  class Clients :IClients
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

    }
}
