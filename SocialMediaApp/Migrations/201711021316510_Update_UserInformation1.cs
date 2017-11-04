namespace SocialMediaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_UserInformation1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserInformations", "AccountUserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInformations", "AccountUserName", c => c.String());
        }
    }
}
