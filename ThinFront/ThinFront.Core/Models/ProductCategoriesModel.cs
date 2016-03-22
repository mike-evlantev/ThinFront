using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Models
{
    public class ProductCategoriesModel
    {
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public int InventoryId { get; set; }

        public IEnumerable<ProductSubcategoriesModel> ProductSubcategories { get; set; }
    }
}
