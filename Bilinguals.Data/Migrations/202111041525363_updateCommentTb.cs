namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCommentTb : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comments", "FavoriteUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "FavoriteUser", c => c.String());
        }
    }
}
