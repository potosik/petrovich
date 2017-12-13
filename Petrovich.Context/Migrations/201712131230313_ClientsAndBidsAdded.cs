namespace Petrovich.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientsAndBidsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Guid(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(),
                        Registered = c.String(),
                        PassportId = c.String(nullable: false),
                        PassportData = c.String(),
                        PersonalId = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        PhonesJson = c.String(),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        BidId = c.Guid(nullable: false, identity: true),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                        Client_ClientId = c.Guid(),
                    })
                .PrimaryKey(t => t.BidId)
                .ForeignKey("dbo.Clients", t => t.Client_ClientId)
                .Index(t => t.Client_ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bids", "Client_ClientId", "dbo.Clients");
            DropIndex("dbo.Bids", new[] { "Client_ClientId" });
            DropTable("dbo.Bids");
            DropTable("dbo.Clients");
        }
    }
}
