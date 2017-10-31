namespace SocialMediaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_UderInformation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInformations",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInformations");
        }
    }
}
