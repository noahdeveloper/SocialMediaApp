namespace SocialMediaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UserAccountInformation_Model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsersAccountInformations",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Age = c.Int(nullable: false),
                        FavoriteQuote = c.String(),
                        ProfilePic = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UsersAccountInformations");
        }
    }
}
