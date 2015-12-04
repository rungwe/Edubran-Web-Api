namespace EdubranApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "projectId", "dbo.Projects");
            DropIndex("dbo.Comments", new[] { "projectId" });
            DropTable("dbo.Comments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        registrationId = c.String(),
                        comment = c.String(),
                        date = c.String(),
                        projectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Comments", "projectId");
            AddForeignKey("dbo.Comments", "projectId", "dbo.Projects", "Id", cascadeDelete: true);
        }
    }
}
