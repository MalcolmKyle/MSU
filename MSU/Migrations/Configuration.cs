namespace MSU.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Data.Entity;

    internal sealed class Configuration : DbMigrationsConfiguration<MSU.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            
        }
        private ApplicationDbContext context = new ApplicationDbContext();

        protected override void Seed(MSU.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            var students = new List<Student>
            {
                new Student { FirstName = "Carson",   LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { FirstName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Arturo",   LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Gytis",    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Yan",      LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstName = "Peggy",    LastName = "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { FirstName = "Laura",    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstName = "Nino",     LastName = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-09-01") }
            };


            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var instructors = new List<Instructor>
            {
                new Instructor { FirstName = "Kim",     LastName = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstName = "Fadi",    LastName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstName = "Roger",   LastName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Instructor { FirstName = "Candace", LastName = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { FirstName = "Roger",   LastName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12") }
            };
            instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department { DepartmentName = "English",     Budget = 350000,
                    InstructorId  = instructors.Single( i => i.LastName == "Abercrombie").InstructorId },
                new Department { DepartmentName = "Mathematics", Budget = 100000,
                    InstructorId  = instructors.Single( i => i.LastName == "Fakhouri").InstructorId},
                new Department { DepartmentName = "Engineering", Budget = 350000,
                    InstructorId  = instructors.Single( i => i.LastName == "Harui").InstructorId },
                new Department { DepartmentName = "Economics",   Budget = 100000,
                    InstructorId  = instructors.Single( i => i.LastName == "Kapoor").InstructorId}
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.DepartmentName, s));
            context.SaveChanges();

           

            var courses = new List<Course>
            {
                new Course {CourseId= 1050, Title = "Chemistry",      Credits = 3,
                  DepartmentId = departments.Single( s => s.DepartmentName == "Engineering").DepartmentId,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseId = 4022, Title = "Microeconomics", Credits = 3,
                  DepartmentId = departments.Single( s => s.DepartmentName == "Economics").DepartmentId,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseId = 4041, Title = "Macroeconomics", Credits = 3,
                  DepartmentId = departments.Single( s => s.DepartmentName == "Economics").DepartmentId,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseId = 1045, Title = "Calculus",       Credits = 4,
                  DepartmentId = departments.Single( s => s.DepartmentName == "Mathematics").DepartmentId,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseId = 3141, Title = "Trigonometry",   Credits = 4,
                  DepartmentId = departments.Single( s => s.DepartmentName == "Mathematics").DepartmentId,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseId = 2021, Title = "Composition",    Credits = 3,
                  DepartmentId = departments.Single( s => s.DepartmentName == "English").DepartmentId,
                  Instructors = new List<Instructor>()
                },
                new Course {CourseId = 2042, Title = "Literature",     Credits = 4,
                  DepartmentId = departments.Single( s => s.DepartmentName == "English").DepartmentId,
                  Instructors = new List<Instructor>()
                },
            };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.CourseId, s));
            context.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment {
                    InstructorId = instructors.Single( i => i.LastName == "Fakhouri").InstructorId,
                    OfficeLocation = "Smith 17" },
                new OfficeAssignment {
                    InstructorId = instructors.Single( i => i.LastName == "Harui").InstructorId,
                    OfficeLocation = "Gowan 27" },
                new OfficeAssignment {
                    InstructorId = instructors.Single( i => i.LastName == "Kapoor").InstructorId,
                    OfficeLocation = "Thompson 304" },
            };
            officeAssignments.ForEach(s => context.OfficeAssigments.AddOrUpdate(p => p.InstructorId, s));
            context.SaveChanges();

            AddOrUpdateInstructor(context, "Chemistry", "Kapoor");
            AddOrUpdateInstructor(context, "Chemistry", "Harui");
            AddOrUpdateInstructor(context, "Microeconomics", "Zheng");
            AddOrUpdateInstructor(context, "Macroeconomics", "Zheng");

            AddOrUpdateInstructor(context, "Calculus", "Fakhouri");
            AddOrUpdateInstructor(context, "Trigonometry", "Harui");
            AddOrUpdateInstructor(context, "Composition", "Abercrombie");
            AddOrUpdateInstructor(context, "Literature", "Abercrombie");

            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                // internal set was needed for StudentId and Course Id?
                new Enrollment {
                    StudentId= students.Single(s => s.LastName == "Alexander").ID,
                    CourseId = courses.Single(c => c.Title == "Chemistry" ).CourseId,
                    Grade = Grade.A
                },
                 new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Alexander").ID,
                    CourseId = courses.Single(c => c.Title == "Microeconomics" ).CourseId,
                    Grade = Grade.C
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Alexander").ID,
                    CourseId = courses.Single(c => c.Title == "Macroeconomics" ).CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentId = students.Single(s => s.LastName == "Alonso").ID,
                    CourseId = courses.Single(c => c.Title == "Calculus" ).CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentId = students.Single(s => s.LastName == "Alonso").ID,
                    CourseId = courses.Single(c => c.Title == "Trigonometry" ).CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Alonso").ID,
                    CourseId = courses.Single(c => c.Title == "Composition" ).CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Anand").ID,
                    CourseId = courses.Single(c => c.Title == "Chemistry" ).CourseId
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Anand").ID,
                    CourseId = courses.Single(c => c.Title == "Microeconomics").CourseId,
                    Grade = Grade.B
                 },
                new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Barzdukas").ID,
                    CourseId = courses.Single(c => c.Title == "Chemistry").CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Li").ID,
                    CourseId = courses.Single(c => c.Title == "Composition").CourseId,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentId = students.Single(s => s.LastName == "Justice").ID,
                    CourseId = courses.Single(c => c.Title == "Literature").CourseId,
                    Grade = Grade.B
                 }
            };

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                         s.Student.ID == e.StudentId &&
                         s.Course.CourseId == e.CourseId).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }

        void AddOrUpdateInstructor(ApplicationDbContext context, string courseTitle, string instructorName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = crs.Instructors.SingleOrDefault(i => i.LastName == instructorName);
            if (inst == null)
                crs.Instructors.Add(context.Instructors.Single(i => i.LastName == instructorName));
        }
    
    }
    
}
