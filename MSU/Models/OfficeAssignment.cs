using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSU.Models
{
    public class OfficeAssignment
    {
        /*
         * When there is a  one-to-zero-or-one relationship or a  one-to-one relationship between two entities (such as between OfficeAssignment and Instructor)
         * EF can't work out which end of the relationship is the principal and which end is dependent. One-to-one relationships have a reference navigation property in each class to the other class. 
         * The ForeignKey Attribute can be applied to the dependent class to establish the relationship. 
        */
        [Key, ForeignKey("Instructor")]
        public int InstructorId { get; set; }

        [StringLength(20, ErrorMessage ="Location must be less than 20 charactes."), Display(Name ="Office Location")]
        public string OfficeLocation { get; set; }

        public virtual Instructor Instructor { get; set; }
    }
}