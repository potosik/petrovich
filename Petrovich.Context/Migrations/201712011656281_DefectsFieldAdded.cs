namespace Petrovich.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DefectsFieldAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Defects", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Defects");
        }
    }
}
