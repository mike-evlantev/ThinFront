using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Domain;
using ThinFront.Core.Utility;

namespace ThinFront.Core.Models
{
    public class CustomersModel
    {
        // properties
        public int CustomerId { get; set; }
        public int ResellerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressesModel BillingAddress { get; set; }
        public AddressesModel ShippingAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
