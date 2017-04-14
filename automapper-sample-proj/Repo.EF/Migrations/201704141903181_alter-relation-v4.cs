namespace Repo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterrelationv4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Status", "Employee_Id", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Status", "Employee_Id");
        }
    }
}
