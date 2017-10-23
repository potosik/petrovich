namespace Petrovich.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupInventoryNumberAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "InventoryPart", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "InventoryPart");
        }
    }
}
