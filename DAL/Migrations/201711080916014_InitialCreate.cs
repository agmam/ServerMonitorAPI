namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailRecipients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        ServerId = c.Int(nullable: false),
                        EventTypeId = c.Int(nullable: false),
                        ServerDetailId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EventTypes", t => t.EventTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Servers", t => t.ServerId, cascadeDelete: true)
                .ForeignKey("dbo.ServerDetails", t => t.ServerDetailId)
                .Index(t => t.ServerId)
                .Index(t => t.EventTypeId)
                .Index(t => t.ServerDetailId);
            
            CreateTable(
                "dbo.EventTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Name = c.String(),
                        ShouldNotify = c.Boolean(nullable: false),
                        PeakValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RiskEstimate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Servers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        ServerName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServerDetailAverages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        ServerId = c.Int(nullable: false),
                        CPUUtilization = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RAMAvailable = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RAMTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UpTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BytesReceived = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BytesSent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Handles = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Processes = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Temperature = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Servers", t => t.ServerId, cascadeDelete: true)
                .Index(t => t.ServerId);
            
            CreateTable(
                "dbo.ServerDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        ServerId = c.Int(nullable: false),
                        CPUUtilization = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RAMAvailable = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RAMTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UpTime = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BytesReceived = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BytesSent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Handles = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Processes = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Temperature = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Servers", t => t.ServerId, cascadeDelete: true)
                .Index(t => t.ServerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "ServerDetailId", "dbo.ServerDetails");
            DropForeignKey("dbo.ServerDetails", "ServerId", "dbo.Servers");
            DropForeignKey("dbo.ServerDetailAverages", "ServerId", "dbo.Servers");
            DropForeignKey("dbo.Events", "ServerId", "dbo.Servers");
            DropForeignKey("dbo.Events", "EventTypeId", "dbo.EventTypes");
            DropIndex("dbo.ServerDetails", new[] { "ServerId" });
            DropIndex("dbo.ServerDetailAverages", new[] { "ServerId" });
            DropIndex("dbo.Events", new[] { "ServerDetailId" });
            DropIndex("dbo.Events", new[] { "EventTypeId" });
            DropIndex("dbo.Events", new[] { "ServerId" });
            DropTable("dbo.ServerDetails");
            DropTable("dbo.ServerDetailAverages");
            DropTable("dbo.Servers");
            DropTable("dbo.EventTypes");
            DropTable("dbo.Events");
            DropTable("dbo.EmailRecipients");
        }
    }
}
