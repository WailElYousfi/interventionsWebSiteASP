namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update18 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Interventions", "AssignmentManagerId", "dbo.Users");
            AddForeignKey("dbo.Interventions", "AssignmentManagerId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Interventions", "AssignmentManagerId", "dbo.Users");
            AddForeignKey("dbo.Interventions", "AssignmentManagerId", "dbo.Users", "UserId");
        }
    }
}
