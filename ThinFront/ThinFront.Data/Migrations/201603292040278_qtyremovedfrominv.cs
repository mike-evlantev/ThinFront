namespace ThinFront.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qtyremovedfrominv : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Inventories", "ProductQuantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inventories", "ProductQuantity", c => c.Int(nullable: false));
        }
    }
}
