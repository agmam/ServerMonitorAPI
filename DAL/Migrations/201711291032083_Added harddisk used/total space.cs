namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedharddiskusedtotalspace : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServerDetails", "HarddiskUsedSpace", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ServerDetails", "HarddiskTotalSpace", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServerDetails", "HarddiskTotalSpace");
            DropColumn("dbo.ServerDetails", "HarddiskUsedSpace");
        }
    }
}
