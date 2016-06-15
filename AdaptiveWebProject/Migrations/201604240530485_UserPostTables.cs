namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPostTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostDifficulties",
                c => new
                    {
                        PostId = c.String(nullable: false, maxLength: 128),
                        ViewCount = c.Int(nullable: false),
                        FiveViewCount = c.Int(nullable: false),
                        DelayedDays = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        Difficulty = c.String(),
                    })
                .PrimaryKey(t => t.PostId);
            
            CreateTable(
                "dbo.PostTags",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        Tags = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ExpertiseLevel = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Users", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.PostTags");
            DropTable("dbo.PostDifficulties");
        }
    }
}
