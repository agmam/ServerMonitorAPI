namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Emailrecipient : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmailRecipients", "EmailRecipient_Id", "dbo.EmailRecipients");
            DropIndex("dbo.EmailRecipients", new[] { "EmailRecipient_Id" });
            AddColumn("dbo.EmailRecipients", "Email", c => c.String());
            DropColumn("dbo.EmailRecipients", "EmailRecipient_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmailRecipients", "EmailRecipient_Id", c => c.Int());
            DropColumn("dbo.EmailRecipients", "Email");
            CreateIndex("dbo.EmailRecipients", "EmailRecipient_Id");
            AddForeignKey("dbo.EmailRecipients", "EmailRecipient_Id", "dbo.EmailRecipients", "Id");
        }
    }
}
