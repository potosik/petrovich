namespace Petrovich.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        BranchId = c.Guid(nullable: false, identity: true),
                        Title = c.String(),
                        InventoryPart = c.String(maxLength: 2),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.BranchId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Guid(nullable: false, identity: true),
                        Title = c.String(),
                        InventoryPart = c.Int(nullable: false),
                        BranchId = c.Guid(nullable: false),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Guid(nullable: false, identity: true),
                        Title = c.String(),
                        CategoryId = c.Guid(nullable: false),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Guid(nullable: false, identity: true),
                        Title = c.String(),
                        InventoryPart = c.Int(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                        GroupId = c.Guid(),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.CategoryId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogId = c.Guid(nullable: false, identity: true),
                        CorrelationId = c.Guid(nullable: false),
                        Severity = c.Int(nullable: false),
                        Message = c.String(),
                        StackTrace = c.String(),
                        InnerExceptionMessage = c.String(),
                        CallStack = c.String(),
                        Created = c.DateTime(),
                        CreatedBy = c.String(),
                        Modified = c.DateTime(),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.LogId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Groups", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "BranchId", "dbo.Branches");
            DropIndex("dbo.Products", new[] { "GroupId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Groups", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "BranchId" });
            DropTable("dbo.Logs");
            DropTable("dbo.Products");
            DropTable("dbo.Groups");
            DropTable("dbo.Categories");
            DropTable("dbo.Branches");
        }
    }
}
