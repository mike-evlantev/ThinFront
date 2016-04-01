using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Domain
{
    public class ResellerProduct
    {
        public int ResellerId { get; set; }
        public int ProductId { get; set; }

        public virtual ThinFrontUser Reseller { get; set; }
        public virtual Product Product { get; set; }
    }
}
