namespace Petrovich.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceCalculationType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "PriceCalculationType", c => c.Int(nullable: false, defaultValue: 0));
            DropColumn("dbo.Categories", "PriceType");
            DropColumn("dbo.Groups", "PriceType");
            DropColumn("dbo.Products", "PriceType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "PriceType", c => c.Int());
            AddColumn("dbo.Groups", "PriceType", c => c.Int());
            AddColumn("dbo.Categories", "PriceType", c => c.Int());
            DropColumn("dbo.Categories", "PriceCalculationType");
        }
    }
}
