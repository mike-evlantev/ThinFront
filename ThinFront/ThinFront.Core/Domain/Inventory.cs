using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Models;

namespace ThinFront.Core.Domain
{
    public class Inventory
    {
        // Update method used in the InventoriesController(API)
        public void Update(InventoriesModel inventory)
        {
            // ???Update InventoryId???
            InventoryId = inventory.InventoryId;
            SupplierId = inventory.SupplierId;
            ProductQuantity = inventory.ProductQuantity;
        } 

        public int InventoryId { get; set; }
        public int SupplierId { get; set; }
        public int ProductQuantity { get; set; }

        // on the many side of 1-to-many
        public virtual ThinFrontUser Supplier { get; set; }
        //public virtual Supplier Supplier { get; set; }

        // on the 1 side of 1-to-many
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
