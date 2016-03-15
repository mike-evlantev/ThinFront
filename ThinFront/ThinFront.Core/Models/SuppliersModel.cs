using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Domain;
using ThinFront.Core.Utility;

namespace ThinFront.Core.Models
{
    class SuppliersModel
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public AddressesModel BillingAddress { get; set; }
        public AddressesModel ShippingAddress { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }
}
