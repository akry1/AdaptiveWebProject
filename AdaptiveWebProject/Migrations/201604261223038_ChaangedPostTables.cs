namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChaangedPostTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostDetails",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        ViewCount = c.Int(),
                        FiveViewCount = c.Int(),
                        DelayedDays = c.Double(),
                        Weight = c.Double(),
                        Difficulty = c.String(),
                    })
                .PrimaryKey(t => t.PostId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        Tags = c.String(),
                        Body = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.PostDetails", t => t.PostId)
                .ForeignKey("dbo.PostTags", t => t.PostId)
                .Index(t => t.PostId);
            
            CreateTable(
                "dbo.PostTags",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        Tags = c.String(),
                    })
                .PrimaryKey(t => t.PostId);
            
            DropTable("dbo.Questions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Tags = c.String(),
                        Body = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Posts", "PostId", "dbo.PostTags");
            DropForeignKey("dbo.Posts", "PostId", "dbo.PostDetails");
            DropIndex("dbo.Posts", new[] { "PostId" });
            DropTable("dbo.PostTags");
            DropTable("dbo.Posts");
            DropTable("dbo.PostDetails");
        }
    }
}
