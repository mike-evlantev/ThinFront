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
        //public IDbSet<Customer> Customers { get; set; }
        public IDbSet<Inventory> Inventories { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<OrderItem> OrderItems { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<ProductCategory> ProductCategories { get; set; }
        public IDbSet<ProductSubcategory> ProductSubcategories { get; set; }
        public IDbSet<Promotion> Promotions { get; set; }
        public IDbSet<PromotionalProduct> PromotionalProducts { get; set; }
        //public IDbSet<Reseller> Resellers { get; set; }
        public IDbSet<ResellerProductCategory> ResellerProductCategories { get; set; }
        public IDbSet<Role> Roles { get; set; }
        //public IDbSet<Supplier> Suppliers { get; set; }
        public IDbSet<ThinFrontUser> Users { get; set; }
        public IDbSet<ResellerProduct> ResellerProducts { get; set; }

        // Explicitly model relationships
        // Code-First Fluent API
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // go through each class and model each relationship individually

            // based on the ERD an Address has many AddressTypes, Customers, Resellers, and Suppliers
            // only entity relationships on the 1 side of 1-to-many are modelled here
            // **if modelling relationships on the many side of 1-to-many then use .HasRequired and .WithMany**

            // START OF COMMENT: Conflict between ".HasKey" and ".WithOptional". Commented out below.

            // Address is on the many side of all of its relationships. It will not be modelled
            // model compound key of entity Address
            // modelBuilder.Entity<Address>()
                        // the compound key for address is a combo of the 4 keys below
                        //.HasKey(a => new { a.AddressTypeId, a.CustomerId, a.ResellerId, a.SupplierId });
            
            // END OF COMMENT

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
            // on the principal side of the 1-to-1 with ThinFrontUser
            modelBuilder.Entity<ThinFrontUser>()
                        .HasMany(c => c.Addresses)
                        .WithRequired(a => a.User)
                        .HasForeignKey(a => a.UserId)
                        .WillCascadeOnDelete(false);


            modelBuilder.Entity<ThinFrontUser>()
                        .HasMany(c => c.Orders)
                        .WithRequired(o => o.Customer)
                        .HasForeignKey(o => o.CustomerId)
                        .WillCascadeOnDelete(false);


            // No longer needed, this used to map a Customer to it's User record
            //modelBuilder.Entity<Customer>()
            //            .HasOptional(c => c.User)
            //            .WithRequired(u => u.Customer);

            // model of Inventory
            // on the 1 side of 1-to-many with ProductCategory
            modelBuilder.Entity<Inventory>()
                        .HasMany(i => i.ProductCategories)
                        .WithRequired(pc => pc.Inventory)
                        .HasForeignKey(pc => pc.InventoryId);

            modelBuilder.Entity<ResellerProduct>().HasKey(rp => new { rp.ResellerId, rp.ProductId });

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
                        .HasMany(p => p.ResellerProducts)
                        .WithRequired(pr => pr.Product)
                        .HasForeignKey(pr => pr.ProductId);

            modelBuilder.Entity<Product>()
                        .HasMany(p => p.PromotionalProducts)
                        .WithRequired(pp => pp.Product)
                        .HasForeignKey(pp => pp.ProductId);

            // model of ProductCategory
            // on the 1 side of 1-to-many with ResellerProductCategory
            // on the 1 side of 1-to-many with ProductSubcategory
            modelBuilder.Entity<ProductCategory>()
                        .HasMany(pc => pc.ResellerProductCategories)
                        .WithRequired(rpc => rpc.ProductCategory)
                        .HasForeignKey(rpc => rpc.ProductCategoryId);

            modelBuilder.Entity<ProductCategory>()
                        .HasMany(pc => pc.ProductSubcategories)
                        .WithRequired(ps => ps.ProductCategory)
                        .HasForeignKey(ps => ps.ProductCategoryId);

            // ProductSubcategory is on the many side of both of its 1-to-many relationships.
            // model compound key of entity ProductSubcategory
            modelBuilder.Entity<ProductSubcategory>()
                        .HasMany(psc => psc.Products)
                        .WithRequired(p => p.ProductSubcategory)
                        .HasForeignKey(p => p.ProductSubcategoryId);



            // model of Promotion
            // on the 1 side of 1-to-many with PromotionalProduct
            modelBuilder.Entity<Promotion>()
                        .HasMany(p => p.PromotionalProducts)
                        .WithRequired(pp => pp.Promotion)
                        .HasForeignKey(pp => pp.PromotionId)
                        .WillCascadeOnDelete(false);

            // PromotionalProduct is on the many side of both of its 1-to-many relationships.
            // model compound key of entity PromotionalProduct
            modelBuilder.Entity<PromotionalProduct>()
                        // the compound key for PromotionalProduct is a combo of the 2 keys below
                        .HasKey(pp => new { pp.ProductId, pp.PromotionId });

            // model of Reseller
            // on the 1 side of 1-to-many with Address
            // on the 1 side of 1-to-many with Customer
            // on the 1 side of 1-to-many with ResellerProductCategory
            // on the principal side of 1-to-1 with ThinFrontUser
            //modelBuilder.Entity<Reseller>()
            //            .HasMany(r => r.Addresses)
            //            .WithOptional(a => a.Reseller)
            //            .HasForeignKey(a => a.ResellerId);

            modelBuilder.Entity<ThinFrontUser>()
                        .HasMany(r => r.Customers)
                        .WithOptional(c => c.Reseller)
                        .HasForeignKey(c => c.ResellerId);

            modelBuilder.Entity<ThinFrontUser>()
                        .HasMany(r => r.ResellerProducts)
                        .WithRequired(r => r.Reseller)
                        .HasForeignKey(r => r.ResellerId)
                        .WillCascadeOnDelete(true);                    

            modelBuilder.Entity<ThinFrontUser>()
                        .HasMany(r => r.ResellerProductCategories)
                        .WithRequired(rpc => rpc.User)
                        .HasForeignKey(rpc => rpc.ResellerId)
                        .WillCascadeOnDelete(false);
                        // ResellerProductCategories will be deleted through
                        // ALLOWED: ThinFrontUser(Supplier) -> Inventory -> Product Category -> ResellerProductCategory
                        // DENIED : ThinFrontUser(Supplier) -> ResellerProductCategory

            // No longer needed, this used to map a Reseller to it's User record
            //modelBuilder.Entity<Reseller>()
            //            .HasOptional(r => r.User)
            //            .WithRequired(u => u.Reseller);

            // ResellerProductCategory is on the many side of both of its 1-to-many relationships.
            // model compound key of entity ResellerProductCategory
            modelBuilder.Entity<ResellerProductCategory>()
                        // the compound key for ResellerProductCategory is a combo of the 2 keys below
                        .HasKey(rpc => new { rpc.ProductCategoryId, rpc.ResellerId });

            // model of Role
            // on the 1 side of 1-to-many with ThinFrontUser
            modelBuilder.Entity<Role>()
                        .HasMany(r => r.Users)
                        .WithOptional(u => u.Role)
                        .HasForeignKey(u => u.RoleId);

            // model of Supplier
            // on the 1 side of 1-to-many with Address
            // on the 1 side of 1-to-many with Inventory
            // on the 1 side of 1-to-many with Promotion
            // on the principal side of 1-to-1 with ThinFrontUser
            //modelBuilder.Entity<Supplier>()
            //            .HasMany(s => s.Addresses)
            //            .WithOptional(a => a.Supplier)
            //            .HasForeignKey(a => a.SupplierId);

            modelBuilder.Entity<ThinFrontUser>()
                        .HasMany(s => s.Inventories)
                        .WithRequired(i => i.Supplier)
                        .HasForeignKey(i => i.SupplierId);

            modelBuilder.Entity<ThinFrontUser>()
                        .HasMany(s => s.Promotions)
                        .WithRequired(p => p.Supplier)
                        .HasForeignKey(p => p.SupplierId);

            // No longer needed, this used to map a Supplier to it's User record
            //modelBuilder.Entity<Supplier>()
            //            .HasOptional(s => s.User)
            //            .WithRequired(u => u.Supplier);
        }
    }
}
