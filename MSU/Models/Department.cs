using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSU.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required,StringLength(50),Display(Name ="Department Name")]
        public string DepartmentName { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        // In the code for the Department entity, the Column attribute is being used to change SQL data type mapping 
        // so that the column will be defined using the SQL Server money type in the database
        public decimal Budget { get; set; }

        public int? InstructorId { get; set; }

        public virtual Instructor Administrator { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}