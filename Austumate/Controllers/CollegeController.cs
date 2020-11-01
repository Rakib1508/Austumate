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
    public class CollegeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Index()
        {
            var colleges = db.Colleges.Include(c => c.Campus);
            return View(await colleges.ToListAsync());
        }

        public async Task<ActionResult> Colleges()
        {
            var colleges = db.Colleges.Include(c => c.Campus);
            return View(await colleges.ToListAsync());
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollegeModel collegeModel = await db.Colleges.FindAsync(id);
            if (collegeModel == null)
            {
                return HttpNotFound();
            }
            return View(collegeModel);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.CampusName = new SelectList(db.Campuses, "CampusID", "CampusName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CollegeID,CollegeName,CampusName,CollegeMailbox,PhoneNumber")] CollegeModel collegeModel)
        {
            if (ModelState.IsValid)
            {
                db.Colleges.Add(collegeModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CampusName = new SelectList(db.Campuses, "CampusID", "CampusName", collegeModel.CampusName);
            return View(collegeModel);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollegeModel collegeModel = await db.Colleges.FindAsync(id);
            if (collegeModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusName = new SelectList(db.Campuses, "CampusID", "CampusName", collegeModel.CampusName);
            return View(collegeModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CollegeID,CollegeName,CampusName,CollegeMailbox,PhoneNumber")] CollegeModel collegeModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collegeModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CampusName = new SelectList(db.Campuses, "CampusID", "CampusName", collegeModel.CampusName);
            return View(collegeModel);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollegeModel collegeModel = await db.Colleges.FindAsync(id);
            if (collegeModel == null)
            {
                return HttpNotFound();
            }
            return View(collegeModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            CollegeModel collegeModel = await db.Colleges.FindAsync(id);
            db.Colleges.Remove(collegeModel);
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
