//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ThinFront.Core.Utility;

//namespace ThinFront.Core.Domain
//{
//    public class Supplier
//    {
//        public int SupplierId { get; set; }
//        public int? UserId { get; set; }
//        public string SupplierName { get; set; }
//        public string ContactFirstName { get; set; }
//        public string ContactLastName { get; set; }
//        public Address BillingAddress
//        {
//            get
//            {
//                return AddressInitializer.Initialize(Addresses, AddressTypes.Billing);
//            }
//        }
//        public Address ShippingAddress
//        {
//            get
//            {
//                return AddressInitializer.Initialize(Addresses, AddressTypes.Shipping);
//            }
//        }
//        public string Phone { get; set; }
//        public string Fax { get; set; }
//        public string Email { get; set; }

//        // on the principal side of 1-to-1
//        // 1-to-1 is (Principal)<--->(Dependent)
//        // Principal has no foreign key
//        // Dependent has the foreign key
//        public virtual ThinFrontUser User { get; set; }

//        // on the 1 side of 1-to-many
//        public virtual ICollection<Address> Addresses { get; set; }
//        public virtual ICollection<Promotion> Promotions { get; set; }
//        public virtual ICollection<Inventory> Inventories { get; set; }
//    }
//}
