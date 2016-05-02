namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewUserTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAcceptedAnswers",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        AcceptedAnswers = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserUpvotes",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Upvotes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserUpvotes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserAcceptedAnswers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserUpvotes", new[] { "UserId" });
            DropIndex("dbo.UserAcceptedAnswers", new[] { "UserId" });
            DropTable("dbo.UserUpvotes");
            DropTable("dbo.UserAcceptedAnswers");
        }
    }
}
