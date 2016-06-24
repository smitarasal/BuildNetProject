using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Domain.Spec
{
    public interface IDataContext<T> where T : ObjectContext
    {   
        /// <summary>
        /// Current Data Context.
        /// </summary>
        /// <value>The data context.</value>
        T DataContext { get; }
    }
}
