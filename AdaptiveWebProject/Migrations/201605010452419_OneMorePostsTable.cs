namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneMorePostsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostTagsImps",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        Tags = c.String(),
                    })
                .PrimaryKey(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PostTagsImps");
        }
    }
}
