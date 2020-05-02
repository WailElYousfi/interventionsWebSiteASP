namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Interventions", "ActionDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Interventions", "ActionDate", c => c.String(nullable: false));
        }
    }
}
