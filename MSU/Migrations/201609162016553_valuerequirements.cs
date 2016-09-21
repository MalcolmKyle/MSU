namespace MSU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class valuerequirements : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Students", "MiddleName", c => c.String(maxLength: 20));
            AlterColumn("dbo.Students", "FirstName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "MiddleName", c => c.String());
            AlterColumn("dbo.Students", "LastName", c => c.String(nullable: false));
        }
    }
}
