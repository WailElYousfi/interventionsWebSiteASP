namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update20 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Interventions", "DirectionId", "dbo.Directions");
            DropForeignKey("dbo.Staffs", "DirectionId", "dbo.Directions");
            AddForeignKey("dbo.Interventions", "DirectionId", "dbo.Directions", "DirectionId", cascadeDelete: true);
            AddForeignKey("dbo.Staffs", "DirectionId", "dbo.Directions", "DirectionId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "DirectionId", "dbo.Directions");
            DropForeignKey("dbo.Interventions", "DirectionId", "dbo.Directions");
            AddForeignKey("dbo.Staffs", "DirectionId", "dbo.Directions", "DirectionId");
            AddForeignKey("dbo.Interventions", "DirectionId", "dbo.Directions", "DirectionId");
        }
    }
}
