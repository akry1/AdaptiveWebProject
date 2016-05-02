namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFKTOIMP : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PostTagsImps", "PostId");
            AddForeignKey("dbo.PostTagsImps", "PostId", "dbo.Posts", "PostId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostTagsImps", "PostId", "dbo.Posts");
            DropIndex("dbo.PostTagsImps", new[] { "PostId" });
        }
    }
}
