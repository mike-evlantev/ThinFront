using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Models
{
    public class InventoriesModel
    {
        public int InventoryId { get; set; }
        public int SupplierId { get; set; }
        public IEnumerable<ProductCategoriesModel> ProductCategories { get; set; }
    }
}
