namespace SocialMediaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Deleted_Test : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Tests");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
