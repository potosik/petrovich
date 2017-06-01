namespace Petrovich.Context.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        CorrelationId = c.Guid(nullable: false),
                        Severity = c.Int(nullable: false),
                        Message = c.String(),
                        StackTrace = c.String(),
                        InnerExceptionMessage = c.String(),
                    })
                .PrimaryKey(t => t.LogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logs");
        }
    }
}
