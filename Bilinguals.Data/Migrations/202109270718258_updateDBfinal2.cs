namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDBfinal2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserDialogs", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserDialogs", new[] { "UserId" });
            AlterColumn("dbo.UserDialogs", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UserDialogs", "UserId");
            AddForeignKey("dbo.UserDialogs", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDialogs", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserDialogs", new[] { "UserId" });
            AlterColumn("dbo.UserDialogs", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserDialogs", "UserId");
            AddForeignKey("dbo.UserDialogs", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
