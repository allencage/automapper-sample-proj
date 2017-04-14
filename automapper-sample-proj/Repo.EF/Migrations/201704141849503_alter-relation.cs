namespace Repo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterrelation : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Status", new[] { "Id" });
            CreateIndex("dbo.Employees", "Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Employees", new[] { "Id" });
            CreateIndex("dbo.Status", "Id");
        }
    }
}
