﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Domain
{
    public class ProductSubcategory
    {
        // composite key
        public int ProductSubCategoryId { get; set; }
        public int ProductCategoryId { get; set; }

        public int ProductSubcategoryName { get; set; }

        //on the many side of 1-to-many
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
