namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sentences", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sentences", "UserId");
        }
    }
}
