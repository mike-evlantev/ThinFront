using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Models;

namespace ThinFront.Core.Domain
{
    public class Order
    {

        // Update method used in the OrdersController(API)
        public void Update(OrdersModel order)
        {
            OrderId = order.OrderId;
            CustomerId = order.CustomerId;
            OrderDate = order.OrderDate;
            OrderTotal = order.OrderTotal;
        }
        
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
        public virtual ThinFrontUser User { get; set; }
        //public virtual Customer Customer { get; set; }
    }
}
