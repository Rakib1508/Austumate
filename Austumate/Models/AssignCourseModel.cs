using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Austumate.Models
{
    [Table("dbo.AssignCourses")]
    public class AssignCourseModel
    {
        [Key]
        [StringLength(200)]
        public string AssignCourseID { get; set; }

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

        [Required]
        [StringLength(150)]
        [Display(Name = "Course Acronym")]
        public string CourseName { get; set; }

        [Display(Name = "Attendance Score (%)")]
        [Range(0, 100, ErrorMessage = "Must be within 0 and 100")]
        public int AttendanceRate { get; set; }

        [Display(Name = "Lab Score (%)")]
        [Range(0, 100, ErrorMessage = "Must be within 0 and 100")]
        public int LabRate { get; set; }

        [Display(Name = "Homework Score (%)")]
        [Range(0, 100, ErrorMessage = "Must be within 0 and 100")]
        public int HomeworkRate { get; set; }

        [Display(Name = "Final Exam Score (%)")]
        [Range(0, 100, ErrorMessage = "Must be within 0 and 100")]
        public int FinalExamRate { get; set; }
    }
}