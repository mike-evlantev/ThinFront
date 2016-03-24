using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Models;

namespace ThinFront.Core.Domain
{
    public class Promotion
    {
        // TO BE USED in PromotionsController (POST: Product-to-Promotion)
        // public void Add(PromotionalProductsModel product)
        // {
        // 
        // }
        
        //Update method used in the PromotionsController (API)
        public void Update(PromotionsModel promotion)
        {
            PromotionId = promotion.PromotionId;
            SupplierId = promotion.SupplierId;
            PromotionTitle = promotion.PromotionTitle;
            StartDate = promotion.StartDate;
            EndDate = promotion.EndDate;
        }
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
