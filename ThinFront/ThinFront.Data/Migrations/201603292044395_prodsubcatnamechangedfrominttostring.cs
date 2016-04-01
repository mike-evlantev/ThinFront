namespace ThinFront.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prodsubcatnamechangedfrominttostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductSubcategories", "ProductSubcategoryName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductSubcategories", "ProductSubcategoryName", c => c.Int(nullable: false));
        }
    }
}
