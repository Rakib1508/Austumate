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
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.Profiles.ToListAsync());
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Users()
        {
            return View(await db.Profiles.ToListAsync());
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileModel profileModel = await db.Profiles.FindAsync(id);
            if (profileModel == null)
            {
                return HttpNotFound();
            }
            return View(profileModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProfileID,FirstName,MiddleName,LastName,Birthday,Sex,PersonID,Address,Website,Bio")] ProfileModel profileModel)
        {
            profileModel.ProfileID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Profiles.Add(profileModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(profileModel);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileModel profileModel = await db.Profiles.FindAsync(id);
            if (profileModel == null)
            {
                return HttpNotFound();
            }
            return View(profileModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProfileID,FirstName,MiddleName,LastName,Birthday,Sex,PersonID,Address,Website,Bio")] ProfileModel profileModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profileModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(profileModel);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileModel profileModel = await db.Profiles.FindAsync(id);
            if (profileModel == null)
            {
                return HttpNotFound();
            }
            return View(profileModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ProfileModel profileModel = await db.Profiles.FindAsync(id);
            db.Profiles.Remove(profileModel);
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
