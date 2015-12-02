namespace EdubranApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Applications", new[] { "studentID" });
            AddColumn("dbo.Applications", "companyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Applications", "studentId");
            CreateIndex("dbo.Applications", "companyId");
            AddForeignKey("dbo.Applications", "companyId", "dbo.Companies", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Applications", "companyId", "dbo.Companies");
            DropIndex("dbo.Applications", new[] { "companyId" });
            DropIndex("dbo.Applications", new[] { "studentId" });
            DropColumn("dbo.Applications", "companyId");
            CreateIndex("dbo.Applications", "studentID");
        }
    }
}
