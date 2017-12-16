namespace SocialMediaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateToConversationContent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConversationContents", "FromOwner", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConversationContents", "FromOwner");
        }
    }
}
