using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustumateDataLibrary.Models
{
    public class ScoreModel
    {
        public string ScoreID { get; set; }
        public string CourseID { get; set; }
        public string TeacherID { get; set; }
        public string StudentID { get; set; }
        public string SemesterID { get; set; }
        public float Score { get; set; }
        public string Remark { get; set; }
    }
}