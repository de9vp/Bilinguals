namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        UserId = c.String(maxLength: 128),
                        TimeStamp = c.DateTime(nullable: false),
                        DialogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateCreated = c.DateTime(),
                        UserId = c.String(maxLength: 128),
                        StrDialogId = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Dialogs", "Reviews", c => c.Int(nullable: false));
            DropColumn("dbo.Sentences", "UserId");
            DropColumn("dbo.UserDialogs", "Reviews");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserDialogs", "Reviews", c => c.Int(nullable: false));
            AddColumn("dbo.Sentences", "UserId", c => c.String());
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropColumn("dbo.Dialogs", "Reviews");
            DropTable("dbo.Groups");
            DropTable("dbo.Comments");
        }
    }
}
