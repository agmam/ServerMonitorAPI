namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventChanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "ServerDetailId", "dbo.ServerDetails");
            DropIndex("dbo.Events", new[] { "ServerDetailId" });
            AddColumn("dbo.Events", "ServerDetailAverageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "ServerDetailAverageId");
            AddForeignKey("dbo.Events", "ServerDetailAverageId", "dbo.ServerDetailAverages", "Id");
            DropColumn("dbo.Events", "ServerDetailId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "ServerDetailId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Events", "ServerDetailAverageId", "dbo.ServerDetailAverages");
            DropIndex("dbo.Events", new[] { "ServerDetailAverageId" });
            DropColumn("dbo.Events", "ServerDetailAverageId");
            CreateIndex("dbo.Events", "ServerDetailId");
            AddForeignKey("dbo.Events", "ServerDetailId", "dbo.ServerDetails", "Id");
        }
    }
}
