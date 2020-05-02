namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update22 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Interventions", "AssignmentManagerId", "dbo.Users");
            DropForeignKey("dbo.Requests", "ReceptionEmployeeId", "dbo.Users");
            AddForeignKey("dbo.Interventions", "AssignmentManagerId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Requests", "ReceptionEmployeeId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "ReceptionEmployeeId", "dbo.Users");
            DropForeignKey("dbo.Interventions", "AssignmentManagerId", "dbo.Users");
            AddForeignKey("dbo.Requests", "ReceptionEmployeeId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Interventions", "AssignmentManagerId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
