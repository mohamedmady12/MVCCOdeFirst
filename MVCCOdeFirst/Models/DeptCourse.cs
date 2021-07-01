using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCCOdeFirst.Models
{
    public class DeptCourse
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Department")]
        public int DeptID { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Department Department { get; set; }
        public Course Course { get; set; }
    }
}