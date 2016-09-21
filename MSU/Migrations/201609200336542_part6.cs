namespace MSU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class part6 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Courses", newName: "Course");
            RenameTable(name: "dbo.Enrollments", newName: "Enrollment");
            RenameTable(name: "dbo.Students", newName: "Student");
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        InstructorId = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Instructor", t => t.InstructorId)
                .Index(t => t.InstructorId);
            
            CreateTable(
                "dbo.Instructor",
                c => new
                    {
                        InstructorId = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        MiddleName = c.String(maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        HireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InstructorId);
            
            CreateTable(
                "dbo.OfficeAssignment",
                c => new
                    {
                        InstructorId = c.Int(nullable: false),
                        OfficeLocation = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.InstructorId)
                .ForeignKey("dbo.Instructor", t => t.InstructorId)
                .Index(t => t.InstructorId);
            
            CreateTable(
                "dbo.CourseInstructor",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        InstructorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseId, t.InstructorId })
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Instructor", t => t.InstructorId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.InstructorId);
            // Had to add the next few lines to get around alter table conflict

            /*
             * Sometimes when you execute migrations with existing data, you need to insert stub data into the database to satisfy foreign key constraints, and that's what you have to do now. 
             * The generated code in the ComplexDataModel Up method adds a non-nullable DepartmentID foreign key to the Course table. 
             * Because there are already rows in the Course table when the code runs, the AddColumn operation will fail because SQL Server doesn't know what value to put in the column that can't be null. 
             * Therefore have to change the code to give the new column a default value, and create a stub department named "Temp" to act as the default department. 
             * As a result, existing Course rows will all be related to the "Temp" department after the Up method runs.  
             * You can relate them to the correct departments in the Seed method. 
             *
             * */

            // Create  a department for course to point to.
            Sql("INSERT INTO dbo.Department (DepartmentName, Budget) VALUES ('Temp', 0.00)");
            //  default value for FK points to department created above.
            AddColumn("dbo.Course", "DepartmentID", c => c.Int(nullable: false, defaultValue: 1));
            //AddColumn("dbo.Course", "DepartmentID", c => c.Int(nullable: false));

            AddColumn("dbo.Course", "RoomNumber", c => c.String(maxLength: 20));
            //AddColumn("dbo.Course", "DepartmentId", c => c.Int(nullable: false));
            AlterColumn("dbo.Course", "Title", c => c.String(maxLength: 20));
            CreateIndex("dbo.Course", "DepartmentId");
            AddForeignKey("dbo.Course", "DepartmentId", "dbo.Department", "DepartmentId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseInstructor", "InstructorId", "dbo.Instructor");
            DropForeignKey("dbo.CourseInstructor", "CourseId", "dbo.Course");
            DropForeignKey("dbo.Course", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.Department", "InstructorId", "dbo.Instructor");
            DropForeignKey("dbo.OfficeAssignment", "InstructorId", "dbo.Instructor");
            DropIndex("dbo.CourseInstructor", new[] { "InstructorId" });
            DropIndex("dbo.CourseInstructor", new[] { "CourseId" });
            DropIndex("dbo.OfficeAssignment", new[] { "InstructorId" });
            DropIndex("dbo.Department", new[] { "InstructorId" });
            DropIndex("dbo.Course", new[] { "DepartmentId" });
            AlterColumn("dbo.Course", "Title", c => c.String());
            DropColumn("dbo.Course", "DepartmentId");
            DropColumn("dbo.Course", "RoomNumber");
            DropTable("dbo.CourseInstructor");
            DropTable("dbo.OfficeAssignment");
            DropTable("dbo.Instructor");
            DropTable("dbo.Department");
            RenameTable(name: "dbo.Student", newName: "Students");
            RenameTable(name: "dbo.Enrollment", newName: "Enrollments");
            RenameTable(name: "dbo.Course", newName: "Courses");
        }
    }
}
