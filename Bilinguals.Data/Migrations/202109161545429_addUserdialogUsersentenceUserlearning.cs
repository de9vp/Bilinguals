namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserdialogUsersentenceUserlearning : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDialogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Reviews = c.Int(nullable: false),
                        LearningProgress = c.String(),
                        Featured = c.Boolean(nullable: false),
                        Mastered = c.Boolean(nullable: false),
                        IsLearning = c.Boolean(nullable: false),
                        DialogId = c.Int(),
                        UserId = c.String(maxLength: 128),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dialogs", t => t.DialogId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.DialogId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserSentences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SentenceId = c.Int(nullable: false),
                        UserDialogId = c.Int(),
                        Reviews = c.Int(nullable: false),
                        DifficultyLevel = c.Int(nullable: false),
                        Featured = c.Boolean(nullable: false),
                        Mastered = c.Boolean(nullable: false),
                        DateFeatured = c.DateTime(),
                        Note = c.String(),
                        UserId = c.String(maxLength: 128),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sentences", t => t.SentenceId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.UserDialogs", t => t.UserDialogId)
                .Index(t => t.SentenceId)
                .Index(t => t.UserDialogId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLearnings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LearningUrl = c.String(),
                        LearningData = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Dialogs", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLearnings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSentences", "UserDialogId", "dbo.UserDialogs");
            DropForeignKey("dbo.UserSentences", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSentences", "SentenceId", "dbo.Sentences");
            DropForeignKey("dbo.UserDialogs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserDialogs", "DialogId", "dbo.Dialogs");
            DropIndex("dbo.UserLearnings", new[] { "UserId" });
            DropIndex("dbo.UserSentences", new[] { "UserId" });
            DropIndex("dbo.UserSentences", new[] { "UserDialogId" });
            DropIndex("dbo.UserSentences", new[] { "SentenceId" });
            DropIndex("dbo.UserDialogs", new[] { "UserId" });
            DropIndex("dbo.UserDialogs", new[] { "DialogId" });
            DropColumn("dbo.Dialogs", "Description");
            DropTable("dbo.UserLearnings");
            DropTable("dbo.UserSentences");
            DropTable("dbo.UserDialogs");
        }
    }
}
