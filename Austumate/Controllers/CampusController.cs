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
    public class CampusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Index()
        {
            return View(await db.Campuses.ToListAsync());
        }

        public async Task<ActionResult> Campuses()
        {
            return View(await db.Campuses.ToListAsync());
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampusModel campusModel = await db.Campuses.FindAsync(id);
            if (campusModel == null)
            {
                return HttpNotFound();
            }
            return View(campusModel);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CampusID,CampusName,Address")] CampusModel campusModel)
        {
            if (ModelState.IsValid)
            {
                db.Campuses.Add(campusModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(campusModel);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampusModel campusModel = await db.Campuses.FindAsync(id);
            if (campusModel == null)
            {
                return HttpNotFound();
            }
            return View(campusModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CampusID,CampusName,Address")] CampusModel campusModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(campusModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(campusModel);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CampusModel campusModel = await db.Campuses.FindAsync(id);
            if (campusModel == null)
            {
                return HttpNotFound();
            }
            return View(campusModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            CampusModel campusModel = await db.Campuses.FindAsync(id);
            db.Campuses.Remove(campusModel);
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
