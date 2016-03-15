using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinFront.Core.Domain;

namespace ThinFront.Data.Infrastructure
{
    public class ThinFrontDataContext : DbContext
    {
        // create a constructor that must inherit from a parent 
        // constructor because it inherits from DbContext
        // the "Name" after base will be the name of the DB 
        public ThinFrontDataContext() : base("ThinFront")
        {
        }
        // need an IDbSet for each of my classes
        // IDbSet is another name for a list that can talk to a DB
        // These will be SQL tables
        public IDbSet<Address> Addresses { get; set; }
        public IDbSet<AddressType> AddressTypes { get; set; }
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<Inventory> Inventories { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<OrderItem> OrderItems { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<ProductCategory> ProductCategories { get; set; }
        public IDbSet<ProductSubcategory> ProductSubcategories { get; set; }
        public IDbSet<Promotion> Promotions { get; set; }
        public IDbSet<PromotionalProduct> PromotionalProducts { get; set; }
        public IDbSet<Reseller> Resellers { get; set; }
        public IDbSet<ResellerProductCategory> ResellerProductCategories { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<Supplier> Suppliers { get; set; }
        public IDbSet<ThinFrontUser> Users { get; set; }

        // Explicitly model relationships
        // Code-First Fluent API
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // go through each class and model each relationship individually

            // based on the ERD an Address has many AddressTypes, Customers, Resellers, and Suppliers
            // only entity relationships on the 1 side of 1-to-many are modelled here
            // **if modelling relationships on the many side of 1-to-many then use .HasRequired and .WithMany**

            // Address is on the many side of all of its relationships. It will not be modelled
            // model compound key of entity Address
            modelBuilder.Entity<Address>()
                        // the compound key for address is a combo of the 4 keys below
                        .HasKey(a => new { a.AddressTypeId, a.CustomerId, a.ResellerId, a.SupplierId });

            // model for AddressType
            // on the 1 side of 1-to-many with Address
            // build a model of entity Address that...	
            modelBuilder.Entity<AddressType>()
                        // ...is on the 1 side of 1-to-many with Addresses...
                        .HasMany(at => at.Addresses)
                        // ...where a AddressType is required (!nullable) to have an Address...
                        .WithRequired(a => a.AddressType)
                        // ...and Address has a foreign key of AddressTypeId
                        .HasForeignKey(a => a.AddressTypeId);

            // model of Customer
            // on the 1 side of 1-to-many with Address
            // on the 1 side of 1-to-many with Orders
            modelBuilder.Entity<Customer>()
                        .HasMany(c => c.Addresses)
                        .WithRequired(a => a.Customer)
                        .HasForeignKey(a => a.CustomerId);


            modelBuilder.Entity<Customer>()
                        .HasMany(c => c.Orders)
                        .WithRequired(o => o.Customer)
                        .HasForeignKey(o => o.CustomerId);

            // model of Inventory
            // on the 1 side of 1-to-many with ProductCategory
            modelBuilder.Entity<Inventory>()
                        .HasMany(i => i.ProductCategories)
                        .WithRequired(pc => pc.Inventory)
                        .HasForeignKey(pc => pc.InventoryId);

            // model of Order
            // on the 1 side of 1-to-many with Address
            modelBuilder.Entity<Order>()
                        .HasMany(o => o.OrderItems)
                        .WithRequired(oi => oi.Order)
                        .HasForeignKey(oi => oi.OrderId);

            // OrderItem is on the many side of both of its 1-to-many relationships. It will not be modelled
            // model compound key of entity OrderItem
            modelBuilder.Entity<OrderItem>()
                        // the compound key for OrderItem is a combo of the 2 keys below
                        .HasKey(oi => new { oi.OrderId, oi.ProductId });

            // model of Product
            // on the 1 side of 1-to-many with OrderItem
            // on the 1 side of 1-to-many with ProductSubcategory
            // on the 1 side of 1-to-many with PromotionalProduct
            modelBuilder.Entity<Product>()
                        .HasMany(p => p.OrderItems)
                        .WithRequired(oi => oi.Product)
                        .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<Product>()
                        .HasMany(p => p.ProductSubcategories)
                        .WithRequired(ps => ps.Product)
                        .HasForeignKey(ps => ps.ProductId);

            modelBuilder.Entity<Product>()
                        .HasMany(p => p.PromotionalProducts)
                        .WithRequired(pp => pp.Product)
                        .HasForeignKey(pp => pp.ProductId);

            // model of ProductCategory
            // on the 1 side of 1-to-many with ResellerProductCategory
            // on the 1 side of 1-to-many with ProductSubcategory
            modelBuilder.Entity<ProductCategory>()
                        .HasMany(pc => pc.ResellerProductCategories)
                        .WithRequired(rpc = rpc.ProductCategory)
                        .HasForeignKey(rpc = rpc.ProductCategoryId);

            modelBuilder.Entity<ProductCategory>()
                        .HasMany(pc => pc.ProductSubcategories)
                        .WithRequired(ps = ps.ProductCategory)
                        .HasForeignKey(ps = ps.ProductCategoryId);

            // ProductSubcategory is on the many side of both of its 1-to-many relationships.
            // model compound key of entity ProductSubcategory
            modelBuilder.Entity<ProductSubcategory>()
                        // the compound key for ProductSubcategory is a combo of the 2 keys below
                        .HasKey(ps => new { ps.ProductId, ps.ProductCategory });


            // model of Promotion
            // on the 1 side of 1-to-many with PromotionalProduct
            modelBuilder.Entity<Promotion>()
                        .HasMany(p => p.PromotionalProducts)
                        .WithRequired(pp => pp.Promotion)
                        .HasForeignKey(pp => pp.PromotionId);

            // PromotionalProduct is on the many side of both of its 1-to-many relationships.
            // model compound key of entity PromotionalProduct
            modelBuilder.Entity<PromotionalProduct>()
                        // the compound key for PromotionalProduct is a combo of the 2 keys below
                        .HasKey(pp => new { pp.ProductId, pp.PromotionId });

            // model of Reseller
            // on the 1 side of 1-to-many with Address
            // on the 1 side of 1-to-many with Customer
            // on the 1 side of 1-to-many with ResellerProductCategory
            modelBuilder.Entity<Reseller>()
                        .HasMany(r => r.Addresses)
                        .WithRequired(a => a.Reseller)
                        .HasForeignKey(a => a.ResellerId);

            modelBuilder.Entity<Reseller>()
                        .HasMany(r => r.Customers)
                        .WithRequired(c => c.Reseller)
                        .HasForeignKey(c => c.ResellerId);

            modelBuilder.Entity<Reseller>()
                        .HasMany(r => r.ResellerProductCategories)
                        .WithRequired(rpc => rpc.Reseller)
                        .HasForeignKey(rpc => rpc.ResellerId);

            // ResellerProductCategory is on the many side of both of its 1-to-many relationships.
            // model compound key of entity ResellerProductCategory
            modelBuilder.Entity<ResellerProductCategory>()
                        // the compound key for ResellerProductCategory is a combo of the 2 keys below
                        .HasKey(pp => new { rpc.ProductCategoryId, rpc.Reseller });

            // model of Role
            // on the 1 side of 1-to-many with ThinFrontUser
            modelBuilder.Entity<Role>()
                        .HasMany(r => r.Users)
                        .WithRequired(u => u.Role)
                        .HasForeignKey(u => u.RoleId);

            // model of Supplier
            // on the 1 side of 1-to-many with Address
            // on the 1 side of 1-to-many with Inventory
            // on the 1 side of 1-to-many with Promotion
            // on the principal side of 1-to-1 with ThinFrontUser
            modelBuilder.Entity<Supplier>()
                        .HasMany(s => s.Addresses)
                        .WithRequired(a => a.Supplier)
                        .HasForeignKey(a => a.SupplierId);

            modelBuilder.Entity<Supplier>()
                        .HasMany(s => s.Inventories)
                        .WithRequired(i => i.Supplier)
                        .HasForeignKey(i => i.SupplierId);

            modelBuilder.Entity<Supplier>()
                        .HasMany(s => s.Promotions)
                        .WithRequired(p => p.Supplier)
                        .HasForeignKey(p => p.SupplierId);

            modelBuilder.Entity<Supplier>()
                        .HasRequired(s => s.User)
                        .WithOptional(u => u.Supplier);
            // model of ThinFrontUser
            // on the dependant side of 1-to-1 with Customer
            // on the dependant side of 1-to-1 with Reseller
            // on the dependant side of 1-to-1 with Supplier
            modelBuilder.Entity<ThinFrontUser>()
                        .HasRequired(u => u.Customer)
                        .WithOptional(c => c.User);
        }
    }
}
