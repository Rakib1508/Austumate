using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Austumate.Models;
using Microsoft.AspNet.Identity;
using static AustumateDataLibrary.BusinessLogic.ScoreProcessor;

namespace Austumate.Controllers
{
    [Authorize]
    public class EnrollStudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Student")]
        public async Task<ActionResult> Index()
        {
            var enrollStudents = db.EnrollStudents.Include(e => e.ActiveCourses).Include(e => e.Course).Include(e => e.Semester).Include(e => e.Student).Include(e => e.Teacher);
            return View(await enrollStudents.ToListAsync());
        }

        [Authorize(Roles = "Registrar, Administrator")]
        public async Task<ActionResult> StudentList()
        {
            var enrollStudents = db.EnrollStudents.Include(e => e.ActiveCourses).Include(e => e.Course).Include(e => e.Semester).Include(e => e.Student).Include(e => e.Teacher);
            return View(await enrollStudents.ToListAsync());
        }

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Namelist()
        {
            var enrollStudents = db.EnrollStudents.Include(e => e.ActiveCourses).Include(e => e.Course).Include(e => e.Semester).Include(e => e.Student).Include(e => e.Teacher);
            return View(await enrollStudents.ToListAsync());
        }

        [Authorize(Roles = "Student")]
        public ActionResult Create()
        {
            ViewBag.ActiveCourseID = new SelectList(db.AssignCourses, "AssignCourseID", "CourseName");
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Session");
            ViewBag.StudentID = new SelectList(db.Students, "ProfileID", "StudentName");
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EnrollStudentID,ActiveCourseID,CourseID,TeacherID,StudentID,SemesterID")] EnrollStudentModel enrollStudentModel)
        {
            enrollStudentModel.StudentID = User.Identity.GetUserId();
            enrollStudentModel.EnrollStudentID = enrollStudentModel.ActiveCourseID + " " +
                                                 enrollStudentModel.StudentID + " " +
                                                 enrollStudentModel.SemesterID;
            string temp = enrollStudentModel.ActiveCourseID;
            string[] list = temp.Split(' ');
            enrollStudentModel.CourseID = list[0];
            enrollStudentModel.TeacherID = list[1];
            if (ModelState.IsValid)
            {
                db.EnrollStudents.Add(enrollStudentModel);
                await db.SaveChangesAsync();
                int CreateAttendanceTable = InitializeAttendanceTable(enrollStudentModel.EnrollStudentID,
                                                                        enrollStudentModel.CourseID,
                                                                        enrollStudentModel.TeacherID,
                                                                        enrollStudentModel.StudentID,
                                                                        enrollStudentModel.SemesterID,
                                                                        0, "");
                int CreateLabTable = InitializeLabTable(enrollStudentModel.EnrollStudentID,
                                                                        enrollStudentModel.CourseID,
                                                                        enrollStudentModel.TeacherID,
                                                                        enrollStudentModel.StudentID,
                                                                        enrollStudentModel.SemesterID,
                                                                        0, "");
                int CreateHomeworkTable = InitializeHomeworkTable(enrollStudentModel.EnrollStudentID,
                                                                        enrollStudentModel.CourseID,
                                                                        enrollStudentModel.TeacherID,
                                                                        enrollStudentModel.StudentID,
                                                                        enrollStudentModel.SemesterID,
                                                                        0, "");
                int CreateFinalExamTable = InitializeFinalExamTable(enrollStudentModel.EnrollStudentID,
                                                                        enrollStudentModel.CourseID,
                                                                        enrollStudentModel.TeacherID,
                                                                        enrollStudentModel.StudentID,
                                                                        enrollStudentModel.SemesterID,
                                                                        0, "");
                int CreateScoresheetTable = InitializeScoresheetTable(enrollStudentModel.EnrollStudentID,
                                                                        enrollStudentModel.CourseID,
                                                                        enrollStudentModel.TeacherID,
                                                                        enrollStudentModel.StudentID,
                                                                        enrollStudentModel.SemesterID,
                                                                        0, 0, 0, 0, 0, "", "");
                return RedirectToAction("Index");
            }

            ViewBag.ActiveCourseID = new SelectList(db.AssignCourses, "AssignCourseID", "CourseName", enrollStudentModel.ActiveCourseID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", enrollStudentModel.CourseID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Session", enrollStudentModel.SemesterID);
            ViewBag.StudentID = new SelectList(db.Students, "ProfileID", "StudentName", enrollStudentModel.StudentID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName", enrollStudentModel.TeacherID);
            return View(enrollStudentModel);
        }

        [Authorize(Roles = "Registrar, Administrator")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EnrollStudentModel enrollStudentModel = await db.EnrollStudents.FindAsync(id);
            if (enrollStudentModel == null)
            {
                return HttpNotFound();
            }
            return View(enrollStudentModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            EnrollStudentModel enrollStudentModel = await db.EnrollStudents.FindAsync(id);
            db.EnrollStudents.Remove(enrollStudentModel);
            await db.SaveChangesAsync();
            int AttendanceRecordDeleted = DeleteAttendanceRecord(id);
            int LabRecordDeleted = DeleteLabRecord(id);
            int HomeworkRecordDeleted = DeleteHomeworkRecord(id);
            int FinalExamRecordDeleted = DeleteFinalExamRecord(id);
            int ScoresheetRecordDeleted = DeleteScoresheetRecord(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
