namespace EdubranApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "applicationTime", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "currentDate", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "closingDate", c => c.Int(nullable: false));
            AddColumn("dbo.Feedbacks", "time", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedbacks", "time");
            DropColumn("dbo.Projects", "closingDate");
            DropColumn("dbo.Projects", "currentDate");
            DropColumn("dbo.Applications", "applicationTime");
        }
    }
}
