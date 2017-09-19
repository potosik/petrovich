namespace Petrovich.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductPriceAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Price", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Price");
        }
    }
}
