namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskTypesAndEmailRecipients : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskTypes",
                c => new
                    {
                        TaskTypeID = c.Int(nullable: false, identity: true),
                        TaskTypeKey = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TaskTypeID);
            
            CreateTable(
                "dbo.TaskEmailRecipients",
                c => new
                    {
                        TaskEmailRecipientID = c.Int(nullable: false, identity: true),
                        TaskTypeID = c.Int(nullable: false),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.TaskEmailRecipientID)
                .ForeignKey("dbo.TaskTypes", t => t.TaskTypeID, cascadeDelete: true)
                .Index(t => t.TaskTypeID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TaskEmailRecipients", new[] { "TaskTypeID" });
            DropForeignKey("dbo.TaskEmailRecipients", "TaskTypeID", "dbo.TaskTypes");
            DropTable("dbo.TaskEmailRecipients");
            DropTable("dbo.TaskTypes");
        }
    }
}
