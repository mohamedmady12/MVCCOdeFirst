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
    public class DepartmentsController : Controller
    {
        private ITIModel db = new ITIModel();

        public ActionResult AddCrsToDept(int id)
        {

            var s = db.DeptCourses.Where(a => a.DeptID == id).Select(a => a.Course).ToList();
            var allCrs = db.Courses.ToList<Course>();
            var ExitCourse = allCrs.Except(s).ToList<Course>();

            return View(ExitCourse);
        }
        [HttpPost]
        public ActionResult AddCrsToDept(int id, int[] course)
        {
            foreach (var item in course)
            {
                db.DeptCourses.Add(new DeptCourse { DeptID = id, CourseID = item });
                var stds = db.Students.Where(a => a.DeptID == id).ToList();
                foreach (var studentitem in stds)
                {
                    if (!db.StudentCourses.Any(a => a.CourseID == item && a.StudentID == studentitem.id))
                    {
                        db.StudentCourses.Add(new StudentCourse { StudentID = studentitem.id, CourseID = item });
                    }
                }
            }
            db.SaveChanges();
            return RedirectToAction("index");

        }


        public ActionResult RemoveCrsToDept(int id)
        {

            var sd = db.DeptCourses.Where(a => a.DeptID == id).Select(a => a.Course).ToList();
            return View(sd);
        }
        [HttpPost]
        public ActionResult RemoveCrsToDept(int id, int[] course)
        {
            foreach (var item in course)
            {
                DeptCourse dc = db.DeptCourses.FirstOrDefault(a => a.CourseID == item && a.DeptID == id);
                db.DeptCourses.Remove(dc);
            }
            db.SaveChanges();
            return RedirectToAction("index");

        }






        // GET: Departments
        public ActionResult Index()
        {
            DepartmentCourses dept_Crs = new DepartmentCourses();
            dept_Crs.Courses = db.Courses.ToList();
            dept_Crs.Departments = db.Departments.ToList();

            return View("Index",dept_Crs);

        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeptID,DeptName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeptID,DeptName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
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
