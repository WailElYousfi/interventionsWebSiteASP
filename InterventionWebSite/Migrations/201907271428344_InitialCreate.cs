namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agencies",
                c => new
                    {
                        AgencyId = c.Int(nullable: false, identity: true),
                        AgencyName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AgencyId);
            
            CreateTable(
                "dbo.Interventions",
                c => new
                    {
                        InterventionId = c.Int(nullable: false, identity: true),
                        ActionDate = c.String(nullable: false),
                        ClosingDate = c.String(),
                        Nature = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        AgencyId_AgencyId = c.Int(),
                        AssignmentManagerId_UserId = c.Int(),
                        DirectionId_DirectionId = c.Int(),
                        RequestId_RequestId = c.Int(),
                        IntervenerId_UserId = c.Int(),
                        StateId_StateId = c.Int(),
                    })
                .PrimaryKey(t => t.InterventionId)
                .ForeignKey("dbo.Agencies", t => t.AgencyId_AgencyId)
                .ForeignKey("dbo.Users", t => t.AssignmentManagerId_UserId)
                .ForeignKey("dbo.Directions", t => t.DirectionId_DirectionId)
                .ForeignKey("dbo.Requests", t => t.RequestId_RequestId)
                .ForeignKey("dbo.Users", t => t.IntervenerId_UserId)
                .ForeignKey("dbo.States", t => t.StateId_StateId)
                .Index(t => t.AgencyId_AgencyId)
                .Index(t => t.AssignmentManagerId_UserId)
                .Index(t => t.DirectionId_DirectionId)
                .Index(t => t.RequestId_RequestId)
                .Index(t => t.IntervenerId_UserId)
                .Index(t => t.StateId_StateId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(),
                        Password = c.String(maxLength: 40),
                        RoleId_RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId_RoleId)
                .Index(t => t.RoleId_RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Directions",
                c => new
                    {
                        DirectionId = c.Int(nullable: false, identity: true),
                        DirectionName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DirectionId);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        StaffName = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Phone = c.String(),
                        AgencyId_AgencyId = c.Int(),
                        DirectionId_DirectionId = c.Int(),
                    })
                .PrimaryKey(t => t.StaffId)
                .ForeignKey("dbo.Agencies", t => t.AgencyId_AgencyId)
                .ForeignKey("dbo.Directions", t => t.DirectionId_DirectionId)
                .Index(t => t.AgencyId_AgencyId)
                .Index(t => t.DirectionId_DirectionId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        TypeOfCommunication = c.String(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        ReceptionEmployee_UserId = c.Int(),
                        StaffId_StaffId = c.Int(),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Users", t => t.ReceptionEmployee_UserId)
                .ForeignKey("dbo.Staffs", t => t.StaffId_StaffId)
                .Index(t => t.ReceptionEmployee_UserId)
                .Index(t => t.StaffId_StaffId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        StateName = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "dbo.UserUsers",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        User_UserId1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.User_UserId1 })
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Users", t => t.User_UserId1)
                .Index(t => t.User_UserId)
                .Index(t => t.User_UserId1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Interventions", "StateId_StateId", "dbo.States");
            DropForeignKey("dbo.Interventions", "IntervenerId_UserId", "dbo.Users");
            DropForeignKey("dbo.Requests", "StaffId_StaffId", "dbo.Staffs");
            DropForeignKey("dbo.Requests", "ReceptionEmployee_UserId", "dbo.Users");
            DropForeignKey("dbo.Interventions", "RequestId_RequestId", "dbo.Requests");
            DropForeignKey("dbo.Staffs", "DirectionId_DirectionId", "dbo.Directions");
            DropForeignKey("dbo.Staffs", "AgencyId_AgencyId", "dbo.Agencies");
            DropForeignKey("dbo.Interventions", "DirectionId_DirectionId", "dbo.Directions");
            DropForeignKey("dbo.Interventions", "AssignmentManagerId_UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId_RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserUsers", "User_UserId1", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Interventions", "AgencyId_AgencyId", "dbo.Agencies");
            DropIndex("dbo.UserUsers", new[] { "User_UserId1" });
            DropIndex("dbo.UserUsers", new[] { "User_UserId" });
            DropIndex("dbo.Requests", new[] { "StaffId_StaffId" });
            DropIndex("dbo.Requests", new[] { "ReceptionEmployee_UserId" });
            DropIndex("dbo.Staffs", new[] { "DirectionId_DirectionId" });
            DropIndex("dbo.Staffs", new[] { "AgencyId_AgencyId" });
            DropIndex("dbo.Users", new[] { "RoleId_RoleId" });
            DropIndex("dbo.Interventions", new[] { "StateId_StateId" });
            DropIndex("dbo.Interventions", new[] { "IntervenerId_UserId" });
            DropIndex("dbo.Interventions", new[] { "RequestId_RequestId" });
            DropIndex("dbo.Interventions", new[] { "DirectionId_DirectionId" });
            DropIndex("dbo.Interventions", new[] { "AssignmentManagerId_UserId" });
            DropIndex("dbo.Interventions", new[] { "AgencyId_AgencyId" });
            DropTable("dbo.UserUsers");
            DropTable("dbo.States");
            DropTable("dbo.Requests");
            DropTable("dbo.Staffs");
            DropTable("dbo.Directions");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Interventions");
            DropTable("dbo.Agencies");
        }
    }
}
