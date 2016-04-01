using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Models
{
    public class ProductSubcategoriesModel
    {
        public int ProductSubcategoryId { get; set; }
        public string ProductSubcategoryName { get; set; }
        public int ProductCategoryId { get; set; }

        public IEnumerable<ProductsModel> Products { get; set; }
    }
}
