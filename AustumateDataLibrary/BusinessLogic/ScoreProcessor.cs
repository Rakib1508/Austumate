using AustumateDataLibrary.DataAccess;
using AustumateDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustumateDataLibrary.BusinessLogic
{
    public static class ScoreProcessor
    {
        public static int InitializeAttendanceTable(string scoreID, string courseID, string teacherID,
                                            string studentID, string semesterID, float score, string remark)
        {
            ScoreModel data = new ScoreModel
            {
                ScoreID = scoreID,
                CourseID = courseID,
                TeacherID = teacherID,
                StudentID = studentID,
                SemesterID = semesterID,
                Score = score,
                Remark = remark
            };
            string sql = @"INSERT into dbo.AttendanceScores (EnrollStudentID, CourseID, TeacherID,
                                            StudentID, SemesterID, Score, Remark)
                                            values (@ScoreID, @CourseID, @TeacherID,
                                            @StudentID, @SemesterID, @Score, @Remark);";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int DeleteAttendanceRecord(string id)
        {
            string sql = "DELETE from dbo.AttendanceScores WHERE EnrollStudentID = '" + id + "';";
            return SqlDataAccess.RemoveData(sql);
        }

        public static int InitializeLabTable(string scoreID, string courseID, string teacherID,
                                            string studentID, string semesterID, float score, string remark)
        {
            ScoreModel data = new ScoreModel
            {
                ScoreID = scoreID,
                CourseID = courseID,
                TeacherID = teacherID,
                StudentID = studentID,
                SemesterID = semesterID,
                Score = score,
                Remark = remark
            };
            string sql = @"INSERT into dbo.LabScores (EnrollStudentID, CourseID, TeacherID,
                                            StudentID, SemesterID, Score, Remark)
                                            values (@ScoreID, @CourseID, @TeacherID,
                                            @StudentID, @SemesterID, @Score, @Remark);";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int DeleteLabRecord(string id)
        {
            string sql = "DELETE from dbo.LabScores WHERE EnrollStudentID = '" + id + "';";
            return SqlDataAccess.RemoveData(sql);
        }

        public static int InitializeHomeworkTable(string scoreID, string courseID, string teacherID,
                                            string studentID, string semesterID, float score, string remark)
        {
            ScoreModel data = new ScoreModel
            {
                ScoreID = scoreID,
                CourseID = courseID,
                TeacherID = teacherID,
                StudentID = studentID,
                SemesterID = semesterID,
                Score = score,
                Remark = remark
            };
            string sql = @"INSERT into dbo.HomeworkScores (EnrollStudentID, CourseID, TeacherID,
                                            StudentID, SemesterID, Score, Remark)
                                            values (@ScoreID, @CourseID, @TeacherID,
                                            @StudentID, @SemesterID, @Score, @Remark);";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int DeleteHomeworkRecord(string id)
        {
            string sql = "DELETE from dbo.HomeworkScores WHERE EnrollStudentID = '" + id + "';";
            return SqlDataAccess.RemoveData(sql);
        }

        public static int InitializeFinalExamTable(string scoreID, string courseID, string teacherID,
                                            string studentID, string semesterID, float score, string remark)
        {
            ScoreModel data = new ScoreModel
            {
                ScoreID = scoreID,
                CourseID = courseID,
                TeacherID = teacherID,
                StudentID = studentID,
                SemesterID = semesterID,
                Score = score,
                Remark = remark
            };
            string sql = @"INSERT into dbo.FinalExamScores (EnrollStudentID, CourseID, TeacherID,
                                            StudentID, SemesterID, Score, Remark)
                                            values (@ScoreID, @CourseID, @TeacherID,
                                            @StudentID, @SemesterID, @Score, @Remark);";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int DeleteFinalExamRecord(string id)
        {
            string sql = "DELETE from dbo.FinalExamScores WHERE EnrollStudentID = '" + id + "';";
            return SqlDataAccess.RemoveData(sql);
        }

        public static int InitializeScoresheetTable(string scoreID, string courseID, string teacherID,
                                            string studentID, string semesterID, float attendanceScore,
                                            float labScore, float homeworkScore, float finalExamScore,
                                            float totalScore, string grade, string remark)
        {
            ScoresheetViewModel data = new ScoresheetViewModel
            {
                ScoreID = scoreID,
                CourseID = courseID,
                TeacherID = teacherID,
                StudentID = studentID,
                SemesterID = semesterID,
                AttendanceScore = attendanceScore,
                LabScore = labScore,
                HomeworkScore = homeworkScore,
                FinalExamScore = finalExamScore,
                TotalScore = totalScore,
                Grade = grade,
                Remark = remark
            };
            string sql = @"INSERT into dbo.Scoresheets (EnrollStudentID, CourseID, TeacherID,
                                            StudentID, SemesterID, AttendanceScore, LabScore,
                                            HomeworkScore, FinalExamScore, TotalScore, Grade, Remark)
                                            values (@ScoreID, @CourseID, @TeacherID,
                                            @StudentID, @SemesterID, @AttendanceScore, @LabScore,
                                            @HomeworkScore, @FinalExamScore, @TotalScore, @Grade, @Remark);";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int DeleteScoresheetRecord(string id)
        {
            string sql = "DELETE from dbo.Scoresheets WHERE EnrollStudentID = '" + id + "';";
            return SqlDataAccess.RemoveData(sql);
        }
    }
}