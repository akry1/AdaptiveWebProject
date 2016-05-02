namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserNameTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserNames",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNames", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserNames", new[] { "UserId" });
            DropTable("dbo.UserNames");
        }
    }
}
