namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attendancescore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttendanceScores",
                c => new
                    {
                        EnrollStudentID = c.String(nullable: false, maxLength: 400),
                        CourseID = c.String(nullable: false, maxLength: 50),
                        TeacherID = c.String(nullable: false, maxLength: 128),
                        StudentID = c.String(nullable: false, maxLength: 128),
                        SemesterID = c.String(nullable: false, maxLength: 20),
                        Score = c.Single(nullable: false),
                        Remark = c.String(maxLength: 256),
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
            DropForeignKey("dbo.AttendanceScores", "TeacherID", "dbo.Teachers");
            DropForeignKey("dbo.AttendanceScores", "StudentID", "dbo.Students");
            DropForeignKey("dbo.AttendanceScores", "SemesterID", "dbo.Semesters");
            DropForeignKey("dbo.AttendanceScores", "CourseID", "dbo.Courses");
            DropIndex("dbo.AttendanceScores", new[] { "SemesterID" });
            DropIndex("dbo.AttendanceScores", new[] { "StudentID" });
            DropIndex("dbo.AttendanceScores", new[] { "TeacherID" });
            DropIndex("dbo.AttendanceScores", new[] { "CourseID" });
            DropTable("dbo.AttendanceScores");
        }
    }
}
