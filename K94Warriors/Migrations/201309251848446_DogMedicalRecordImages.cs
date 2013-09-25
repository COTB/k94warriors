namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DogMedicalRecordImages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DogMedicalRecordImages",
                c => new
                    {
                        DogImageID = c.Int(nullable: false, identity: true),
                        DogMedicalRecordID = c.Int(nullable: false),
                        BlobKey = c.Guid(nullable: false),
                        MimeType = c.String(),
                        DogMedicalRecord_RecordID = c.Int(),
                    })
                .PrimaryKey(t => t.DogImageID)
                .ForeignKey("dbo.DogMedicalRecords", t => t.DogMedicalRecord_RecordID)
                .Index(t => t.DogMedicalRecord_RecordID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DogMedicalRecordImages", new[] { "DogMedicalRecord_RecordID" });
            DropForeignKey("dbo.DogMedicalRecordImages", "DogMedicalRecord_RecordID", "dbo.DogMedicalRecords");
            DropTable("dbo.DogMedicalRecordImages");
        }
    }
}
