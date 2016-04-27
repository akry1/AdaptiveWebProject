namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostsTableChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Data", c => c.String());
            DropColumn("dbo.Posts", "Tags");
            DropColumn("dbo.Posts", "Body");
            DropColumn("dbo.Posts", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Title", c => c.String());
            AddColumn("dbo.Posts", "Body", c => c.String());
            AddColumn("dbo.Posts", "Tags", c => c.String());
            DropColumn("dbo.Posts", "Data");
        }
    }
}
