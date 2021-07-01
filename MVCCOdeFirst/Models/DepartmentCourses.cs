using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCCOdeFirst.Models
{
    public class DepartmentCourses
    {
        public DepartmentCourses()
        {
            Departments = new List<Department>();
            Courses = new List<Course>();
        }
        public  List< Department> Departments {get; set; }
        public  List< Course> Courses {get; set; }

        public int CourseId { get; set; }
    }
}