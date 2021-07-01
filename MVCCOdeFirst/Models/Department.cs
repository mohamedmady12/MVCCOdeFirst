using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCCOdeFirst.Models
{
    public class Department
    {
       [Key]
        public int DeptID { get; set; }
     [Required]
     [StringLength(10,MinimumLength =3)]
        public string DeptName { get; set; }

        public virtual List<Student> Students { get; set; }


    }
}