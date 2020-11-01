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
    public class HomeworkScoreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Index()
        {
            var homeworkScores = db.HomeworkScores.Include(h => h.Course).Include(h => h.Semester).Include(h => h.Student).Include(h => h.Teacher);
            return View(await homeworkScores.ToListAsync());
        }

        [Authorize(Roles = "Registrar, Administrator")]
        public async Task<ActionResult> Records()
        {
            var homeworkScores = db.HomeworkScores.Include(h => h.Course).Include(h => h.Semester).Include(h => h.Student).Include(h => h.Teacher);
            return View(await homeworkScores.ToListAsync());
        }

        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeworkScoreModel homeworkScoreModel = await db.HomeworkScores.FindAsync(id);
            if (homeworkScoreModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", homeworkScoreModel.CourseID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Session", homeworkScoreModel.SemesterID);
            ViewBag.StudentID = new SelectList(db.Students, "ProfileID", "StudentName", homeworkScoreModel.StudentID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName", homeworkScoreModel.TeacherID);
            return View(homeworkScoreModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EnrollStudentID,CourseID,TeacherID,StudentID,SemesterID,Score,Remark")] HomeworkScoreModel homeworkScoreModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(homeworkScoreModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                int RecordStored = UpdateHomeworkScore(homeworkScoreModel.EnrollStudentID, homeworkScoreModel.CourseID, homeworkScoreModel.TeacherID, homeworkScoreModel.Score);
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", homeworkScoreModel.CourseID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Session", homeworkScoreModel.SemesterID);
            ViewBag.StudentID = new SelectList(db.Students, "ProfileID", "StudentName", homeworkScoreModel.StudentID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "ProfileID", "TeacherName", homeworkScoreModel.TeacherID);
            return View(homeworkScoreModel);
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
