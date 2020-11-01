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
    public class AssignCourseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Registrar, Administrator")]
        public async Task<ActionResult> Index()
        {
            var assignCourses = db.AssignCourses.Include(a => a.Course).Include(a => a.Teacher);
            return View(await assignCourses.ToListAsync());
        }

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> MyCourses()
        {
            var assignCourses = db.AssignCourses.Include(a => a.Course).Include(a => a.Teacher);
            return View(await assignCourses.ToListAsync());
        }

        [Authorize(Roles = "Student")]
        public async Task<ActionResult> AvailableCourses()
        {
            var assignCourses = db.AssignCourses.Include(a => a.Course).Include(a => a.Teacher);
            return View(await assignCourses.ToListAsync());
        }

        [Authorize(Roles = "Registrar, Administrator")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignCourseModel assignCourseModel = await db.AssignCourses.FindAsync(id);
            if (assignCourseModel == null)
            {
                return HttpNotFound();
            }
            return View(assignCourseModel);
        }

        [Authorize(Roles = "Registrar, Administrator")]
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AssignCourseID,CourseID,TeacherID,CourseName,AttendanceRate,LabRate,HomeworkRate,FinalExamRate")] AssignCourseModel assignCourseModel)
        {
            assignCourseModel.AssignCourseID = assignCourseModel.CourseID + " " + assignCourseModel.TeacherID;
            if (ModelState.IsValid)
            {
                db.AssignCourses.Add(assignCourseModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", assignCourseModel.CourseID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName", assignCourseModel.TeacherID);
            return View(assignCourseModel);
        }

        [Authorize(Roles = "Registrar, Administrator")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignCourseModel assignCourseModel = await db.AssignCourses.FindAsync(id);
            if (assignCourseModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", assignCourseModel.CourseID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName", assignCourseModel.TeacherID);
            return View(assignCourseModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AssignCourseID,CourseID,TeacherID,CourseName,AttendanceRate,LabRate,HomeworkRate,FinalExamRate")] AssignCourseModel assignCourseModel)
        {
            assignCourseModel.AssignCourseID = assignCourseModel.CourseID + " " + assignCourseModel.TeacherID;
            if (ModelState.IsValid)
            {
                db.Entry(assignCourseModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", assignCourseModel.CourseID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName", assignCourseModel.TeacherID);
            return View(assignCourseModel);
        }

        [Authorize(Roles = "Registrar, Administrator")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignCourseModel assignCourseModel = await db.AssignCourses.FindAsync(id);
            if (assignCourseModel == null)
            {
                return HttpNotFound();
            }
            return View(assignCourseModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            AssignCourseModel assignCourseModel = await db.AssignCourses.FindAsync(id);
            db.AssignCourses.Remove(assignCourseModel);
            await db.SaveChangesAsync();
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
