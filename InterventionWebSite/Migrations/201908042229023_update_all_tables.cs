namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_all_tables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Staffs", "AgencyId", "dbo.Agencies");
            DropForeignKey("dbo.Staffs", "DirectionId", "dbo.Directions");
            DropIndex("dbo.Interventions", new[] { "Agency_AgencyId" });
            DropIndex("dbo.Interventions", new[] { "Request_RequestId" });
            DropIndex("dbo.Interventions", new[] { "Direction_DirectionId" });
            DropIndex("dbo.Interventions", new[] { "AssignmentManager_UserId" });
            DropIndex("dbo.Interventions", new[] { "Intervener_UserId" });
            DropIndex("dbo.Interventions", new[] { "State_StateId" });
            DropIndex("dbo.Users", new[] { "Role_RoleId" });
            DropIndex("dbo.Requests", new[] { "ReceptionEmployee_UserId" });
            DropIndex("dbo.Requests", new[] { "Staff_StaffId" });
            RenameColumn(table: "dbo.Interventions", name: "Agency_AgencyId", newName: "AgencyId");
            RenameColumn(table: "dbo.Interventions", name: "AssignmentManager_UserId", newName: "AssignmentManagerId");
            RenameColumn(table: "dbo.Interventions", name: "Direction_DirectionId", newName: "DirectionId");
            RenameColumn(table: "dbo.Interventions", name: "Intervener_UserId", newName: "IntervenerId");
            RenameColumn(table: "dbo.Interventions", name: "Request_RequestId", newName: "RequestId");
            RenameColumn(table: "dbo.Interventions", name: "State_StateId", newName: "StateId");
            RenameColumn(table: "dbo.Requests", name: "ReceptionEmployee_UserId", newName: "ReceptionEmployeeId");
            RenameColumn(table: "dbo.Users", name: "Role_RoleId", newName: "RoleId");
            RenameColumn(table: "dbo.Requests", name: "Staff_StaffId", newName: "StaffId");
            AlterColumn("dbo.Interventions", "AgencyId", c => c.Int(nullable: false));
            AlterColumn("dbo.Interventions", "RequestId", c => c.Int(nullable: false));
            AlterColumn("dbo.Interventions", "DirectionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Interventions", "AssignmentManagerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Interventions", "IntervenerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Interventions", "StateId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.Requests", "ReceptionEmployeeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Requests", "StaffId", c => c.Int(nullable: false));
            CreateIndex("dbo.Interventions", "StateId");
            CreateIndex("dbo.Interventions", "AgencyId");
            CreateIndex("dbo.Interventions", "DirectionId");
            CreateIndex("dbo.Interventions", "RequestId");
            CreateIndex("dbo.Interventions", "AssignmentManagerId");
            CreateIndex("dbo.Interventions", "IntervenerId");
            CreateIndex("dbo.Users", "RoleId");
            CreateIndex("dbo.Requests", "StaffId");
            CreateIndex("dbo.Requests", "ReceptionEmployeeId");
            AddForeignKey("dbo.Staffs", "AgencyId", "dbo.Agencies", "AgencyId");
            AddForeignKey("dbo.Staffs", "DirectionId", "dbo.Directions", "DirectionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "DirectionId", "dbo.Directions");
            DropForeignKey("dbo.Staffs", "AgencyId", "dbo.Agencies");
            DropIndex("dbo.Requests", new[] { "ReceptionEmployeeId" });
            DropIndex("dbo.Requests", new[] { "StaffId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Interventions", new[] { "IntervenerId" });
            DropIndex("dbo.Interventions", new[] { "AssignmentManagerId" });
            DropIndex("dbo.Interventions", new[] { "RequestId" });
            DropIndex("dbo.Interventions", new[] { "DirectionId" });
            DropIndex("dbo.Interventions", new[] { "AgencyId" });
            DropIndex("dbo.Interventions", new[] { "StateId" });
            AlterColumn("dbo.Requests", "StaffId", c => c.Int());
            AlterColumn("dbo.Requests", "ReceptionEmployeeId", c => c.Int());
            AlterColumn("dbo.Users", "RoleId", c => c.Int());
            AlterColumn("dbo.Interventions", "StateId", c => c.Int());
            AlterColumn("dbo.Interventions", "IntervenerId", c => c.Int());
            AlterColumn("dbo.Interventions", "AssignmentManagerId", c => c.Int());
            AlterColumn("dbo.Interventions", "DirectionId", c => c.Int());
            AlterColumn("dbo.Interventions", "RequestId", c => c.Int());
            AlterColumn("dbo.Interventions", "AgencyId", c => c.Int());
            RenameColumn(table: "dbo.Requests", name: "StaffId", newName: "Staff_StaffId");
            RenameColumn(table: "dbo.Users", name: "RoleId", newName: "Role_RoleId");
            RenameColumn(table: "dbo.Requests", name: "ReceptionEmployeeId", newName: "ReceptionEmployee_UserId");
            RenameColumn(table: "dbo.Interventions", name: "StateId", newName: "State_StateId");
            RenameColumn(table: "dbo.Interventions", name: "RequestId", newName: "Request_RequestId");
            RenameColumn(table: "dbo.Interventions", name: "IntervenerId", newName: "Intervener_UserId");
            RenameColumn(table: "dbo.Interventions", name: "DirectionId", newName: "Direction_DirectionId");
            RenameColumn(table: "dbo.Interventions", name: "AssignmentManagerId", newName: "AssignmentManager_UserId");
            RenameColumn(table: "dbo.Interventions", name: "AgencyId", newName: "Agency_AgencyId");
            CreateIndex("dbo.Requests", "Staff_StaffId");
            CreateIndex("dbo.Requests", "ReceptionEmployee_UserId");
            CreateIndex("dbo.Users", "Role_RoleId");
            CreateIndex("dbo.Interventions", "State_StateId");
            CreateIndex("dbo.Interventions", "Intervener_UserId");
            CreateIndex("dbo.Interventions", "AssignmentManager_UserId");
            CreateIndex("dbo.Interventions", "Direction_DirectionId");
            CreateIndex("dbo.Interventions", "Request_RequestId");
            CreateIndex("dbo.Interventions", "Agency_AgencyId");
            AddForeignKey("dbo.Staffs", "DirectionId", "dbo.Directions", "DirectionId", cascadeDelete: true);
            AddForeignKey("dbo.Staffs", "AgencyId", "dbo.Agencies", "AgencyId", cascadeDelete: true);
        }
    }
}
