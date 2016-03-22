using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Models
{
    public class ResellerProductCategoriesModel
    {
        // compound key = PK's of ResellerId and ProductCategoryId
        public int ResellerId { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
