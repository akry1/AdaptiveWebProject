namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTableChange : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "UsersDatas");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UsersDatas", newName: "Users");
        }
    }
}
