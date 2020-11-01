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
    public class FinalExamScoreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Index()
        {
            var finalExamScores = db.FinalExamScores.Include(f => f.Course).Include(f => f.Semester).Include(f => f.Student).Include(f => f.Teacher);
            return View(await finalExamScores.ToListAsync());
        }

        [Authorize(Roles = "Registrar, Administrator")]
        public async Task<ActionResult> Records()
        {
            var finalExamScores = db.FinalExamScores.Include(f => f.Course).Include(f => f.Semester).Include(f => f.Student).Include(f => f.Teacher);
            return View(await finalExamScores.ToListAsync());
        }

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinalExamScoreModel finalExamScoreModel = await db.FinalExamScores.FindAsync(id);
            if (finalExamScoreModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", finalExamScoreModel.CourseID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Session", finalExamScoreModel.SemesterID);
            ViewBag.StudentID = new SelectList(db.Students, "ProfileID", "StudentName", finalExamScoreModel.StudentID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName", finalExamScoreModel.TeacherID);
            return View(finalExamScoreModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnrollStudentID,CourseID,TeacherID,StudentID,SemesterID,Score,Remark")] FinalExamScoreModel finalExamScoreModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(finalExamScoreModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                int RecordStored = UpdateFinalExamScore(finalExamScoreModel.EnrollStudentID, finalExamScoreModel.CourseID, finalExamScoreModel.TeacherID, finalExamScoreModel.Score);
                int GradeStored = UpdateGrade(finalExamScoreModel.EnrollStudentID, "");
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", finalExamScoreModel.CourseID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Session", finalExamScoreModel.SemesterID);
            ViewBag.StudentID = new SelectList(db.Students, "ProfileID", "StudentName", finalExamScoreModel.StudentID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName", finalExamScoreModel.TeacherID);
            return View(finalExamScoreModel);
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
