using System.Collections.Generic;
using System.ServiceModel;

namespace QSI.Services.Extensions
{
    /// <summary>
    /// ExtendedOperationContext.Current that carries lots of useful information 
    /// about the 'current' service operation being processed. 
    /// </summary>
    public class ExtendedOperationContext : IExtension<OperationContext>
    {
        private IDictionary<string, object> _items;


        /// <summary>
        /// This constructor initializes a dictionary object.
        /// </summary>
        public ExtendedOperationContext()
        {
            _items = new Dictionary<string, object>();
        }


        /// <summary>
        /// Creates an object context which is comprising of ExtendedOperationContext
        /// </summary>
        public static ExtendedOperationContext Current
        {
            get
            {
                if (OperationContext.Current == null)
                {
                    return null;
                }

                ExtendedOperationContext context = OperationContext.Current.Extensions.Find<ExtendedOperationContext>();
                if (context == null)
                {
                    context = new ExtendedOperationContext();
                    OperationContext.Current.Extensions.Add(context);
                }
                return context;
            }
        }


        /// <summary>
        /// Returns dictionary.
        /// </summary>
        public IDictionary<string, object> Items
        {
            get
            {
                return _items;
            }
        }


        /// <summary>
        /// The operation context to attach the extension to.
        /// </summary>
        public void Attach(OperationContext owner)
        {
        }


        /// <summary>
        /// The operation context to detach the extension to.
        /// </summary>
        public void Detach(OperationContext owner)
        {
        }
    }
}