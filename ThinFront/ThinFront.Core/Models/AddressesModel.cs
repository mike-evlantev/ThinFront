using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Models
{
    public class AddressesModel
    {
        // properties			
        public int AddressTypeId { get; set; }
        public int? CustomerId { get; set; }
        public int? ResellerId { get; set; }
        public int? SupplierId { get; set; }
        // Right-click references
        // Add Reference
        // Search for ...annotaions
        // check box next to result
        // Inlcude namespace System.ComponentModel.DataAnnotations
        [Required]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
    }
}
