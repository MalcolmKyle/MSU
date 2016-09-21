using MSU.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSU.Models
{
    public class Instructor
    {
        // Source: http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-a-more-complex-data-model-for-an-asp-net-mvc-application

        public int InstructorId { get; set; }

        [Required, Display(Name ="Last Name"), StringLength(50)]
        public string LastName { get; set; }
        [Display(Name = "Middle Name"), StringLength(50)]
        public string MiddleName { get; set; }
        [Required, Display(Name = "First Name"), StringLength(50)]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Hire Date"),DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime HireDate { get; set; }

        /*
         * The Courses and OfficeAssignment properties are navigation properties. 
         * They are typically defined as virtual so that they can take advantage of an Entity Framework feature called lazy loading. 
         * In addition, if a navigation property can hold multiple entities, its type must implement the ICollection<T> Interface. 
         * For example IList<T> qualifies but not IEnumerable<T> because IEnumerable<T> doesn't implement Add.
         */
        public virtual ICollection<Course> Courses { get; set; }
        // Each instructor must have an office
        // Add room number to classes model?
      //  [Display(Name ="Office")]
        public virtual OfficeAssignment OfficeAssignemnt { get; set; }
    }
}
 