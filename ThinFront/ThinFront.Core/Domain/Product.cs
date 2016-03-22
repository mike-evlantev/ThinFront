using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Domain
{
    public class Product
    {
        public int ProductId { get; set; }
        public int ProductSubCategoryId { get; set; }
        public string ImageUrl { get; set; }
        public string Brand { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }

        // on the 1 side of 1-to-many
        public virtual ProductSubcategory ProductSubCategory { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<PromotionalProduct> PromotionalProducts { get; set; }
    }
}
