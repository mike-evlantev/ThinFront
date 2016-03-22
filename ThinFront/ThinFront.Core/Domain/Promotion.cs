using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Domain
{
    public class Promotion
    {
        public int PromotionId { get; set; }
        public int SupplierId { get; set; }
        public string PromotionTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // on the many side of 1-to-many
        public virtual ICollection<PromotionalProduct> PromotionalProducts { get; set; }
        // on the 1 side of 1-to-many
        public virtual ThinFrontUser Supplier { get; set; }
        //public virtual Supplier Supplier { get; set; }
    }
}
