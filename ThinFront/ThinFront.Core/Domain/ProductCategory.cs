using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Models;

namespace ThinFront.Core.Domain
{
    public class ProductCategory
    {
        // Update method used in the ProductCategoriesController (API)
        public void Update(ProductCategoriesModel productCategory)
        {
            // ProductCategoryId = productCategory.ProductCategoryId;
            ProductCategoryName = productCategory.ProductCategoryName;
            InventoryId = productCategory.InventoryId;

        }
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public int InventoryId { get; set; }

        // on the many side of 1-to-many
        public virtual Inventory Inventory { get; set; }

        // on the 1 side of 1-to-many	
        public virtual ICollection<ProductSubcategory> ProductSubcategories { get; set; }
        public virtual ICollection<ResellerProductCategory> ResellerProductCategories { get; set; }
    }
}
