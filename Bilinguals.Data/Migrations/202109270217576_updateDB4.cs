namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDB4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "FavoriteUser", c => c.String());
            AddColumn("dbo.Sentences", "FavoriteUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sentences", "FavoriteUser");
            DropColumn("dbo.Comments", "FavoriteUser");
        }
    }
}
