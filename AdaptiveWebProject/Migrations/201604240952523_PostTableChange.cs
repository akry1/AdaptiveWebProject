namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostTableChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PostTags", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PostTags", "Tags", c => c.Int(nullable: false));
        }
    }
}
