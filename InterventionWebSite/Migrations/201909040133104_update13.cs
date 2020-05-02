namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Interventions", "RequestId", "dbo.Requests");
            DropIndex("dbo.Interventions", new[] { "RequestId" });
            AlterColumn("dbo.Interventions", "RequestId", c => c.Int(nullable: false));
            CreateIndex("dbo.Interventions", "RequestId");
            AddForeignKey("dbo.Interventions", "RequestId", "dbo.Requests", "RequestId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Interventions", "RequestId", "dbo.Requests");
            DropIndex("dbo.Interventions", new[] { "RequestId" });
            AlterColumn("dbo.Interventions", "RequestId", c => c.Int());
            CreateIndex("dbo.Interventions", "RequestId");
            AddForeignKey("dbo.Interventions", "RequestId", "dbo.Requests", "RequestId");
        }
    }
}
