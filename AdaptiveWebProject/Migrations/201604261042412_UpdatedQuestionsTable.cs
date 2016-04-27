namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedQuestionsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Questions", new[] { "UserId" });
            AddColumn("dbo.Questions", "Tags", c => c.String());
            AddColumn("dbo.Questions", "Body", c => c.String());
            AddColumn("dbo.Questions", "Title", c => c.String());
            DropColumn("dbo.Questions", "UserId");
            DropColumn("dbo.Questions", "Question");
            DropColumn("dbo.Questions", "isRelevant");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "isRelevant", c => c.Boolean(nullable: false));
            AddColumn("dbo.Questions", "Question", c => c.String());
            AddColumn("dbo.Questions", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.Questions", "Title");
            DropColumn("dbo.Questions", "Body");
            DropColumn("dbo.Questions", "Tags");
            CreateIndex("dbo.Questions", "UserId");
            AddForeignKey("dbo.Questions", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
