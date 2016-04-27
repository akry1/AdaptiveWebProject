namespace AdaptiveWebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Questions1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "Question", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "Question", c => c.String(maxLength: 1));
        }
    }
}
