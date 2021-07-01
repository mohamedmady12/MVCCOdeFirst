using MVCCOdeFirst.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCCOdeFirst.Models
{
    public class Student
    {
        //    [DatabaseGenerated(DatabaseGeneratedOption.)]
        public int id { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Name { get; set; }
        [Range(10, 90)]

        public int Age { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]
        public string password { get; set; }
        [NotMapped]//msh haero7 el dataase
        public string ConPassword { get; set; }
       [ForeignKey("Department")]
        public int DeptID { get; set; }

        public virtual Department Department { get; set; }

        public virtual List<StudentCourse> MyStudenCRS { get; set; }
    }
}