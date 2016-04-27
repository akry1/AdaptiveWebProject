namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChaangedPostTables3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostDetails", "PostId", "dbo.Posts");
            DropPrimaryKey("dbo.PostDetails");
            AddColumn("dbo.PostDetails", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PostDetails", "Id");
            AddForeignKey("dbo.PostDetails", "PostId", "dbo.Posts", "PostId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostDetails", "PostId", "dbo.Posts");
            DropPrimaryKey("dbo.PostDetails");
            DropColumn("dbo.PostDetails", "Id");
            AddPrimaryKey("dbo.PostDetails", "PostId");
            AddForeignKey("dbo.PostDetails", "PostId", "dbo.Posts", "PostId");
        }
    }
}
