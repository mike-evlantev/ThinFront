using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Models;

namespace ThinFront.Core.Domain
{
    public class OrderItem
    {
        public OrderItem()
        {

        }

        public OrderItem(OrderItemsModel orderItem)
        {
            this.Update(orderItem);
        }

        public void Update(OrderItemsModel orderItem)
        {
            OrderItemId = orderItem.OrderItemId;
            ProductId = orderItem.ProductId;
            ProductQuantity = orderItem.ProductQuantity;
            FinalPrice = orderItem.FinalPrice;
        }

        public int OrderItemId { get; set; }
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
