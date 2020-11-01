namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class labscore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LabScores",
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
            DropForeignKey("dbo.LabScores", "TeacherID", "dbo.Teachers");
            DropForeignKey("dbo.LabScores", "StudentID", "dbo.Students");
            DropForeignKey("dbo.LabScores", "SemesterID", "dbo.Semesters");
            DropForeignKey("dbo.LabScores", "CourseID", "dbo.Courses");
            DropIndex("dbo.LabScores", new[] { "SemesterID" });
            DropIndex("dbo.LabScores", new[] { "StudentID" });
            DropIndex("dbo.LabScores", new[] { "TeacherID" });
            DropIndex("dbo.LabScores", new[] { "CourseID" });
            DropTable("dbo.LabScores");
        }
    }
}
