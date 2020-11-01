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
    public class RegistrarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Registrar")]
        public async Task<ActionResult> Index()
        {
            var registrars = db.Registrars.Include(r => r.Profile);
            return View(await registrars.ToListAsync());
        }
        
        public async Task<ActionResult> Registrars()
        {
            var registrars = db.Registrars.Include(r => r.Profile);
            return View(await registrars.ToListAsync());
        }

        [Authorize(Roles = "Registrar")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistrarModel registrarModel = await db.Registrars.FindAsync(id);
            if (registrarModel == null)
            {
                return HttpNotFound();
            }
            return View(registrarModel);
        }

        [Authorize(Roles = "Registrar")]
        public ActionResult Create()
        {
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProfileID,RegistrarID,RegistrarName,JoinDate,Mailbox")] RegistrarModel registrarModel)
        {
            registrarModel.ProfileID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Registrars.Add(registrarModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", registrarModel.ProfileID);
            return View(registrarModel);
        }

        [Authorize(Roles = "Registrar")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistrarModel registrarModel = await db.Registrars.FindAsync(id);
            if (registrarModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", registrarModel.ProfileID);
            return View(registrarModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProfileID,RegistrarID,RegistrarName,JoinDate,Mailbox")] RegistrarModel registrarModel)
        {
            registrarModel.ProfileID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(registrarModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", registrarModel.ProfileID);
            return View(registrarModel);
        }

        [Authorize(Roles = "Registrar")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistrarModel registrarModel = await db.Registrars.FindAsync(id);
            if (registrarModel == null)
            {
                return HttpNotFound();
            }
            return View(registrarModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            RegistrarModel registrarModel = await db.Registrars.FindAsync(id);
            db.Registrars.Remove(registrarModel);
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
