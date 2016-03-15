using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Domain
{
    // identifies the PK for AddressType
    public enum AddressTypes
    {
        Billing = 1,
        Shipping = 2
    }
    public class AddressType
    {
        // properties
        public int AddressTypeId { get; set; }
        public string Description { get; set; }

        // on the 1 side of 1-to-many
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
