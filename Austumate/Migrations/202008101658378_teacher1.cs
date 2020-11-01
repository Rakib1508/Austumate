namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teacher1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Teachers", "ProfileID");
            AddForeignKey("dbo.Teachers", "ProfileID", "dbo.UserProfiles", "ProfileID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "ProfileID", "dbo.UserProfiles");
            DropIndex("dbo.Teachers", new[] { "ProfileID" });
        }
    }
}
