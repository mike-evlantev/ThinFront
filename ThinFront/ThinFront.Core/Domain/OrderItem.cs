using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Domain
{
    public class OrderItem
    {
        // ProductId(PK) + OrderId(PK) = compound key
        // on the many side ot 1-to-many = capture ID
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public int ProductQuantity { get; set; }
        public decimal? FinalPrice { get; set; }
        public decimal TotalPrice
        {
            get
            {
                // Qty * FinalPrice is nullable(can have value or no value) that gets its value or defaults to product price
                return ProductQuantity * (FinalPrice = FinalPrice.GetValueOrDefault(Product.Price)).GetValueOrDefault();
            }
        }
        // on the many side of 1-to-many
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
