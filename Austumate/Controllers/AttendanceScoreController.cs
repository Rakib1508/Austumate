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
using static AustumateDataLibrary.BusinessLogic.ScoresheetProcessor;

namespace Austumate.Controllers
{
    [Authorize(Roles = "Teacher, Registrar, Administrator")]
    public class AttendanceScoreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Index()
        {
            var attendanceScores = db.AttendanceScores.Include(a => a.Course).Include(a => a.Semester).Include(a => a.Student).Include(a => a.Teacher);
            return View(await attendanceScores.ToListAsync());
        }

        [Authorize(Roles = "Registrar, Administrator")]
        public async Task<ActionResult> Records()
        {
            var attendanceScores = db.AttendanceScores.Include(a => a.Course).Include(a => a.Semester).Include(a => a.Student).Include(a => a.Teacher);
            return View(await attendanceScores.ToListAsync());
        }

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttendanceScoreModel attendanceScoreModel = await db.AttendanceScores.FindAsync(id);
            if (attendanceScoreModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", attendanceScoreModel.CourseID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Session", attendanceScoreModel.SemesterID);
            ViewBag.StudentID = new SelectList(db.Students, "ProfileID", "StudentName", attendanceScoreModel.StudentID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName", attendanceScoreModel.TeacherID);
            return View(attendanceScoreModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnrollStudentID,CourseID,TeacherID,StudentID,SemesterID,Score,Remark")] AttendanceScoreModel attendanceScoreModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendanceScoreModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                int RecordStored = UpdateAttendanceScore(attendanceScoreModel.EnrollStudentID, attendanceScoreModel.CourseID, attendanceScoreModel.TeacherID, attendanceScoreModel.Score);
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", attendanceScoreModel.CourseID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Session", attendanceScoreModel.SemesterID);
            ViewBag.StudentID = new SelectList(db.Students, "ProfileID", "StudentName", attendanceScoreModel.StudentID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName", attendanceScoreModel.TeacherID);
            return View(attendanceScoreModel);
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
