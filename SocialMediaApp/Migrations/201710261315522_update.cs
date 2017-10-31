namespace SocialMediaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.TestModels");
            DropTable("dbo.UsersAccountInformations");
        }
        
        public override void Down()
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
            
            CreateTable(
                "dbo.TestModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        test = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
    }
}
