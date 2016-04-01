using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Models;
using ThinFront.Core.Utility;

namespace ThinFront.Core.Domain
{
    public class ThinFrontUser : IUser<int>
    {
        public ThinFrontUser()
        {

        }

        public void Update(ThinFrontUsersModel thinFrontUser)
        {
            RoleId = thinFrontUser.RoleId;
            ResellerId = thinFrontUser.ResellerId;
            UserName = thinFrontUser.UserName;
            CompanyName = thinFrontUser.CompanyName;
            FirstName = thinFrontUser.FirstName;
            LastName = thinFrontUser.LastName;
            Phone = thinFrontUser.Phone;
            Fax = thinFrontUser.Fax;
            // chain update methods together for nested objects
            BillingAddress.Update(thinFrontUser.BillingAddress);
            ShippingAddress.Update(thinFrontUser.ShippingAddress);
        }

        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? ResellerId { get; set; }
        public int? SupplierId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public Address BillingAddress
        {
            get
            {
                // returns a billing address for the customer
                return AddressInitializer.Initialize(Addresses, AddressTypes.Billing);
            }
        }
        public Address ShippingAddress
        {
            get
            {
                // returns a shipping address for the customer
                return AddressInitializer.Initialize(Addresses, AddressTypes.Shipping);
            }
        }

        public virtual Role Role { get; set; }
        public virtual ThinFrontUser Reseller { get; set; }
        public virtual ThinFrontUser Supplier { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Promotion> Promotions { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<ThinFrontUser> Customers { get; set; }
        public virtual ICollection<ResellerProductCategory> ResellerProductCategories { get; set; }
        public virtual ICollection<ResellerProduct> ResellerProducts { get; set; }
    }
}
