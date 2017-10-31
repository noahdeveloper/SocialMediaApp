namespace SocialMediaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_UserInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInformations", "ProfilePicName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInformations", "ProfilePicName");
        }
    }
}
