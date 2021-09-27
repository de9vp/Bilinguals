namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDBfinal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "StrSentenceId", c => c.String());
            AddColumn("dbo.Groups", "DateModified", c => c.DateTime());
            DropColumn("dbo.Groups", "StrDialogId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "StrDialogId", c => c.String());
            DropColumn("dbo.Groups", "DateModified");
            DropColumn("dbo.Groups", "StrSentenceId");
        }
    }
}
