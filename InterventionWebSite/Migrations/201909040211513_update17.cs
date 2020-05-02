namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Interventions", "AgencyId", "dbo.Agencies");
            DropForeignKey("dbo.Staffs", "AgencyId", "dbo.Agencies");
            AddForeignKey("dbo.Interventions", "AgencyId", "dbo.Agencies", "AgencyId", cascadeDelete: true);
            AddForeignKey("dbo.Staffs", "AgencyId", "dbo.Agencies", "AgencyId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "AgencyId", "dbo.Agencies");
            DropForeignKey("dbo.Interventions", "AgencyId", "dbo.Agencies");
            AddForeignKey("dbo.Staffs", "AgencyId", "dbo.Agencies", "AgencyId");
            AddForeignKey("dbo.Interventions", "AgencyId", "dbo.Agencies", "AgencyId");
        }
    }
}
