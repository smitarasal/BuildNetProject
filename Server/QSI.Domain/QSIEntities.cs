using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Domain
{
    public partial class QSIEntities
    {

        public bool IsEventRegistered { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        /// <value>The user ID.</value>
        public Guid UserID { get; set; }
    }
}
