using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Domain;

namespace ThinFront.Core.Models
{
    public class OrderItemsModel
    {
        public int OrderItemId { get; set; }
        // ProductId(PK) + OrderId(PK) = compound key
        // on the many side ot 1-to-many = capture ID
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public int ProductQuantity { get; set; }
        public decimal? FinalPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
