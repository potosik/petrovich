namespace Petrovich.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductImagesAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageDefault", c => c.String());
            AddColumn("dbo.Products", "ImageSmall", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageSmall");
            DropColumn("dbo.Products", "ImageDefault");
        }
    }
}
