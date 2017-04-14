namespace Repo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeestatusrelations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsHired = c.Boolean(nullable: false),
                        ContractType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Status", "Id", "dbo.Employees");
            DropIndex("dbo.Status", new[] { "Id" });
            DropTable("dbo.Status");
            DropTable("dbo.Employees");
        }
    }
}
