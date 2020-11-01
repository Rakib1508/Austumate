using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Austumate.Models
{
    [Table("dbo.AttendanceScores")]
    public class AttendanceScoreModel
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

        [Display(Name = "Attendance Score")]
        [Range(0, 100, ErrorMessage = "Must be within 0~100")]
        public float Score { get; set; }

        [StringLength(256)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comments")]
        public string Remark { get; set; }
    }
}