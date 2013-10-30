namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTaskTypeToEnum : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskEmailRecipients", "TaskTypeID", "dbo.TaskTypes");
            DropIndex("dbo.TaskEmailRecipients", new[] { "TaskTypeID" });
            DropTable("dbo.TaskTypes");
            DropTable("dbo.TaskEmailRecipients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TaskEmailRecipients",
                c => new
                    {
                        TaskEmailRecipientID = c.Int(nullable: false, identity: true),
                        TaskTypeID = c.Int(nullable: false),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.TaskEmailRecipientID);
            
            CreateTable(
                "dbo.TaskTypes",
                c => new
                    {
                        TaskTypeID = c.Int(nullable: false, identity: true),
                        TaskTypeKey = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TaskTypeID);
            
            CreateIndex("dbo.TaskEmailRecipients", "TaskTypeID");
            AddForeignKey("dbo.TaskEmailRecipients", "TaskTypeID", "dbo.TaskTypes", "TaskTypeID", cascadeDelete: true);
        }
    }
}
