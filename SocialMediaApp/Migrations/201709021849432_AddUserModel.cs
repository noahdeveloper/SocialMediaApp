namespace SocialMediaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfimPassword = c.String(nullable: false),
                        IPAddressCreatedAccountDevice = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserAccounts");
        }
    }
}
