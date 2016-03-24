using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Domain
{
    public class ResellerProductCategory
    {
        // composite key = PK's of ResellerId and ProductCategoryId
        public int ResellerId { get; set; }
        public int ProductCategoryId { get; set; }

        // on the many side of 1-to-many
        public virtual ThinFrontUser User { get; set; }
        //public virtual Reseller Reseller { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
