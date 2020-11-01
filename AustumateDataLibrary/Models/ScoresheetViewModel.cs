using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustumateDataLibrary.Models
{
    public class ScoresheetViewModel
    {
        public string ScoreID { get; set; }
        public string CourseID { get; set; }
        public string TeacherID { get; set; }
        public string StudentID { get; set; }
        public string SemesterID { get; set; }
        public float AttendanceScore { get; set; }
        public float LabScore { get; set; }
        public float HomeworkScore { get; set; }
        public float FinalExamScore { get; set; }
        public float TotalScore { get; set; }
        public string Grade { get; set; }
        public string Remark { get; set; }
    }
}