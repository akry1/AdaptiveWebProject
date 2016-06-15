namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPostDetailsTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.PostDetails");
            AlterColumn("dbo.PostDetails", "PostId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PostDetails", "PostId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PostDetails");
            AlterColumn("dbo.PostDetails", "PostId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.PostDetails", "PostId");
        }
    }
}
