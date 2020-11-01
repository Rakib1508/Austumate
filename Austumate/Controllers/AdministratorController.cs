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
    [Authorize(Roles = "Administrator")]
    public class AdministratorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public async Task<ActionResult> Index()
        {
            var administrators = db.Administrators.Include(a => a.Profile);
            return View(await administrators.ToListAsync());
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdministratorModel administratorModel = await db.Administrators.FindAsync(id);
            if (administratorModel == null)
            {
                return HttpNotFound();
            }
            return View(administratorModel);
        }

        public ActionResult Create()
        {
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProfileID,AdministratorID,AdministratorName,JoinDate,Mailbox")] AdministratorModel administratorModel)
        {
            administratorModel.ProfileID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Administrators.Add(administratorModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", administratorModel.ProfileID);
            return View(administratorModel);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdministratorModel administratorModel = await db.Administrators.FindAsync(id);
            if (administratorModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", administratorModel.ProfileID);
            return View(administratorModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProfileID,AdministratorID,AdministratorName,JoinDate,Mailbox")] AdministratorModel administratorModel)
        {
            administratorModel.ProfileID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(administratorModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", administratorModel.ProfileID);
            return View(administratorModel);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdministratorModel administratorModel = await db.Administrators.FindAsync(id);
            if (administratorModel == null)
            {
                return HttpNotFound();
            }
            return View(administratorModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            AdministratorModel administratorModel = await db.Administrators.FindAsync(id);
            db.Administrators.Remove(administratorModel);
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
