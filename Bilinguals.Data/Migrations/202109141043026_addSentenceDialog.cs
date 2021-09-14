namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSentenceDialog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dialogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sentences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnText = c.String(),
                        ViText = c.String(),
                        DialogId = c.Int(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dialogs", t => t.DialogId, cascadeDelete: true)
                .Index(t => t.DialogId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sentences", "DialogId", "dbo.Dialogs");
            DropIndex("dbo.Sentences", new[] { "DialogId" });
            DropTable("dbo.Sentences");
            DropTable("dbo.Dialogs");
        }
    }
}
