namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDBfinal3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserSentences", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserSentences", new[] { "UserId" });
            AlterColumn("dbo.UserSentences", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UserSentences", "UserId");
            AddForeignKey("dbo.UserSentences", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSentences", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserSentences", new[] { "UserId" });
            AlterColumn("dbo.UserSentences", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserSentences", "UserId");
            AddForeignKey("dbo.UserSentences", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
