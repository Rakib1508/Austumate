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
    public class MajorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Index()
        {
            var majors = db.Majors.Include(m => m.College);
            return View(await majors.ToListAsync());
        }

        public async Task<ActionResult> Majors()
        {
            var majors = db.Majors.Include(m => m.College);
            return View(await majors.ToListAsync());
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorModel majorModel = await db.Majors.FindAsync(id);
            if (majorModel == null)
            {
                return HttpNotFound();
            }
            return View(majorModel);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.CollegeName = new SelectList(db.Colleges, "CollegeID", "CollegeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MajorID,MajorName,CollegeName,Duration,Level,Requirements")] MajorModel majorModel)
        {
            if (ModelState.IsValid)
            {
                db.Majors.Add(majorModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CollegeName = new SelectList(db.Colleges, "CollegeID", "CollegeName", majorModel.CollegeName);
            return View(majorModel);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorModel majorModel = await db.Majors.FindAsync(id);
            if (majorModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CollegeName = new SelectList(db.Colleges, "CollegeID", "CollegeName", majorModel.CollegeName);
            return View(majorModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MajorID,MajorName,CollegeName,Duration,Level,Requirements")] MajorModel majorModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(majorModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CollegeName = new SelectList(db.Colleges, "CollegeID", "CollegeName", majorModel.CollegeName);
            return View(majorModel);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorModel majorModel = await db.Majors.FindAsync(id);
            if (majorModel == null)
            {
                return HttpNotFound();
            }
            return View(majorModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            MajorModel majorModel = await db.Majors.FindAsync(id);
            db.Majors.Remove(majorModel);
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
