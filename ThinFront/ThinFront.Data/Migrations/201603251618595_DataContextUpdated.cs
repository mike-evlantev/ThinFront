namespace ThinFront.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataContextUpdated : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Products", new[] { "ProductSubCategoryId" });
            AddColumn("dbo.ThinFrontUsers", "SupplierId", c => c.Int());
            AddColumn("dbo.OrderItems", "OrderItemId", c => c.Int(nullable: false));
            AddColumn("dbo.PromotionalProducts", "PromotionalProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.ThinFrontUsers", "SupplierId");
            CreateIndex("dbo.Products", "ProductSubcategoryId");
            AddForeignKey("dbo.ThinFrontUsers", "SupplierId", "dbo.ThinFrontUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThinFrontUsers", "SupplierId", "dbo.ThinFrontUsers");
            DropIndex("dbo.Products", new[] { "ProductSubcategoryId" });
            DropIndex("dbo.ThinFrontUsers", new[] { "SupplierId" });
            DropColumn("dbo.PromotionalProducts", "PromotionalProductId");
            DropColumn("dbo.OrderItems", "OrderItemId");
            DropColumn("dbo.ThinFrontUsers", "SupplierId");
            CreateIndex("dbo.Products", "ProductSubCategoryId");
        }
    }
}
