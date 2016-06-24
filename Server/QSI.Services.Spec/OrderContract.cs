using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QSI.Services.Spec
{
    [DataContract]
    public class OrderContract
    {
        [DataMember]
        public string OrderID { get; set; }

        [DataMember]
        public string OrderDate { get; set; }

        [DataMember]
        public string ShippedDate { get; set; }

        [DataMember]
        public string ShipCountry { get; set; }

        [DataMember]
        public string OrderTotal { get; set; }
    }
}
