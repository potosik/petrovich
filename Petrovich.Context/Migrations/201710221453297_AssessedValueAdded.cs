namespace Petrovich.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssessedValueAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "AssessedValue", c => c.Double(nullable: false, defaultValue: 0f));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "AssessedValue");
        }
    }
}
