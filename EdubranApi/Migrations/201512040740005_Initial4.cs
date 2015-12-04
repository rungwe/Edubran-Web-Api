namespace EdubranApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Feedbacks", "companyId", "dbo.Companies");
            DropForeignKey("dbo.Feedbacks", "studentId", "dbo.Students");
            DropIndex("dbo.Feedbacks", new[] { "studentId" });
            DropIndex("dbo.Feedbacks", new[] { "companyId" });
            AddColumn("dbo.Feedbacks", "clientId", c => c.Int(nullable: false));
            AddColumn("dbo.Feedbacks", "name", c => c.String(nullable: false));
            AddColumn("dbo.Feedbacks", "profile_picture", c => c.String());
            AddColumn("dbo.Feedbacks", "type", c => c.String(nullable: false));
            DropColumn("dbo.Feedbacks", "studentId");
            DropColumn("dbo.Feedbacks", "companyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Feedbacks", "companyId", c => c.Int(nullable: false));
            AddColumn("dbo.Feedbacks", "studentId", c => c.Int(nullable: false));
            DropColumn("dbo.Feedbacks", "type");
            DropColumn("dbo.Feedbacks", "profile_picture");
            DropColumn("dbo.Feedbacks", "name");
            DropColumn("dbo.Feedbacks", "clientId");
            CreateIndex("dbo.Feedbacks", "companyId");
            CreateIndex("dbo.Feedbacks", "studentId");
            AddForeignKey("dbo.Feedbacks", "studentId", "dbo.Students", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Feedbacks", "companyId", "dbo.Companies", "Id", cascadeDelete: true);
        }
    }
}
