using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCCOdeFirst.Models
{
    public class Course
    {
        [Key]
        public int Cr_ID { get; set; }
        [Required]
        public string Cr_name { get; set; }
        public virtual List<StudentCourse> Mycourse { get; set; }
    }
}