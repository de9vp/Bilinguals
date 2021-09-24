namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeUserIdAllowNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserDialogs", "DialogId", "dbo.Dialogs");
            DropIndex("dbo.UserDialogs", new[] { "DialogId" });
            AlterColumn("dbo.UserDialogs", "DialogId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserDialogs", "DialogId");
            AddForeignKey("dbo.UserDialogs", "DialogId", "dbo.Dialogs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDialogs", "DialogId", "dbo.Dialogs");
            DropIndex("dbo.UserDialogs", new[] { "DialogId" });
            AlterColumn("dbo.UserDialogs", "DialogId", c => c.Int());
            CreateIndex("dbo.UserDialogs", "DialogId");
            AddForeignKey("dbo.UserDialogs", "DialogId", "dbo.Dialogs", "Id");
        }
    }
}
