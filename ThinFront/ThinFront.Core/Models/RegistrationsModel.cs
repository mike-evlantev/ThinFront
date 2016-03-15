using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Models
{
    public class RegistrationsModel
    {
        // copy from pawze
        public class Customer
        {
            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            public AddressesModel BillingAddress {get;set;}
            public AddressesModel ShippingAddress { get; set; }

            [Required]
            public string EmailAddress { get; set; }

            [Required]
            [StringLength(64, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The password and confirm passwords do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public class Reseller
        {
            [Required]
            public string ResellerName { get; set; }

            [Required]
            public string ContactFirstName { get; set; }

            [Required]
            public string ContactLastName { get; set; }

            public AddressesModel BillingAddress { get; set; }
            public AddressesModel ShippingAddress { get; set; }

            [Required]
            public string Phone { get; set; }

            [Required]
            public string Fax { get; set; }

            [Required]
            public string EmailAddress { get; set; }

            [Required]
            [StringLength(64, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The password and confirm passwords do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public class Supplier
        {
            [Required]
            public string SupplierName { get; set; }

            [Required]
            public string ContactFirstName { get; set; }

            [Required]
            public string ContactLastName { get; set; }

            public AddressesModel BillingAddress { get; set; }
            public AddressesModel ShippingAddress { get; set; }

            [Required]
            public string Phone { get; set; }

            [Required]
            public string Fax { get; set; }

            [Required]
            public string EmailAddress { get; set; }

            [Required]
            [StringLength(64, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The password and confirm passwords do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
}
