namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class registrar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Registrars",
                c => new
                    {
                        ProfileID = c.String(nullable: false, maxLength: 128),
                        RegistrarID = c.String(nullable: false, maxLength: 50),
                        RegistrarName = c.String(nullable: false, maxLength: 200),
                        JoinDate = c.DateTime(nullable: false),
                        Mailbox = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ProfileID)
                .ForeignKey("dbo.UserProfiles", t => t.ProfileID)
                .Index(t => t.ProfileID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Registrars", "ProfileID", "dbo.UserProfiles");
            DropIndex("dbo.Registrars", new[] { "ProfileID" });
            DropTable("dbo.Registrars");
        }
    }
}
