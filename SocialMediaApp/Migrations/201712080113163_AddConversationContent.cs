namespace SocialMediaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConversationContent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConversationContents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ConversationName = c.String(),
                        SentFrom = c.String(),
                        Message = c.String(),
                        SentTo = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ConversationContents");
        }
    }
}
