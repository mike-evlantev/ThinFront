using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Models
{
    public class ThinFrontUsersModel
    {
        public int Id { get; set; }
        // User is on the many side of 1-to-many of relationship with Role (gets a foreign key)
        public int RoleId { get; set; }
        public string UserName { get; set; }

        public int? CustomerId { get; set; }
        public int? ResellerId { get; set; }
        public int? SupplierId { get; set; }
    }
}
