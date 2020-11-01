namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assigncourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignCourses",
                c => new
                    {
                        AssignCourseID = c.String(nullable: false, maxLength: 200),
                        CourseID = c.String(nullable: false, maxLength: 50),
                        TeacherID = c.String(nullable: false, maxLength: 128),
                        CourseName = c.String(nullable: false, maxLength: 150),
                        AttendanceRate = c.Int(nullable: false),
                        LabRate = c.Int(nullable: false),
                        HomeworkRate = c.Int(nullable: false),
                        FinalExamRate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssignCourseID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherID, cascadeDelete: false)
                .Index(t => t.CourseID)
                .Index(t => t.TeacherID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignCourses", "TeacherID", "dbo.Teachers");
            DropForeignKey("dbo.AssignCourses", "CourseID", "dbo.Courses");
            DropIndex("dbo.AssignCourses", new[] { "TeacherID" });
            DropIndex("dbo.AssignCourses", new[] { "CourseID" });
            DropTable("dbo.AssignCourses");
        }
    }
}
