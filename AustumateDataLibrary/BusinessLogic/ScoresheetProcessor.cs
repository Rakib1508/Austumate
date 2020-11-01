using AustumateDataLibrary.DataAccess;
using AustumateDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustumateDataLibrary.BusinessLogic
{
    public static class ScoresheetProcessor
    {
        public static List<CourseViewModel> GetCourseTemplate()
        {
            string sql = @"SELECT * from dbo.AssignCourses;";
            return SqlDataAccess.LoadData<CourseViewModel>(sql);
        }

        public static List<ScoresheetViewModel> GetScoresheet(string id)
        {
            string sql = @"SELECT * from dbo.Scoresheets WHERE EnrollStudentID = '" + id + "';";
            return SqlDataAccess.LoadData<ScoresheetViewModel>(sql);
        }

        public static int UpdateAttendanceScore(string scoreID, string courseID, string teacherID, float score)
        {
            var list = GetCourseTemplate();
            foreach (var row in list)
            {
                if (row.AssignCourseID == courseID + " " + teacherID)
                    score = score * (row.AttendanceRate / (float)100);
            }
            ScoreModel data = new ScoreModel
            {
                ScoreID = scoreID,
                Score = score
            };
            string sql = @"UPDATE dbo.Scoresheets SET AttendanceScore = @Score,
                            TotalScore = @Score + LabScore + HomeworkScore + FinalExamScore
                            WHERE EnrollStudentID = @ScoreID";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateLabScore(string scoreID, string courseID, string teacherID, float score)
        {
            var list = GetCourseTemplate();
            foreach (var row in list)
            {
                if (row.AssignCourseID == courseID + " " + teacherID)
                    score = score * (row.LabRate / (float)100);
            }
            ScoreModel data = new ScoreModel
            {
                ScoreID = scoreID,
                Score = score
            };
            string sql = @"UPDATE dbo.Scoresheets SET LabScore = @Score,
                            TotalScore = AttendanceScore + @Score + HomeworkScore + FinalExamScore
                            WHERE EnrollStudentID = @ScoreID";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateHomeworkScore(string scoreID, string courseID, string teacherID, float score)
        {
            var list = GetCourseTemplate();
            foreach (var row in list)
            {
                if (row.AssignCourseID == courseID + " " + teacherID)
                    score = score * (row.HomeworkRate / (float)100);
            }
            ScoreModel data = new ScoreModel
            {
                ScoreID = scoreID,
                Score = score
            };
            string sql = @"UPDATE dbo.Scoresheets SET HomeworkScore = @Score,
                            TotalScore = AttendanceScore + LabScore + @Score + FinalExamScore
                            WHERE EnrollStudentID = @ScoreID";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateFinalExamScore(string scoreID, string courseID, string teacherID, float score)
        {
            var list = GetCourseTemplate();
            foreach (var row in list)
            {
                if (row.AssignCourseID == courseID + " " + teacherID)
                    score = score * (row.FinalExamRate / (float)100);
            }
            ScoreModel data = new ScoreModel
            {
                ScoreID = scoreID,
                Score = score
            };
            string sql = @"UPDATE dbo.Scoresheets SET FinalExamScore = @Score,
                            TotalScore = AttendanceScore + LabScore + HomeworkScore + @Score
                            WHERE EnrollStudentID = @ScoreID";
            return SqlDataAccess.SaveData(sql, data);
        }

        public static int UpdateGrade(string id, string grade)
        {
            var list = GetScoresheet(id);
            if (list[0].TotalScore >= 90)
                grade = "A";
            else if (list[0].TotalScore >= 80)
                grade = "B";
            else if (list[0].TotalScore >= 70)
                grade = "C";
            else if (list[0].TotalScore >= 60)
                grade = "D";
            else
                grade = "F";
            ScoresheetViewModel data = new ScoresheetViewModel
            {
                ScoreID = id,
                Grade = grade
            };
            string sql = @"UPDATE dbo.Scoresheets SET Grade = @Grade
                            WHERE EnrollStudentID = @ScoreID";
            return SqlDataAccess.SaveData(sql, data);
        }
    }
}