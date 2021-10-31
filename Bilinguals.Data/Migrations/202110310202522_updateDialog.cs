namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDialog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dialogs", "Subcribers", c => c.Int(nullable: false));
            DropColumn("dbo.Dialogs", "Reviews");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dialogs", "Reviews", c => c.Int(nullable: false));
            DropColumn("dbo.Dialogs", "Subcribers");
        }
    }
}
