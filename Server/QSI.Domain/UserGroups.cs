using QSI.Domain.Spec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Domain
{
   public partial class UserGroups :IUserGroups
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ClientId { get; set; }

        public string ModulesPermission { get; set; }

        public string MapPermissions { get; set; }

        public string UserType { get; set; }
    }
}
