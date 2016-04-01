namespace ThinFront.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddResellerProductTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResellerProducts",
                c => new
                    {
                        ResellerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ResellerId, t.ProductId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ThinFrontUsers", t => t.ResellerId, cascadeDelete: false)
                .Index(t => t.ResellerId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResellerProducts", "ResellerId", "dbo.ThinFrontUsers");
            DropForeignKey("dbo.ResellerProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.ResellerProducts", new[] { "ProductId" });
            DropIndex("dbo.ResellerProducts", new[] { "ResellerId" });
            DropTable("dbo.ResellerProducts");
        }
    }
}
