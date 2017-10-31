namespace SocialMediaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestBoolToTestModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TestModels", "test", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestModels", "test");
        }
    }
}
