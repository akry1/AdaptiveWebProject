namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateToStrings : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserAcceptedAnswers", "AcceptedAnswers", c => c.String());
            AlterColumn("dbo.UserUpvotes", "Upvotes", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserUpvotes", "Upvotes", c => c.Int(nullable: false));
            AlterColumn("dbo.UserAcceptedAnswers", "AcceptedAnswers", c => c.Int(nullable: false));
        }
    }
}
