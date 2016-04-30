namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePostsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Tags", c => c.String());
            AddColumn("dbo.Posts", "Question", c => c.String());
            DropColumn("dbo.Posts", "Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Data", c => c.String());
            DropColumn("dbo.Posts", "Question");
            DropColumn("dbo.Posts", "Tags");
        }
    }
}
