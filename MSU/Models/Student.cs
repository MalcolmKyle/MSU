using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MSU.Models
{
    public class Student
    {
        // ID or studentID? with respect to Enrollment
        public int ID { get; set; }
        [Required, Display(Name ="Last Name"), StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }
        [Display(Name = "Middle Name"),StringLength(20, ErrorMessage = "Middle name cannot be longer than 20 characters.")]
        public string MiddleName { get; set; }
        [Required, Display(Name = "First Name"),StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime EnrollmentDate { get; set; }

        public string Secret { get; set; }

        [Display(Name ="Enrollments")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}