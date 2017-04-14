namespace Repo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterrelationv2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "Id", "dbo.Status");
            DropIndex("dbo.Employees", new[] { "Id" });
            DropPrimaryKey("dbo.Status");
            AlterColumn("dbo.Status", "Id", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Status", "Id");
            CreateIndex("dbo.Status", "Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Status", new[] { "Id" });
            DropPrimaryKey("dbo.Status");
            AlterColumn("dbo.Status", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Status", "Id");
            CreateIndex("dbo.Employees", "Id");
            AddForeignKey("dbo.Employees", "Id", "dbo.Status", "Id");
        }
    }
}
