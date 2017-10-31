namespace SocialMediaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditToUserAccount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserAccounts", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.UserAccounts", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.UserAccounts", "ConfimPassword", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAccounts", "ConfimPassword", c => c.String());
            AlterColumn("dbo.UserAccounts", "Password", c => c.String());
            AlterColumn("dbo.UserAccounts", "UserName", c => c.String());
        }
    }
}
