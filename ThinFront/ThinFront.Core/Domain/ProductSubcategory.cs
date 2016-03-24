using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Models;

namespace ThinFront.Core.Domain
{
    public class ProductSubcategory
    {
        // Update method used in the ProductSubcategoriesController (API)
        public void Update (ProductSubcategoriesModel productSubcategory)
        {
            ProductSubcategoryId = productSubcategory.ProductSubcategoryId;
            ProductSubcategoryName = productSubcategory.ProductSubcategoryName;
            ProductCategoryId = productSubcategory.ProductCategoryId;
        }
        public int ProductSubcategoryId { get; set; }
        public int ProductSubcategoryName { get; set; }
        public int ProductCategoryId { get; set; }

        //on the many side of 1-to-many
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
