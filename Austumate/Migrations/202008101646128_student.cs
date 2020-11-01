namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class student : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ProfileID = c.String(nullable: false, maxLength: 128),
                        StudentID = c.String(nullable: false, maxLength: 50),
                        StudentName = c.String(nullable: false, maxLength: 200),
                        MajorID = c.String(nullable: false, maxLength: 50),
                        ClassName = c.String(maxLength: 255),
                        StartDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProfileID)
                .ForeignKey("dbo.Majors", t => t.MajorID, cascadeDelete: true)
                .Index(t => t.MajorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "MajorID", "dbo.Majors");
            DropIndex("dbo.Students", new[] { "MajorID" });
            DropTable("dbo.Students");
        }
    }
}
