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
    public class CourseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Index()
        {
            var courses = db.Courses.Include(c => c.College).Include(c => c.Course);
            return View(await courses.ToListAsync());
        }

        public async Task<ActionResult> Courses()
        {
            var courses = db.Courses.Include(c => c.College).Include(c => c.Course);
            return View(await courses.ToListAsync());
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseModel courseModel = await db.Courses.FindAsync(id);
            if (courseModel == null)
            {
                return HttpNotFound();
            }
            return View(courseModel);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.CollegeName = new SelectList(db.Colleges, "CollegeID", "CollegeName");
            ViewBag.RequiredCourse = new SelectList(db.Courses, "CourseID", "CourseName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CourseID,CourseName,CollegeName,Credit,ClassHours,RequiredCourse")] CourseModel courseModel)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(courseModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CollegeName = new SelectList(db.Colleges, "CollegeID", "CollegeName", courseModel.CollegeName);
            ViewBag.RequiredCourse = new SelectList(db.Courses, "CourseID", "CourseName", courseModel.RequiredCourse);
            return View(courseModel);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseModel courseModel = await db.Courses.FindAsync(id);
            if (courseModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CollegeName = new SelectList(db.Colleges, "CollegeID", "CollegeName", courseModel.CollegeName);
            ViewBag.RequiredCourse = new SelectList(db.Courses, "CourseID", "CourseName", courseModel.RequiredCourse);
            return View(courseModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CourseID,CourseName,CollegeName,Credit,ClassHours,RequiredCourse")] CourseModel courseModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CollegeName = new SelectList(db.Colleges, "CollegeID", "CollegeName", courseModel.CollegeName);
            ViewBag.RequiredCourse = new SelectList(db.Courses, "CourseID", "CourseName", courseModel.RequiredCourse);
            return View(courseModel);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseModel courseModel = await db.Courses.FindAsync(id);
            if (courseModel == null)
            {
                return HttpNotFound();
            }
            return View(courseModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            CourseModel courseModel = await db.Courses.FindAsync(id);
            db.Courses.Remove(courseModel);
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
