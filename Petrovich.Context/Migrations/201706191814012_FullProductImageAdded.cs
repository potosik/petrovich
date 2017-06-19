namespace Petrovich.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FullProductImageAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FullImages",
                c => new
                    {
                        FullImageId = c.Guid(nullable: false, identity: true),
                        Content = c.Binary(),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.FullImageId);
            
            AddColumn("dbo.Products", "FullImageId", c => c.Guid());
            CreateIndex("dbo.Products", "FullImageId");
            AddForeignKey("dbo.Products", "FullImageId", "dbo.FullImages", "FullImageId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "FullImageId", "dbo.FullImages");
            DropIndex("dbo.Products", new[] { "FullImageId" });
            DropColumn("dbo.Products", "FullImageId");
            DropTable("dbo.FullImages");
        }
    }
}
