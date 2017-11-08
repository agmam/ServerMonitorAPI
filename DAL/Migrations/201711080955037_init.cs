namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServerDetailAverages", "NetworkUtilization", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ServerDetails", "NetworkUtilization", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServerDetails", "NetworkUtilization");
            DropColumn("dbo.ServerDetailAverages", "NetworkUtilization");
        }
    }
}
