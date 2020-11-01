namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class college : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colleges",
                c => new
                    {
                        CollegeID = c.String(nullable: false, maxLength: 50),
                        CollegeName = c.String(nullable: false, maxLength: 100),
                        CampusName = c.String(nullable: false, maxLength: 128),
                        CollegeMailbox = c.String(nullable: false, maxLength: 255),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.CollegeID)
                .ForeignKey("dbo.Campuses", t => t.CampusName, cascadeDelete: true)
                .Index(t => t.CampusName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Colleges", "CampusName", "dbo.Campuses");
            DropIndex("dbo.Colleges", new[] { "CampusName" });
            DropTable("dbo.Colleges");
        }
    }
}
