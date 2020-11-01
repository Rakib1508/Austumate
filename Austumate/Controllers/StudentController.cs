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
    [Authorize(Roles = "Student, Administrator, Registrar, Teacher")]
    public class StudentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Student")]
        public async Task<ActionResult> Index()
        {
            var students = db.Students.Include(s => s.Major);
            return View(await students.ToListAsync());
        }

        [Authorize(Roles = "Administrator, Registrar, Teacher")]
        public async Task<ActionResult> Students()
        {
            var students = db.Students.Include(s => s.Major);
            return View(await students.ToListAsync());
        }

        [Authorize(Roles = "Student")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentModel studentModel = await db.Students.FindAsync(id);
            if (studentModel == null)
            {
                return HttpNotFound();
            }
            return View(studentModel);
        }

        [Authorize(Roles = "Student")]
        public ActionResult Create()
        {
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProfileID,StudentID,StudentName,MajorID,ClassName,StartDate")] StudentModel studentModel)
        {
            studentModel.ProfileID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Students.Add(studentModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", studentModel.MajorID);
            return View(studentModel);
        }

        [Authorize(Roles = "Student")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentModel studentModel = await db.Students.FindAsync(id);
            if (studentModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", studentModel.MajorID);
            return View(studentModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProfileID,StudentID,StudentName,MajorID,ClassName,StartDate")] StudentModel studentModel)
        {
            studentModel.ProfileID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(studentModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", studentModel.MajorID);
            return View(studentModel);
        }

        [Authorize(Roles = "Student")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentModel studentModel = await db.Students.FindAsync(id);
            if (studentModel == null)
            {
                return HttpNotFound();
            }
            return View(studentModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            StudentModel studentModel = await db.Students.FindAsync(id);
            db.Students.Remove(studentModel);
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
