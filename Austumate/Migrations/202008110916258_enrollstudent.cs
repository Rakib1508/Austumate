namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enrollstudent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnrollStudents",
                c => new
                    {
                        EnrollStudentID = c.String(nullable: false, maxLength: 400),
                        ActiveCourseID = c.String(nullable: false, maxLength: 200),
                        CourseID = c.String(nullable: false, maxLength: 50),
                        TeacherID = c.String(nullable: false, maxLength: 128),
                        StudentID = c.String(nullable: false, maxLength: 128),
                        SemesterID = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.EnrollStudentID)
                .ForeignKey("dbo.AssignCourses", t => t.ActiveCourseID, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: false)
                .ForeignKey("dbo.Semesters", t => t.SemesterID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: false)
                .ForeignKey("dbo.Teachers", t => t.TeacherID, cascadeDelete: false)
                .Index(t => t.ActiveCourseID)
                .Index(t => t.CourseID)
                .Index(t => t.TeacherID)
                .Index(t => t.StudentID)
                .Index(t => t.SemesterID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EnrollStudents", "TeacherID", "dbo.Teachers");
            DropForeignKey("dbo.EnrollStudents", "StudentID", "dbo.Students");
            DropForeignKey("dbo.EnrollStudents", "SemesterID", "dbo.Semesters");
            DropForeignKey("dbo.EnrollStudents", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.EnrollStudents", "ActiveCourseID", "dbo.AssignCourses");
            DropIndex("dbo.EnrollStudents", new[] { "SemesterID" });
            DropIndex("dbo.EnrollStudents", new[] { "StudentID" });
            DropIndex("dbo.EnrollStudents", new[] { "TeacherID" });
            DropIndex("dbo.EnrollStudents", new[] { "CourseID" });
            DropIndex("dbo.EnrollStudents", new[] { "ActiveCourseID" });
            DropTable("dbo.EnrollStudents");
        }
    }
}
