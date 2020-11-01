namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scoresheet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Scoresheets",
                c => new
                    {
                        EnrollStudentID = c.String(nullable: false, maxLength: 400),
                        CourseID = c.String(nullable: false, maxLength: 50),
                        TeacherID = c.String(nullable: false, maxLength: 128),
                        StudentID = c.String(nullable: false, maxLength: 128),
                        SemesterID = c.String(nullable: false, maxLength: 20),
                        AttendanceScore = c.Single(nullable: false),
                        LabScore = c.Single(nullable: false),
                        HomeworkScore = c.Single(nullable: false),
                        FinalExamScore = c.Single(nullable: false),
                        TotalScore = c.Single(nullable: false),
                        Grade = c.String(),
                        Remark = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => t.EnrollStudentID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.SemesterID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: false)
                .ForeignKey("dbo.Teachers", t => t.TeacherID, cascadeDelete: false)
                .Index(t => t.CourseID)
                .Index(t => t.TeacherID)
                .Index(t => t.StudentID)
                .Index(t => t.SemesterID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Scoresheets", "TeacherID", "dbo.Teachers");
            DropForeignKey("dbo.Scoresheets", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Scoresheets", "SemesterID", "dbo.Semesters");
            DropForeignKey("dbo.Scoresheets", "CourseID", "dbo.Courses");
            DropIndex("dbo.Scoresheets", new[] { "SemesterID" });
            DropIndex("dbo.Scoresheets", new[] { "StudentID" });
            DropIndex("dbo.Scoresheets", new[] { "TeacherID" });
            DropIndex("dbo.Scoresheets", new[] { "CourseID" });
            DropTable("dbo.Scoresheets");
        }
    }
}
