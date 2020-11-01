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
using System.IO;

namespace Austumate.Controllers
{
    [Authorize]
    public class AvatarController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public async Task<ActionResult> Index()
        {
            var avatars = db.Avatars.Include(a => a.Profile);
            return View(await avatars.ToListAsync());
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> UserAvatars()
        {
            var avatars = db.Avatars.Include(a => a.Profile);
            return View(await avatars.ToListAsync());
        }
        
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvatarModel avatarModel = await db.Avatars.FindAsync(id);
            if (avatarModel == null)
            {
                return HttpNotFound();
            }
            return View(avatarModel);
        }
        
        public ActionResult Create()
        {
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProfileID,AvatarImage")] AvatarModel avatarModel, HttpPostedFileBase image)
        {
            avatarModel.ProfileID = User.Identity.GetUserId();
            string FileName = Path.GetFileName(image.FileName);
            string ImagePath = Server.MapPath("~/Images/" + FileName);
            image.SaveAs(ImagePath);
            avatarModel.AvatarImage = "~/Images/" + FileName;
            if (ModelState.IsValid)
            {
                db.Avatars.Add(avatarModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", avatarModel.ProfileID);
            return View(avatarModel);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvatarModel avatarModel = await db.Avatars.FindAsync(id);
            if (avatarModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", avatarModel.ProfileID);
            return View(avatarModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProfileID,AvatarImage")] AvatarModel avatarModel, HttpPostedFileBase image)
        {
            avatarModel.ProfileID = User.Identity.GetUserId();
            string FileName = Path.GetFileName(image.FileName);
            string ImagePath = Server.MapPath("~/Images/" + FileName);
            image.SaveAs(ImagePath);
            avatarModel.AvatarImage = "~/Images/" + FileName;
            if (ModelState.IsValid)
            {
                db.Entry(avatarModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", avatarModel.ProfileID);
            return View(avatarModel);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvatarModel avatarModel = await db.Avatars.FindAsync(id);
            if (avatarModel == null)
            {
                return HttpNotFound();
            }
            return View(avatarModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            AvatarModel avatarModel = await db.Avatars.FindAsync(id);
            db.Avatars.Remove(avatarModel);
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
