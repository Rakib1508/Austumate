namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class homeworkscore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HomeworkScores",
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
            DropForeignKey("dbo.HomeworkScores", "TeacherID", "dbo.Teachers");
            DropForeignKey("dbo.HomeworkScores", "StudentID", "dbo.Students");
            DropForeignKey("dbo.HomeworkScores", "SemesterID", "dbo.Semesters");
            DropForeignKey("dbo.HomeworkScores", "CourseID", "dbo.Courses");
            DropIndex("dbo.HomeworkScores", new[] { "SemesterID" });
            DropIndex("dbo.HomeworkScores", new[] { "StudentID" });
            DropIndex("dbo.HomeworkScores", new[] { "TeacherID" });
            DropIndex("dbo.HomeworkScores", new[] { "CourseID" });
            DropTable("dbo.HomeworkScores");
        }
    }
}
