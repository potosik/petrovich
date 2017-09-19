namespace Petrovich.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryBasePriceAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "BasePrice", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "BasePrice");
        }
    }
}
