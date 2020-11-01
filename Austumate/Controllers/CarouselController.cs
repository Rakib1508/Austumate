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
using System.IO;
using Microsoft.AspNet.Identity;

namespace Austumate.Controllers
{
    [Authorize]
    public class CarouselController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public async Task<ActionResult> Index()
        {
            var carousels = db.Carousels.Include(c => c.Profile);
            return View(await carousels.ToListAsync());
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> UserCarousels()
        {
            var carousels = db.Carousels.Include(c => c.Profile);
            return View(await carousels.ToListAsync());
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarouselModel carouselModel = await db.Carousels.FindAsync(id);
            if (carouselModel == null)
            {
                return HttpNotFound();
            }
            return View(carouselModel);
        }

        public ActionResult Create()
        {
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProfileID,CarouselImage")] CarouselModel carouselModel, HttpPostedFileBase image)
        {
            carouselModel.ProfileID = User.Identity.GetUserId();
            string FileName = Path.GetFileName(image.FileName);
            string ImagePath = Server.MapPath("~/Images/" + FileName);
            image.SaveAs(ImagePath);
            carouselModel.CarouselImage = "~/Images/" + FileName;
            if (ModelState.IsValid)
            {
                db.Carousels.Add(carouselModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", carouselModel.ProfileID);
            return View(carouselModel);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarouselModel carouselModel = await db.Carousels.FindAsync(id);
            if (carouselModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", carouselModel.ProfileID);
            return View(carouselModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProfileID,CarouselImage")] CarouselModel carouselModel, HttpPostedFileBase image)
        {
            carouselModel.ProfileID = User.Identity.GetUserId();
            string FileName = Path.GetFileName(image.FileName);
            string ImagePath = Server.MapPath("~/Images/" + FileName);
            image.SaveAs(ImagePath);
            carouselModel.CarouselImage = "~/Images/" + FileName;
            if (ModelState.IsValid)
            {
                db.Entry(carouselModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProfileID = new SelectList(db.Profiles, "ProfileID", "FirstName", carouselModel.ProfileID);
            return View(carouselModel);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarouselModel carouselModel = await db.Carousels.FindAsync(id);
            if (carouselModel == null)
            {
                return HttpNotFound();
            }
            return View(carouselModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            CarouselModel carouselModel = await db.Carousels.FindAsync(id);
            db.Carousels.Remove(carouselModel);
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
