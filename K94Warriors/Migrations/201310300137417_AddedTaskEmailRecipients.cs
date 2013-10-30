namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTaskEmailRecipients : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskEmailRecipients",
                c => new
                    {
                        TaskEmailRecipientID = c.Int(nullable: false, identity: true),
                        TaskType = c.Int(nullable: false),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.TaskEmailRecipientID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TaskEmailRecipients");
        }
    }
}
