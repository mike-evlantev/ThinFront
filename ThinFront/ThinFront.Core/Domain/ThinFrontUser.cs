using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Domain
{
    public class ThinFrontUser : IUser<int>
    {
        public ThinFrontUser()
        {

        }
        public int Id { get; set; }
        // User is on the many side of 1-to-many of relationship with Role (gets a foreign key)
        public int RoleId { get; set; }
        public string UserName { get; set; }
        // never modelled
        public string PasswordHash { get; set; }
        // never modelled
        public string SecurityStamp { get; set; }

        public int? CustomerId { get; set; }
        public int? ResellerId { get; set; }
        public int? SupplierId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Reseller Reseller { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Role Role { get; set; }
    }
}
