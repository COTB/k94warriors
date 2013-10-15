namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DogCertifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DogCertifications",
                c => new
                    {
                        DogCertificationID = c.Int(nullable: false, identity: true),
                        DogProfileID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        DateReceived = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.DogCertificationID)
                .ForeignKey("dbo.DogProfiles", t => t.DogProfileID, cascadeDelete: true)
                .Index(t => t.DogProfileID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DogCertifications", new[] { "DogProfileID" });
            DropForeignKey("dbo.DogCertifications", "DogProfileID", "dbo.DogProfiles");
            DropTable("dbo.DogCertifications");
        }
    }
}
