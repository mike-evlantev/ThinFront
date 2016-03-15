using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Domain
{
    public class Address
    {
        // properties			
        public int AddressTypeId { get; set; }
        public int? CustomerId { get; set; }
        public int? ResellerId { get; set; }
        public int? SupplierId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        // on the many side of 1-to-many
        public virtual AddressType AddressType { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Reseller Reseller { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
