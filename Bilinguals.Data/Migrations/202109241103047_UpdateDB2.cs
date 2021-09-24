namespace Bilinguals.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IrregularVerbs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Infinitive = c.String(),
                        SimplePast = c.String(),
                        PastParticiple = c.String(),
                        Mean = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserWords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        EnWord = c.String(),
                        Phonetically = c.String(),
                        ViWord = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserWords", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserWords", new[] { "UserId" });
            DropTable("dbo.UserWords");
            DropTable("dbo.IrregularVerbs");
        }
    }
}
