namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class harddisk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServerDetailAverages", "HarddiskUsedSpace", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ServerDetailAverages", "HarddiskTotalSpace", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ServerDetails", "HarddiskUsedSpace", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ServerDetails", "HarddiskTotalSpace", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServerDetails", "HarddiskTotalSpace");
            DropColumn("dbo.ServerDetails", "HarddiskUsedSpace");
            DropColumn("dbo.ServerDetailAverages", "HarddiskTotalSpace");
            DropColumn("dbo.ServerDetailAverages", "HarddiskUsedSpace");
        }
    }
}
