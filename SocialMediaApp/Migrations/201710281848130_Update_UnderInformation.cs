namespace SocialMediaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_UnderInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInformations", "AccountUserName", c => c.String());
            AddColumn("dbo.UserInformations", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.UserInformations", "FavoriteQuote", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInformations", "FavoriteQuote");
            DropColumn("dbo.UserInformations", "Age");
            DropColumn("dbo.UserInformations", "AccountUserName");
        }
    }
}
