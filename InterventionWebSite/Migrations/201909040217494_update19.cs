namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update19 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Interventions", "StateId", "dbo.States");
            AddForeignKey("dbo.Interventions", "StateId", "dbo.States", "StateId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Interventions", "StateId", "dbo.States");
            AddForeignKey("dbo.Interventions", "StateId", "dbo.States", "StateId");
        }
    }
}
