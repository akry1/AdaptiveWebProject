namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChaangedPostTables2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "PostId", "dbo.PostDetails");
            DropForeignKey("dbo.Posts", "PostId", "dbo.PostTags");
            DropIndex("dbo.Posts", new[] { "PostId" });
            CreateIndex("dbo.PostDetails", "PostId");
            CreateIndex("dbo.PostTags", "PostId");
            AddForeignKey("dbo.PostDetails", "PostId", "dbo.Posts", "PostId");
            AddForeignKey("dbo.PostTags", "PostId", "dbo.Posts", "PostId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostTags", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostDetails", "PostId", "dbo.Posts");
            DropIndex("dbo.PostTags", new[] { "PostId" });
            DropIndex("dbo.PostDetails", new[] { "PostId" });
            CreateIndex("dbo.Posts", "PostId");
            AddForeignKey("dbo.Posts", "PostId", "dbo.PostTags", "PostId");
            AddForeignKey("dbo.Posts", "PostId", "dbo.PostDetails", "PostId");
        }
    }
}
