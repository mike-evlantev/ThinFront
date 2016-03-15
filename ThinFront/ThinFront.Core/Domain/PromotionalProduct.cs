using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Domain
{
    public class PromotionalProduct
    {
        // needs composite key
        public int ProductId { get; set; }
        public int PromotionId { get; set; }
	    public decimal DiscountPercentage { get; set; }
        public decimal PromoPrice
        {
            get
            {
                return Product.Price - (Product.Price * DiscountPercentage);
            }
        }

        // on the 1 side of 1-to-1
        public virtual Product Product { get; set; }
        public virtual Promotion Promotion { get; set; }
    }
}
