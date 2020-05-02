namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "problems", c => c.String(nullable: false));
            AlterColumn("dbo.Requests", "RequestDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "RequestDate", c => c.String(nullable: false));
            DropColumn("dbo.Requests", "problems");
        }
    }
}
