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

namespace Austumate.Controllers
{
    [Authorize]
    public class ScoresheetController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Index()
        {
            var scoresheets = db.Scoresheets.Include(s => s.Course).Include(s => s.Semester).Include(s => s.Student).Include(s => s.Teacher);
            return View(await scoresheets.ToListAsync());
        }

        [Authorize(Roles = "Student")]
        public async Task<ActionResult> MyScoresheet()
        {
            var scoresheets = db.Scoresheets.Include(s => s.Course).Include(s => s.Semester).Include(s => s.Student).Include(s => s.Teacher);
            return View(await scoresheets.ToListAsync());
        }

        [Authorize(Roles = "Registrar, Administrator")]
        public async Task<ActionResult> Resultbook()
        {
            var scoresheets = db.Scoresheets.Include(s => s.Course).Include(s => s.Semester).Include(s => s.Student).Include(s => s.Teacher);
            return View(await scoresheets.ToListAsync());
        }

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScoresheetModel scoresheetModel = await db.Scoresheets.FindAsync(id);
            if (scoresheetModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", scoresheetModel.CourseID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Session", scoresheetModel.SemesterID);
            ViewBag.StudentID = new SelectList(db.Students, "ProfileID", "StudentName", scoresheetModel.StudentID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName", scoresheetModel.TeacherID);
            return View(scoresheetModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnrollStudentID,CourseID,TeacherID,StudentID,SemesterID,AttendanceScore,LabScore,HomeworkScore,FinalExamScore,TotalScore,Grade,Remark")] ScoresheetModel scoresheetModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scoresheetModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", scoresheetModel.CourseID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Session", scoresheetModel.SemesterID);
            ViewBag.StudentID = new SelectList(db.Students, "ProfileID", "StudentName", scoresheetModel.StudentID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName", scoresheetModel.TeacherID);
            return View(scoresheetModel);
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
