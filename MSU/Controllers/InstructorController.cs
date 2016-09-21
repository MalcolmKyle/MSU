using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MSU.Models;

namespace MSU.Controllers
{
    public class InstructorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Instructor
        public ActionResult Index(int? id, int? courseId)
        {
            var viewModel = new InstructorViewModel();
            viewModel.Instructors = db.Instructors.Include(i => i.OfficeAssignemnt).Include(i => i.Courses.Select(c => c.Department))
             .OrderBy(i => i.LastName);

            

            if (id!= null)
            {
                ViewBag.InstructorId = id.Value;
                viewModel.Courses = viewModel.Instructors.Where(i => i.InstructorId == id.Value).Single().Courses;
               // ViewBag.InstructorLastName = viewModel.Instructors.Where(i => i.InstructorId == id.Value).Single().LastName;
               // ViewBag.InstructorFirstName = viewModel.Instructors.Where(i => i.InstructorId == id.Value).Single().FirstName;
            }

            if (courseId != null)
            {
                ViewBag.CourseId = courseId.Value;
                // Lazy loading
                //viewModel.Enrollments = viewModel.Courses.Where(
                //    x => x.CourseID == courseID).Single().Enrollments;
                // Explicit loading
                var selectedCourse = viewModel.Courses.Where(x => x.CourseId == courseId).Single();
                db.Entry(selectedCourse).Collection(x => x.Enrollments).Load();
                foreach (Enrollment enrollment in selectedCourse.Enrollments)
                {
                    db.Entry(enrollment).Reference(x => x.Student).Load();
                }

                viewModel.Enrollments = selectedCourse.Enrollments;
            }

            return View(viewModel);
        }

        // GET: Instructor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: Instructor/Create
        public ActionResult Create()
        {
            ViewBag.InstructorId = new SelectList(db.OfficeAssigments, "InstructorId", "OfficeLocation");
            return View();
        }

        // POST: Instructor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstructorId,LastName,MiddleName,FirstName,HireDate")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                db.Instructors.Add(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstructorId = new SelectList(db.OfficeAssigments, "InstructorId", "OfficeLocation", instructor.InstructorId);
            return View(instructor);
        }

        // GET: Instructor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Include(i=>i.OfficeAssignemnt)
                .Where(i=>i.InstructorId == id).Single();
            if (instructor == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstructorId = new SelectList(db.OfficeAssigments, "InstructorId", "OfficeLocation", instructor.InstructorId);
            return View(instructor);
        }

        // POST: Instructor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstructorId,LastName,MiddleName,FirstName,HireDate")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instructor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstructorId = new SelectList(db.OfficeAssigments, "InstructorId", "OfficeLocation", instructor.InstructorId);
            return View(instructor);
        }

        // GET: Instructor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            db.Instructors.Remove(instructor);
            db.SaveChanges();
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
