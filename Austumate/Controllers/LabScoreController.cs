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
using static AustumateDataLibrary.BusinessLogic.ScoresheetProcessor;

namespace Austumate.Controllers
{
    [Authorize(Roles = "Teacher, Registrar, Administrator")]
    public class LabScoreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Index()
        {
            var labScores = db.LabScores.Include(l => l.Course).Include(l => l.Semester).Include(l => l.Student).Include(l => l.Teacher);
            return View(await labScores.ToListAsync());
        }

        [Authorize(Roles = "Registrar, Administrator")]
        public async Task<ActionResult> Records()
        {
            var labScores = db.LabScores.Include(l => l.Course).Include(l => l.Semester).Include(l => l.Student).Include(l => l.Teacher);
            return View(await labScores.ToListAsync());
        }

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabScoreModel labScoreModel = await db.LabScores.FindAsync(id);
            if (labScoreModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", labScoreModel.CourseID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Session", labScoreModel.SemesterID);
            ViewBag.StudentID = new SelectList(db.Students, "ProfileID", "StudentName", labScoreModel.StudentID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName", labScoreModel.TeacherID);
            return View(labScoreModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnrollStudentID,CourseID,TeacherID,StudentID,SemesterID,Score,Remark")] LabScoreModel labScoreModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(labScoreModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                int RecordStored = UpdateLabScore(labScoreModel.EnrollStudentID, labScoreModel.CourseID, labScoreModel.TeacherID, labScoreModel.Score);
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", labScoreModel.CourseID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Session", labScoreModel.SemesterID);
            ViewBag.StudentID = new SelectList(db.Students, "ProfileID", "StudentName", labScoreModel.StudentID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName", labScoreModel.TeacherID);
            return View(labScoreModel);
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
