namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update12 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Interventions", new[] { "StateId" });
            DropIndex("dbo.Interventions", new[] { "AgencyId" });
            DropIndex("dbo.Interventions", new[] { "DirectionId" });
            DropIndex("dbo.Interventions", new[] { "RequestId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Requests", new[] { "StaffId" });
            DropIndex("dbo.Requests", new[] { "ReceptionEmployeeId" });
            DropIndex("dbo.Staffs", new[] { "AgencyId" });
            DropIndex("dbo.Staffs", new[] { "DirectionId" });
            AlterColumn("dbo.Interventions", "StateId", c => c.Int());
            AlterColumn("dbo.Interventions", "AgencyId", c => c.Int());
            AlterColumn("dbo.Interventions", "DirectionId", c => c.Int());
            AlterColumn("dbo.Interventions", "RequestId", c => c.Int());
            AlterColumn("dbo.Users", "RoleId", c => c.Int());
            AlterColumn("dbo.Requests", "StaffId", c => c.Int());
            AlterColumn("dbo.Requests", "ReceptionEmployeeId", c => c.Int());
            AlterColumn("dbo.Staffs", "AgencyId", c => c.Int());
            AlterColumn("dbo.Staffs", "DirectionId", c => c.Int());
            CreateIndex("dbo.Interventions", "StateId");
            CreateIndex("dbo.Interventions", "AgencyId");
            CreateIndex("dbo.Interventions", "DirectionId");
            CreateIndex("dbo.Interventions", "RequestId");
            CreateIndex("dbo.Users", "RoleId");
            CreateIndex("dbo.Requests", "StaffId");
            CreateIndex("dbo.Requests", "ReceptionEmployeeId");
            CreateIndex("dbo.Staffs", "AgencyId");
            CreateIndex("dbo.Staffs", "DirectionId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Staffs", new[] { "DirectionId" });
            DropIndex("dbo.Staffs", new[] { "AgencyId" });
            DropIndex("dbo.Requests", new[] { "ReceptionEmployeeId" });
            DropIndex("dbo.Requests", new[] { "StaffId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Interventions", new[] { "RequestId" });
            DropIndex("dbo.Interventions", new[] { "DirectionId" });
            DropIndex("dbo.Interventions", new[] { "AgencyId" });
            DropIndex("dbo.Interventions", new[] { "StateId" });
            AlterColumn("dbo.Staffs", "DirectionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Staffs", "AgencyId", c => c.Int(nullable: false));
            AlterColumn("dbo.Requests", "ReceptionEmployeeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Requests", "StaffId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.Interventions", "RequestId", c => c.Int(nullable: false));
            AlterColumn("dbo.Interventions", "DirectionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Interventions", "AgencyId", c => c.Int(nullable: false));
            AlterColumn("dbo.Interventions", "StateId", c => c.Int(nullable: false));
            CreateIndex("dbo.Staffs", "DirectionId");
            CreateIndex("dbo.Staffs", "AgencyId");
            CreateIndex("dbo.Requests", "ReceptionEmployeeId");
            CreateIndex("dbo.Requests", "StaffId");
            CreateIndex("dbo.Users", "RoleId");
            CreateIndex("dbo.Interventions", "RequestId");
            CreateIndex("dbo.Interventions", "DirectionId");
            CreateIndex("dbo.Interventions", "AgencyId");
            CreateIndex("dbo.Interventions", "StateId");
        }
    }
}
