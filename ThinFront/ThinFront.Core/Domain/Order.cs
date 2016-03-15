using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Domain
{
    public class Order
    {
        // properties 
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal? OrderTotal
        {
            get
            {
                // lambda exp - takes input and gives output
                return OrderItems.Sum(oi => oi.TotalPrice);
            }
        }

        // on the 1 side of 1-to-many
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        // on the many side of 1-to-many
        public virtual Customer Customer { get; set; }
    }
}
