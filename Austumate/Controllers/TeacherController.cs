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

namespace Austumate.Controllers
{
    [Authorize]
    public class TeacherController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Index()
        {
            var teachers = db.Teachers.Include(t => t.College).Include(t => t.Profile);
            return View(await teachers.ToListAsync());
        }
        
        public async Task<ActionResult> Teachers()
        {
            var teachers = db.Teachers.Include(t => t.College).Include(t => t.Profile);
            return View(await teachers.ToListAsync());
        }

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherModel teacherModel = await db.Teachers.FindAsync(id);
            if (teacherModel == null)
            {
                return HttpNotFound();
            }
            return View(teacherModel);
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult Create()
        {
            ViewBag.CollegeID = new SelectList(db.Colleges, "CollegeID", "CollegeName");
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProfileID,TeacherID,TeacherName,CollegeID,JoinDate,Mailbox,PhoneNumber")] TeacherModel teacherModel)
        {
            teacherModel.ProfileID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacherModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CollegeID = new SelectList(db.Colleges, "CollegeID", "CollegeName", teacherModel.CollegeID);
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", teacherModel.ProfileID);
            return View(teacherModel);
        }

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherModel teacherModel = await db.Teachers.FindAsync(id);
            if (teacherModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CollegeID = new SelectList(db.Colleges, "CollegeID", "CollegeName", teacherModel.CollegeID);
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", teacherModel.ProfileID);
            return View(teacherModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProfileID,TeacherID,TeacherName,CollegeID,JoinDate,Mailbox,PhoneNumber")] TeacherModel teacherModel)
        {
            teacherModel.ProfileID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(teacherModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CollegeID = new SelectList(db.Colleges, "CollegeID", "CollegeName", teacherModel.CollegeID);
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", teacherModel.ProfileID);
            return View(teacherModel);
        }

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherModel teacherModel = await db.Teachers.FindAsync(id);
            if (teacherModel == null)
            {
                return HttpNotFound();
            }
            return View(teacherModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            TeacherModel teacherModel = await db.Teachers.FindAsync(id);
            db.Teachers.Remove(teacherModel);
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
