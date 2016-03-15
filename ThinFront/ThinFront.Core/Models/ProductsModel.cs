using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinFront.Core.Models
{
    public class ProductsModel
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public string Brand { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
    }
}
