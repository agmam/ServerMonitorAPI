namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Threadshandles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServerDetailAverages", "Threads", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ServerDetails", "Threads", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServerDetails", "Threads");
            DropColumn("dbo.ServerDetailAverages", "Threads");
        }
    }
}
