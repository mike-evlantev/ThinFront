using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Models;

namespace ThinFront.Core.Domain
{
    public class Address
    {
        public Address()
        {

        }
        public Address(AddressesModel model, AddressTypes addressType)
        {
            this.Update(model);
            AddressTypeEnum = addressType;
        }

        // properties			
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public int UserId { get; set; }
        //public int? CustomerId { get; set; }
        //public int? ResellerId { get; set; }
        //public int? SupplierId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public AddressTypes AddressTypeEnum
        {
            get
            {
                return (AddressTypes)AddressTypeId;
            }
            set
            {
                AddressTypeId = (int)value;
            }
        }

        // on the many side of 1-to-many
        public virtual AddressType AddressType { get; set; }
        public virtual ThinFrontUser User { get; set; }
        //public virtual Customer Customer { get; set; }
        //public virtual Reseller Reseller { get; set; }
        //public virtual Supplier Supplier { get; set; }

        public void Update(AddressesModel model)
        {
            Address1 = model.Address1;
            Address2 = model.Address2;
            City = model.City;
            State = model.State;
            ZipCode = model.ZipCode;
        }
    }
}
