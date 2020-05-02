namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update6 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", "IMEIIndex");
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Users", "Email", unique: true, name: "IMEIIndex");
        }
    }
}
