namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewUserTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSatisfactions",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        SatisfactionAnswer = c.String(),
                        SatisfactionDifficulty = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserTags",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Tags = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTags", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserSatisfactions", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserTags", new[] { "UserId" });
            DropIndex("dbo.UserSatisfactions", new[] { "UserId" });
            DropTable("dbo.UserTags");
            DropTable("dbo.UserSatisfactions");
        }
    }
}
