using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Domain.Spec
{
    public interface IUserGroups
    {
         Guid Id { get; set; }

         string Name { get; set; }

         Guid ClientId { get; set; }

         string ModulesPermission { get; set; }

         string MapPermissions { get; set; }

         string UserType { get; set; }

    }
}
