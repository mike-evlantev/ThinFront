using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Models
{
    public class OrdersModel
    {
        // properties 
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal? OrderTotal { get; set; }

        public ThinFrontUsersModel User { get; set; }

        public IEnumerable<OrderItemsModel> OrderItems { get; set; }
    }
}
