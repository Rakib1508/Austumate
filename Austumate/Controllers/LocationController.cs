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
    public class LocationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Index()
        {
            var locations = db.Locations.Include(l => l.Campus);
            return View(await locations.ToListAsync());
        }

        public async Task<ActionResult> Locations()
        {
            var locations = db.Locations.Include(l => l.Campus);
            return View(await locations.ToListAsync());
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationModel locationModel = await db.Locations.FindAsync(id);
            if (locationModel == null)
            {
                return HttpNotFound();
            }
            return View(locationModel);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.CampusName = new SelectList(db.Campuses, "CampusID", "CampusName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LocationID,LocationName,CampusName,LocationDetails")] LocationModel locationModel)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(locationModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CampusName = new SelectList(db.Campuses, "CampusID", "CampusName", locationModel.CampusName);
            return View(locationModel);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationModel locationModel = await db.Locations.FindAsync(id);
            if (locationModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusName = new SelectList(db.Campuses, "CampusID", "CampusName", locationModel.CampusName);
            return View(locationModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LocationID,LocationName,CampusName,LocationDetails")] LocationModel locationModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locationModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CampusName = new SelectList(db.Campuses, "CampusID", "CampusName", locationModel.CampusName);
            return View(locationModel);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LocationModel locationModel = await db.Locations.FindAsync(id);
            if (locationModel == null)
            {
                return HttpNotFound();
            }
            return View(locationModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            LocationModel locationModel = await db.Locations.FindAsync(id);
            db.Locations.Remove(locationModel);
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
