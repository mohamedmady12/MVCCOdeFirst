namespace MVCCOdeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Cr_ID = c.Int(nullable: false, identity: true),
                        Cr_name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Cr_ID);
            
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        StudentID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        Grad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentID, t.CourseID })
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                        Age = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        password = c.String(nullable: false),
                        DeptID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Departments", t => t.DeptID, cascadeDelete: true)
                .Index(t => t.DeptID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DeptID = c.Int(nullable: false, identity: true),
                        DeptName = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.DeptID);
            
            CreateTable(
                "dbo.DeptCourses",
                c => new
                    {
                        DeptID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DeptID, t.CourseID })
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DeptID, cascadeDelete: true)
                .Index(t => t.DeptID)
                .Index(t => t.CourseID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeptCourses", "DeptID", "dbo.Departments");
            DropForeignKey("dbo.DeptCourses", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.StudentCourses", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Students", "DeptID", "dbo.Departments");
            DropForeignKey("dbo.StudentCourses", "CourseID", "dbo.Courses");
            DropIndex("dbo.DeptCourses", new[] { "CourseID" });
            DropIndex("dbo.DeptCourses", new[] { "DeptID" });
            DropIndex("dbo.Students", new[] { "DeptID" });
            DropIndex("dbo.StudentCourses", new[] { "CourseID" });
            DropIndex("dbo.StudentCourses", new[] { "StudentID" });
            DropTable("dbo.DeptCourses");
            DropTable("dbo.Departments");
            DropTable("dbo.Students");
            DropTable("dbo.StudentCourses");
            DropTable("dbo.Courses");
        }
    }
}
