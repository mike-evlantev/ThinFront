using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Domain;

namespace ThinFront.Core.Models
{
    public class PromotionalProductsModel
    {
        public int PromotionalProductId { get; set; }
        // needs compound key
        public int ProductId { get; set; }
        public int PromotionId { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal PromoPrice { get; set; }
    }
}
