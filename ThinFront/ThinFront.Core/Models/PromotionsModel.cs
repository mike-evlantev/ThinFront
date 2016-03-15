using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Models
{
    class PromotionsModel
    {
        public int PromotionId { get; set; }
        public int SupplierId { get; set; }
        public string PromotionTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
