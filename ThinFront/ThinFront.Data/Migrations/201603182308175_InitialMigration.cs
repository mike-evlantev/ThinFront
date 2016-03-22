namespace ThinFront.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        AddressTypeId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        AddressTypeEnum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.AddressTypes", t => t.AddressTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ThinFrontUsers", t => t.UserId)
                .Index(t => t.AddressTypeId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AddressTypes",
                c => new
                    {
                        AddressTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AddressTypeId);
            
            CreateTable(
                "dbo.ThinFrontUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(),
                        ResellerId = c.Int(),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        CompanyName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ThinFrontUsers", t => t.ResellerId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.ResellerId);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        ProductQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryId)
                .ForeignKey("dbo.ThinFrontUsers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ProductCategoryId = c.Int(nullable: false, identity: true),
                        ProductCategoryName = c.String(),
                        InventoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductCategoryId)
                .ForeignKey("dbo.Inventories", t => t.InventoryId, cascadeDelete: true)
                .Index(t => t.InventoryId);
            
            CreateTable(
                "dbo.ProductSubcategories",
                c => new
                    {
                        ProductSubCategoryId = c.Int(nullable: false, identity: true),
                        ProductCategoryId = c.Int(nullable: false),
                        ProductSubcategoryName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductSubCategoryId)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductSubCategoryId = c.Int(nullable: false),
                        ImageUrl = c.String(),
                        Brand = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        Size = c.String(),
                        Color = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductSubcategories", t => t.ProductSubCategoryId, cascadeDelete: true)
                .Index(t => t.ProductSubCategoryId);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductQuantity = c.Int(nullable: false),
                        FinalPrice = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.ThinFrontUsers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.PromotionalProducts",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        PromotionId = c.Int(nullable: false),
                        DiscountPercentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.ProductId, t.PromotionId })
                .ForeignKey("dbo.Promotions", t => t.PromotionId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.PromotionId);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        PromotionId = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        PromotionTitle = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PromotionId)
                .ForeignKey("dbo.ThinFrontUsers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.ResellerProductCategories",
                c => new
                    {
                        ProductCategoryId = c.Int(nullable: false),
                        ResellerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductCategoryId, t.ResellerId })
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.ThinFrontUsers", t => t.ResellerId)
                .Index(t => t.ProductCategoryId)
                .Index(t => t.ResellerId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThinFrontUsers", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ResellerProductCategories", "ResellerId", "dbo.ThinFrontUsers");
            DropForeignKey("dbo.Promotions", "SupplierId", "dbo.ThinFrontUsers");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.ThinFrontUsers");
            DropForeignKey("dbo.Inventories", "SupplierId", "dbo.ThinFrontUsers");
            DropForeignKey("dbo.ProductCategories", "InventoryId", "dbo.Inventories");
            DropForeignKey("dbo.ResellerProductCategories", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.ProductSubcategories", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.Products", "ProductSubCategoryId", "dbo.ProductSubcategories");
            DropForeignKey("dbo.PromotionalProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PromotionalProducts", "PromotionId", "dbo.Promotions");
            DropForeignKey("dbo.OrderItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.ThinFrontUsers", "ResellerId", "dbo.ThinFrontUsers");
            DropForeignKey("dbo.Addresses", "UserId", "dbo.ThinFrontUsers");
            DropForeignKey("dbo.Addresses", "AddressTypeId", "dbo.AddressTypes");
            DropIndex("dbo.ResellerProductCategories", new[] { "ResellerId" });
            DropIndex("dbo.ResellerProductCategories", new[] { "ProductCategoryId" });
            DropIndex("dbo.Promotions", new[] { "SupplierId" });
            DropIndex("dbo.PromotionalProducts", new[] { "PromotionId" });
            DropIndex("dbo.PromotionalProducts", new[] { "ProductId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderItems", new[] { "ProductId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.Products", new[] { "ProductSubCategoryId" });
            DropIndex("dbo.ProductSubcategories", new[] { "ProductCategoryId" });
            DropIndex("dbo.ProductCategories", new[] { "InventoryId" });
            DropIndex("dbo.Inventories", new[] { "SupplierId" });
            DropIndex("dbo.ThinFrontUsers", new[] { "ResellerId" });
            DropIndex("dbo.ThinFrontUsers", new[] { "RoleId" });
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropIndex("dbo.Addresses", new[] { "AddressTypeId" });
            DropTable("dbo.Roles");
            DropTable("dbo.ResellerProductCategories");
            DropTable("dbo.Promotions");
            DropTable("dbo.PromotionalProducts");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Products");
            DropTable("dbo.ProductSubcategories");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Inventories");
            DropTable("dbo.ThinFrontUsers");
            DropTable("dbo.AddressTypes");
            DropTable("dbo.Addresses");
        }
    }
}
