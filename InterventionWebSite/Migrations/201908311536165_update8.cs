namespace InterventionWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_UserId1", "dbo.Users");
            DropIndex("dbo.UserUsers", new[] { "User_UserId" });
            DropIndex("dbo.UserUsers", new[] { "User_UserId1" });
            DropTable("dbo.UserUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserUsers",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        User_UserId1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.User_UserId1 });
            
            CreateIndex("dbo.UserUsers", "User_UserId1");
            CreateIndex("dbo.UserUsers", "User_UserId");
            AddForeignKey("dbo.UserUsers", "User_UserId1", "dbo.Users", "UserId");
            AddForeignKey("dbo.UserUsers", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
