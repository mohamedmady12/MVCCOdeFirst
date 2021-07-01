using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCCOdeFirst.Models;

namespace MVCCOdeFirst.Controllers
{
    public class StudentsController : Controller
    {
        private ITIModel db = new ITIModel();

        // GET: Students

        public ActionResult AddCrsToStd(int id)
        {
          
            var s = db.StudentCourses.Where(a => a.StudentID == id).Select(a => a.Course).ToList();
            var allCrs = db.Courses.ToList<Course>();
            var ExitCourse = allCrs.Except(s).ToList<Course>();
            
            return View(ExitCourse);
        }
        [HttpPost]
        public ActionResult AddCrsToStd(int id, int[] course)
        {
            foreach(var item in course)
            {
                db.StudentCourses.Add(new StudentCourse { StudentID = id, CourseID = item });
            }
            db.SaveChanges();
            return RedirectToAction("index");

        }


        public ActionResult RemoveCrsToSTd(int id)
        {

            var sd = db.StudentCourses.Where(a => a.StudentID == id).Select(a => a.Course).ToList();
            return View(sd);
        }
        [HttpPost]
        public ActionResult RemoveCrsToSTd(int id, int[] course)
        {
            foreach (var item in course)
            {
          StudentCourse sc=db.StudentCourses.FirstOrDefault(a => a.CourseID == item && a.StudentID == id);
                db.StudentCourses.Remove(sc);
            }
            db.SaveChanges();
            return RedirectToAction("index");

        }


        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Department);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.DeptID = new SelectList(db.Departments, "DeptID", "DeptName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Age,Email,password,DeptID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeptID = new SelectList(db.Departments, "DeptID", "DeptName", student.DeptID);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeptID = new SelectList(db.Departments, "DeptID", "DeptName", student.DeptID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Age,Email,password,DeptID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptID = new SelectList(db.Departments, "DeptID", "DeptName", student.DeptID);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
