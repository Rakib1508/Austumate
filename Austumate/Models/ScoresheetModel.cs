using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Austumate.Models
{
    [Table("dbo.Scoresheets")]
    public class ScoresheetModel
    {
        [Key]
        [StringLength(400)]
        public string EnrollStudentID { get; set; }

        [ForeignKey("CourseID")]
        public CourseModel Course { get; set; }
        [Required]
        [Display(Name = "Course")]
        public string CourseID { get; set; }

        [ForeignKey("TeacherID")]
        public TeacherModel Teacher { get; set; }
        [Required]
        [Display(Name = "Teacher")]
        public string TeacherID { get; set; }

        [ForeignKey("StudentID")]
        public StudentModel Student { get; set; }
        [Required]
        [Display(Name = "Student")]
        public string StudentID { get; set; }

        [ForeignKey("SemesterID")]
        public SemesterModel Semester { get; set; }
        [Required]
        [Display(Name = "Semester")]
        public string SemesterID { get; set; }

        [Display(Name = "Attendance")]
        public float AttendanceScore { get; set; }

        [Display(Name = "Lab")]
        public float LabScore { get; set; }

        [Display(Name = "Homework")]
        public float HomeworkScore { get; set; }

        [Display(Name = "Final Exam")]
        public float FinalExamScore { get; set; }
        
        [Display(Name = "Total Score")]
        public float TotalScore { get; set; }
        
        [Display(Name = "Grade")]
        public string Grade { get; set; }

        [Display(Name = "Comments")]
        [StringLength(512)]
        public string Remark { get; set; }
    }
}