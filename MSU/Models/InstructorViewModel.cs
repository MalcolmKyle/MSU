using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MSU.Models;

namespace MSU.Models
{
    public class InstructorViewModel
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}