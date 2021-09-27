namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserSentences", "GroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserSentences", "GroupId");
            AddForeignKey("dbo.UserSentences", "GroupId", "dbo.Groups", "Id", cascadeDelete: true);
            DropColumn("dbo.Groups", "StrSentenceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "StrSentenceId", c => c.String());
            DropForeignKey("dbo.UserSentences", "GroupId", "dbo.Groups");
            DropIndex("dbo.UserSentences", new[] { "GroupId" });
            DropColumn("dbo.UserSentences", "GroupId");
        }
    }
}
