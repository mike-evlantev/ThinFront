using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Utility;

namespace ThinFront.Core.Domain
{
    public class Reseller
    {
        public int ResellerId { get; set; }
        public int? UserId { get; set; }
        public string ResellerName { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string Phone { get; set; }
        public Address BillingAddress
        {
            get
            {
                // returns a billing address for the reseller
                return AddressInitializer.Initialize(Addresses, AddressTypes.Billing);
            }
        }
        public Address ShippingAddress
        {
            get
            {
                // returns a shipping address for the reseller
                return AddressInitializer.Initialize(Addresses, AddressTypes.Shipping);
            }
        }
        public string Fax { get; set; }
        public string Email { get; set; }

        // on the principal side of 1-to-1
        // 1-to-1 is (Principal)<--->(Dependent)
        // Principal has no foreign key
        // Dependent has the foreign key
        public virtual ThinFrontUser User { get; set; }

        // on the 1 side of 1-to-many
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<ResellerProductCategory> ResellerProductCategories { get; set; }
    }
}
