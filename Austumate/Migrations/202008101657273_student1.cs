namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class student1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Students", "ProfileID");
            AddForeignKey("dbo.Students", "ProfileID", "dbo.UserProfiles", "ProfileID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "ProfileID", "dbo.UserProfiles");
            DropIndex("dbo.Students", new[] { "ProfileID" });
        }
    }
}
