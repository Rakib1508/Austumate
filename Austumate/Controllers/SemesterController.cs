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
    [Authorize(Roles = "Administrator")]
    public class SemesterController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.Semesters.ToListAsync());
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SemesterModel semesterModel = await db.Semesters.FindAsync(id);
            if (semesterModel == null)
            {
                return HttpNotFound();
            }
            return View(semesterModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SemesterID,Session")] SemesterModel semesterModel)
        {
            if (ModelState.IsValid)
            {
                db.Semesters.Add(semesterModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(semesterModel);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SemesterModel semesterModel = await db.Semesters.FindAsync(id);
            if (semesterModel == null)
            {
                return HttpNotFound();
            }
            return View(semesterModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SemesterID,Session")] SemesterModel semesterModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(semesterModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(semesterModel);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SemesterModel semesterModel = await db.Semesters.FindAsync(id);
            if (semesterModel == null)
            {
                return HttpNotFound();
            }
            return View(semesterModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            SemesterModel semesterModel = await db.Semesters.FindAsync(id);
            db.Semesters.Remove(semesterModel);
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
