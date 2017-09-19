namespace Petrovich.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupBasePriceAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "BasePrice", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "BasePrice");
        }
    }
}
