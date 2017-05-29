namespace Petrovich.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CallStackAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "CallStack", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logs", "CallStack");
        }
    }
}
