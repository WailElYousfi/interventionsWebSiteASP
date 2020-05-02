namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update14 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Interventions", new[] { "RequestId" });
            AlterColumn("dbo.Interventions", "RequestId", c => c.Int());
            CreateIndex("dbo.Interventions", "RequestId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Interventions", new[] { "RequestId" });
            AlterColumn("dbo.Interventions", "RequestId", c => c.Int(nullable: false));
            CreateIndex("dbo.Interventions", "RequestId");
        }
    }
}
