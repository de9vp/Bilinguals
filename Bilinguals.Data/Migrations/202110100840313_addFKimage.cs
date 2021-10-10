namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFKimage : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.AspNetUsers", "ImageId", "dbo.Images", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ImageId", "dbo.Images");
        }
    }
}
