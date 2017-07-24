namespace Petrovich.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchasingProductInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PurchaseYear", c => c.Int());
            AddColumn("dbo.Products", "PurchaseMonth", c => c.Int());
            CreateIndex("dbo.Products", "PurchaseYear");
            CreateIndex("dbo.Products", "PurchaseMonth");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", new[] { "PurchaseMonth" });
            DropIndex("dbo.Products", new[] { "PurchaseYear" });
            DropColumn("dbo.Products", "PurchaseMonth");
            DropColumn("dbo.Products", "PurchaseYear");
        }
    }
}
