using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSU.Models
{
    
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }
     // ? needed Enrollment does not contain definition of CourseId error  [ForeignKey("Course")]
        public int CourseId { get; internal set; }
      // ? needed  [ForeignKey("Student")]
        public int StudentId { get; internal set; }
        [DisplayFormat(NullDisplayText ="No grade")]
        public Grade? Grade { get; internal set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
    public enum Grade
    {
        A, B, C, D, F, XF
    }
}