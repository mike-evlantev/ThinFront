using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Domain;

namespace ThinFront.Core.Utility
{
    // Creates an Address
    public class AddressInitializer
    {
        // returns Address based on the Address Type
        public static Address Initialize(ICollection<Address> addresses, AddressTypes addressTypeId) 
        {
            // stores an address with assigned Address Type
            Address address = addresses.FirstOrDefault(a => a.AddressTypeEnum == addressTypeId);

            // if the address does not exist...
            if (address == null)
            {
                // ...creates a new address...
                address = new Address
                {
                    // ...with an assigned Address Type
                    AddressTypeEnum = addressTypeId
                };
                // .. adds the created address to DB
                addresses.Add(address);
            }
            // ...otherwise returns an address
            return address;
        }
    }
}
