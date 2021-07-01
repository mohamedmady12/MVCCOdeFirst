using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCCOdeFirst.Models
{
    public class StudentCourse
    {
        [Key]
        [Column(Order =0)]
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public int Grad { get; set; }
        public Student Student  { get; set; }
        public Course Course  { get; set; }
    }
}