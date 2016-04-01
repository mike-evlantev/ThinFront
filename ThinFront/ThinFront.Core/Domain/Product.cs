using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Models;

namespace ThinFront.Core.Domain
{
    public class Product
    {
        public Product()
        {

        }

        // POST 
        public Product(ProductsModel product)
        {
            this.Update(product);
        }


        // Update method that is used in the ProductController (API)
        public void Update(ProductsModel product)
        {
            ProductId = product.ProductId;
            ProductSubcategoryId = product.ProductSubcategoryId;
            ImageUrl = product.ImageUrl;
            Brand = product.Brand;
            Title = product.Title;
            Description = product.Description;
            Size = product.Size;
            Color = product.Color;
            Price = product.Price;
            Quantity = product.Quantity;
        }
        public int ProductId { get; set; }
        public int ProductSubcategoryId { get; set; }
        public string ImageUrl { get; set; }
        public string Brand { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // on the 1 side of 1-to-many
        public virtual ProductSubcategory ProductSubcategory { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<PromotionalProduct> PromotionalProducts { get; set; }
        public virtual ICollection<ResellerProduct> ResellerProducts { get; set; }
    }
}
