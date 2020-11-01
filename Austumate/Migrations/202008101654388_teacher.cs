namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teacher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        ProfileID = c.String(nullable: false, maxLength: 128),
                        TeacherID = c.String(nullable: false, maxLength: 50),
                        TeacherName = c.String(nullable: false, maxLength: 200),
                        CollegeID = c.String(nullable: false, maxLength: 50),
                        JoinDate = c.DateTime(nullable: false),
                        Mailbox = c.String(nullable: false, maxLength: 255),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ProfileID)
                .ForeignKey("dbo.Colleges", t => t.CollegeID, cascadeDelete: true)
                .Index(t => t.CollegeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "CollegeID", "dbo.Colleges");
            DropIndex("dbo.Teachers", new[] { "CollegeID" });
            DropTable("dbo.Teachers");
        }
    }
}
