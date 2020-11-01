namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class administrator : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        ProfileID = c.String(nullable: false, maxLength: 128),
                        AdministratorID = c.String(nullable: false, maxLength: 50),
                        AdministratorName = c.String(nullable: false, maxLength: 200),
                        JoinDate = c.DateTime(nullable: false),
                        Mailbox = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ProfileID)
                .ForeignKey("dbo.UserProfiles", t => t.ProfileID)
                .Index(t => t.ProfileID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Administrators", "ProfileID", "dbo.UserProfiles");
            DropIndex("dbo.Administrators", new[] { "ProfileID" });
            DropTable("dbo.Administrators");
        }
    }
}
