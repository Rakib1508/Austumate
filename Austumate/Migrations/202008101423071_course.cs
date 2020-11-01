namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class course : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.String(nullable: false, maxLength: 50),
                        CourseName = c.String(nullable: false, maxLength: 100),
                        CollegeName = c.String(nullable: false, maxLength: 50),
                        Credit = c.Int(nullable: false),
                        ClassHours = c.Int(nullable: false),
                        RequiredCourse = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.CourseID)
                .ForeignKey("dbo.Colleges", t => t.CollegeName, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.RequiredCourse)
                .Index(t => t.CollegeName)
                .Index(t => t.RequiredCourse);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "RequiredCourse", "dbo.Courses");
            DropForeignKey("dbo.Courses", "CollegeName", "dbo.Colleges");
            DropIndex("dbo.Courses", new[] { "RequiredCourse" });
            DropIndex("dbo.Courses", new[] { "CollegeName" });
            DropTable("dbo.Courses");
        }
    }
}
