namespace Repo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterrelationv5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Status", "Id", "dbo.Employees");
            DropIndex("dbo.Status", new[] { "Id" });
            DropPrimaryKey("dbo.Status");
            AddColumn("dbo.Status", "EmployeeId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Status", "EmployeeId");
            CreateIndex("dbo.Status", "EmployeeId");
            AddForeignKey("dbo.Status", "EmployeeId", "dbo.Employees", "Id");
            DropColumn("dbo.Status", "Employee_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Status", "Employee_Id", c => c.Long(nullable: false));
            DropForeignKey("dbo.Status", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Status", new[] { "EmployeeId" });
            DropPrimaryKey("dbo.Status");
            DropColumn("dbo.Status", "EmployeeId");
            AddPrimaryKey("dbo.Status", "Id");
            CreateIndex("dbo.Status", "Id");
            AddForeignKey("dbo.Status", "Id", "dbo.Employees", "Id");
        }
    }
}
