namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedicalRecordTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedicalRecordTypes",
                c => new
                    {
                        MedicalRecordTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.MedicalRecordTypeID);

            // must seed here for AddColumn below to succeed
            this.Sql("INSERT INTO dbo.MedicalRecordTypes (Name) SELECT 'Miscellaneous' WHERE NOT EXISTS (SELECT 1 FROM dbo.MedicalRecordTypes WHERE Name = 'Miscellaneous')"); 

            AddColumn("dbo.DogMedicalRecords", "MedicalRecordTypeID", c => c.Int(nullable: false, defaultValue: 1));
            AddForeignKey("dbo.DogMedicalRecords", "MedicalRecordTypeID", "dbo.MedicalRecordTypes", "MedicalRecordTypeID", cascadeDelete: true);
            CreateIndex("dbo.DogMedicalRecords", "MedicalRecordTypeID");
            DropColumn("dbo.DogMedicalRecords", "RecordType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DogMedicalRecords", "RecordType", c => c.Int(nullable: false));
            DropIndex("dbo.DogMedicalRecords", new[] { "MedicalRecordTypeID" });
            DropForeignKey("dbo.DogMedicalRecords", "MedicalRecordTypeID", "dbo.MedicalRecordTypes");
            DropColumn("dbo.DogMedicalRecords", "MedicalRecordTypeID");
            DropTable("dbo.MedicalRecordTypes");
        }
    }
}
