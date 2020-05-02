namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update5 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Interventions", new[] { "AssignmentManagerId" });
            DropIndex("dbo.Interventions", new[] { "IntervenerId" });
            AlterColumn("dbo.Interventions", "AssignmentManagerId", c => c.Int());
            AlterColumn("dbo.Interventions", "IntervenerId", c => c.Int());
            CreateIndex("dbo.Interventions", "AssignmentManagerId");
            CreateIndex("dbo.Interventions", "IntervenerId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Interventions", new[] { "IntervenerId" });
            DropIndex("dbo.Interventions", new[] { "AssignmentManagerId" });
            AlterColumn("dbo.Interventions", "IntervenerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Interventions", "AssignmentManagerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Interventions", "IntervenerId");
            CreateIndex("dbo.Interventions", "AssignmentManagerId");
        }
    }
}
