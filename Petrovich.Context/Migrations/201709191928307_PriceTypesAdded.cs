namespace Petrovich.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceTypesAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "PriceType", c => c.Int());
            AddColumn("dbo.Groups", "PriceType", c => c.Int());
            AddColumn("dbo.Products", "PriceType", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "PriceType");
            DropColumn("dbo.Groups", "PriceType");
            DropColumn("dbo.Categories", "PriceType");
        }
    }
}
