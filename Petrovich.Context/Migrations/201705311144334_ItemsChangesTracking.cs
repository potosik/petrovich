namespace Petrovich.Context.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ItemsChangesTracking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "Created", c => c.DateTime());
            AddColumn("dbo.Logs", "CreatedBy", c => c.String());
            AddColumn("dbo.Logs", "Modified", c => c.DateTime());
            AddColumn("dbo.Logs", "ModifiedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logs", "ModifiedBy");
            DropColumn("dbo.Logs", "Modified");
            DropColumn("dbo.Logs", "CreatedBy");
            DropColumn("dbo.Logs", "Created");
        }
    }
}
