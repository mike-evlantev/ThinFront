using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Models
{
    class ProductSubcategoriesModel
    {
        // compound key
        public int ProductCategoryId { get; set; }
        public int ProductId { get; set; }
        public int ProductSubcategoryName { get; set; }
    }
}
