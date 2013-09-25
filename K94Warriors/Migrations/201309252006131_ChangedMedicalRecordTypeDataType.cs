namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedMedicalRecordTypeDataType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DogMedicalRecords", "RecordType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DogMedicalRecords", "RecordType", c => c.String(maxLength: 200));
        }
    }
}
