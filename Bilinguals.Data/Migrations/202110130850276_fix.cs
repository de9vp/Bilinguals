namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AspNetUsers", "ImageId");
            AddForeignKey("dbo.AspNetUsers", "ImageId", "dbo.Images", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ImageId", "dbo.Images");
            DropIndex("dbo.AspNetUsers", new[] { "ImageId" });
        }
    }
}
