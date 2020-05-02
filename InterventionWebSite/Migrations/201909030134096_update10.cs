namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Requests", "RequestDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "RequestDate", c => c.String());
        }
    }
}
