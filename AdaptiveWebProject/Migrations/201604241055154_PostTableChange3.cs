namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostTableChange3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostDetails",
                c => new
                {
                    Id = c.Int(nullable: false),
                    PostId = c.String(nullable: false, maxLength: 128),
                    ViewCount = c.Int(nullable: false),
                    FiveViewCount = c.Int(nullable: false),
                    DelayedDays = c.Double(nullable: false),
                    Weight = c.Double(nullable: false),
                    Difficulty = c.String(),
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.PostDetails");
        }
    }
}
