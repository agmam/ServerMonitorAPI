namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingerrors : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailRecipients", "EmailRecipient_Id", c => c.Int());
            CreateIndex("dbo.EmailRecipients", "EmailRecipient_Id");
            AddForeignKey("dbo.EmailRecipients", "EmailRecipient_Id", "dbo.EmailRecipients", "Id");
            DropColumn("dbo.EmailRecipients", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmailRecipients", "Email", c => c.String());
            DropForeignKey("dbo.EmailRecipients", "EmailRecipient_Id", "dbo.EmailRecipients");
            DropIndex("dbo.EmailRecipients", new[] { "EmailRecipient_Id" });
            DropColumn("dbo.EmailRecipients", "EmailRecipient_Id");
        }
    }
}
