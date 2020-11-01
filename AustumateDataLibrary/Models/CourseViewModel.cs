using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AustumateDataLibrary.Models
{
    public class CourseViewModel
    {
        public string AssignCourseID { get; set; }
        public string CourseID { get; set; }
        public string TeacherID { get; set; }
        public string CourseName { get; set; }
        public int AttendanceRate { get; set; }
        public int LabRate { get; set; }
        public int HomeworkRate { get; set; }
        public int FinalExamRate { get; set; }
    }
}