namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostTableChange4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PostDetails", "ViewCount", c => c.Int());
            AlterColumn("dbo.PostDetails", "FiveViewCount", c => c.Int());
            AlterColumn("dbo.PostDetails", "DelayedDays", c => c.Double());
            AlterColumn("dbo.PostDetails", "Weight", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PostDetails", "Weight", c => c.Double(nullable: false));
            AlterColumn("dbo.PostDetails", "DelayedDays", c => c.Double(nullable: false));
            AlterColumn("dbo.PostDetails", "FiveViewCount", c => c.Int(nullable: false));
            AlterColumn("dbo.PostDetails", "ViewCount", c => c.Int(nullable: false));
        }
    }
}
