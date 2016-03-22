using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Domain;
using ThinFront.Core.Utility;

namespace ThinFront.Core.Models
{
    public class ThinFrontUsersModel
    {
        // public int Id { get; set; }
        // // User is on the many side of 1-to-many of relationship with Role (gets a foreign key)
        // public int RoleId { get; set; }
        // public string UserName { get; set; }
        // 
        // public int? CustomerId { get; set; }
        // public int? ResellerId { get; set; }
        // public int? SupplierId { get; set; }

        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? ResellerId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public AddressesModel BillingAddress { get; set; }
        public AddressesModel ShippingAddress { get; set; }
    }
}
