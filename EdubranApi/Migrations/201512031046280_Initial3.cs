namespace EdubranApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        comment = c.String(nullable: false),
                        date = c.String(),
                        projectId = c.Int(nullable: false),
                        studentId = c.Int(nullable: true),
                        companyId = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.companyId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.studentId, cascadeDelete: true)
                .Index(t => t.studentId)
                .Index(t => t.companyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Feedbacks", "studentId", "dbo.Students");
            DropForeignKey("dbo.Feedbacks", "companyId", "dbo.Companies");
            DropIndex("dbo.Feedbacks", new[] { "companyId" });
            DropIndex("dbo.Feedbacks", new[] { "studentId" });
            DropTable("dbo.Feedbacks");
        }
    }
}
