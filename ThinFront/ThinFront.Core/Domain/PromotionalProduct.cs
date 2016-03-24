using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Models;

namespace ThinFront.Core.Domain
{
    public class PromotionalProduct
    {
        public PromotionalProduct()
        {

        }

        public PromotionalProduct(PromotionalProductsModel promotionalProduct)
        {
            this.Update(promotionalProduct);
        }

        public void Update(PromotionalProductsModel promotionalProduct)
        {
            PromotionalProductId = promotionalProduct.PromotionalProductId;
            ProductId = promotionalProduct.ProductId;
            PromotionId = promotionalProduct.PromotionId;
            DiscountPercentage = promotionalProduct.DiscountPercentage;
        }

        public int PromotionalProductId { get; set; }
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
