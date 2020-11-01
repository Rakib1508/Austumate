namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class avatar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileAvatars",
                c => new
                    {
                        ProfileID = c.String(nullable: false, maxLength: 128),
                        AvatarImage = c.String(),
                    })
                .PrimaryKey(t => t.ProfileID)
                .ForeignKey("dbo.UserProfiles", t => t.ProfileID)
                .Index(t => t.ProfileID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileAvatars", "ProfileID", "dbo.UserProfiles");
            DropIndex("dbo.ProfileAvatars", new[] { "ProfileID" });
            DropTable("dbo.ProfileAvatars");
        }
    }
}
