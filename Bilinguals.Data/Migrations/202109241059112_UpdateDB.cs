namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserLearnings", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserLearnings", new[] { "UserId" });
            AddColumn("dbo.Sentences", "UserId", c => c.String());
            AddColumn("dbo.UserDialogs", "Learned", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserDialogs", "DateLearned", c => c.DateTime());
            AddColumn("dbo.UserDialogs", "Note", c => c.String());
            DropColumn("dbo.UserSentences", "Reviews");
            DropColumn("dbo.UserSentences", "DifficultyLevel");
            DropColumn("dbo.UserSentences", "Featured");
            DropColumn("dbo.UserSentences", "Mastered");
            DropColumn("dbo.UserSentences", "DateFeatured");
            DropColumn("dbo.UserSentences", "Note");
            DropColumn("dbo.UserDialogs", "LearningProgress");
            DropColumn("dbo.UserDialogs", "Featured");
            DropColumn("dbo.UserDialogs", "Mastered");
            DropColumn("dbo.UserDialogs", "IsLearning");
            DropTable("dbo.UserLearnings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserLearnings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LearningUrl = c.String(),
                        LearningData = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserDialogs", "IsLearning", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserDialogs", "Mastered", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserDialogs", "Featured", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserDialogs", "LearningProgress", c => c.String());
            AddColumn("dbo.UserSentences", "Note", c => c.String());
            AddColumn("dbo.UserSentences", "DateFeatured", c => c.DateTime());
            AddColumn("dbo.UserSentences", "Mastered", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserSentences", "Featured", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserSentences", "DifficultyLevel", c => c.Int(nullable: false));
            AddColumn("dbo.UserSentences", "Reviews", c => c.Int(nullable: false));
            DropColumn("dbo.UserDialogs", "Note");
            DropColumn("dbo.UserDialogs", "DateLearned");
            DropColumn("dbo.UserDialogs", "Learned");
            DropColumn("dbo.Sentences", "UserId");
            CreateIndex("dbo.UserLearnings", "UserId");
            AddForeignKey("dbo.UserLearnings", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
