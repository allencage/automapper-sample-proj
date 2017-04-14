namespace Repo.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterrelationv3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Status", new[] { "Id" });
            DropPrimaryKey("dbo.Status");
            AlterColumn("dbo.Status", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Status", "Id");
            CreateIndex("dbo.Status", "Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Status", new[] { "Id" });
            DropPrimaryKey("dbo.Status");
            AlterColumn("dbo.Status", "Id", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Status", "Id");
            CreateIndex("dbo.Status", "Id");
        }
    }
}
