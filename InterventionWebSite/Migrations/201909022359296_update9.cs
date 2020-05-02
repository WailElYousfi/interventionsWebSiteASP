namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Interventions", "ActionDate", c => c.DateTime());
            AlterColumn("dbo.Interventions", "ClosingDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Interventions", "ClosingDate", c => c.String());
            AlterColumn("dbo.Interventions", "ActionDate", c => c.String());
        }
    }
}
