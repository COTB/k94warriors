namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DogMedications : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DogProfiles", "WarriorID", "dbo.WarriorInfo");
            DropForeignKey("dbo.Users", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.DogImages", "DogProfile_ProfileID", "dbo.DogProfiles");
            DropIndex("dbo.DogProfiles", new[] { "WarriorID" });
            DropIndex("dbo.Users", new[] { "UserTypeId" });
            DropIndex("dbo.DogImages", new[] { "DogProfile_ProfileID" });
            DropColumn("dbo.DogImages", "DogProfile_ProfileID");
            CreateTable(
                "dbo.DogMedications",
                c => new
                    {
                        DogMedicationID = c.Int(nullable: false, identity: true),
                        DogProfileID = c.Int(nullable: false),
                        MedicationName = c.String(nullable: false, maxLength: 100),
                        AmountDescription = c.String(nullable: false, maxLength: 100),
                        VetNotes = c.String(),
                        AMDose = c.Boolean(nullable: false),
                        NoonDose = c.Boolean(nullable: false),
                        PMDose = c.Boolean(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DogMedicationID)
                .ForeignKey("dbo.DogProfiles", t => t.DogProfileID, cascadeDelete: true)
                .Index(t => t.DogProfileID);
            
            AlterColumn("dbo.DogProfiles", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.DogProfiles", "Breed", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.DogProfiles", "Color", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Users", "UserTypeID", c => c.Int(nullable: false));
            AlterColumn("dbo.DogSkillNames", "Name", c => c.String());
            AddForeignKey("dbo.Users", "UserTypeID", "dbo.UserTypes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.DogImages", "DogProfileID", "dbo.DogProfiles", "ProfileID", cascadeDelete: true);
            CreateIndex("dbo.Users", "UserTypeID");
            CreateIndex("dbo.DogImages", "DogProfileID");
            DropColumn("dbo.DogProfiles", "WarriorID");
            DropTable("dbo.WarriorInfo");
            DropTable("dbo.DogEventsReport");
            DropTable("dbo.DogNotesReport");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DogNotesReport",
                c => new
                    {
                        NoteID = c.Int(nullable: false),
                        DogProfileID = c.Int(nullable: false),
                        IsCritical = c.Boolean(nullable: false),
                        IsFixed = c.Boolean(nullable: false),
                        CreatedTimeUTC = c.DateTime(nullable: false),
                        CreatedByUserID = c.Guid(nullable: false),
                        Note = c.String(),
                        Name = c.String(maxLength: 200),
                        Breed = c.String(maxLength: 200),
                        Age = c.Int(),
                        Color = c.String(maxLength: 200),
                        PickedUpDate = c.DateTime(),
                        DonorID = c.Int(),
                        GraduationDate = c.DateTime(),
                        SponsorID = c.Int(),
                        WarriorID = c.Int(),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.NoteID, t.DogProfileID, t.IsCritical, t.IsFixed, t.CreatedTimeUTC, t.CreatedByUserID });
            
            CreateTable(
                "dbo.DogEventsReport",
                c => new
                    {
                        EventID = c.Int(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 200),
                        IsComplete = c.Boolean(nullable: false),
                        EventDate = c.DateTime(nullable: false),
                        DogProfileID = c.Int(nullable: false),
                        IsFixed = c.Boolean(nullable: false),
                        CreatedTimeUTC = c.DateTime(nullable: false),
                        CreatedByUserID = c.Guid(nullable: false),
                        Body = c.String(),
                        Name = c.String(maxLength: 200),
                        Breed = c.String(maxLength: 200),
                        Age = c.Int(),
                        Color = c.String(maxLength: 200),
                        PickedUpDate = c.DateTime(),
                        DonorID = c.Int(),
                        GraduationDate = c.DateTime(),
                        SponsorID = c.Int(),
                        WarriorID = c.Int(),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.EventID, t.Subject, t.IsComplete, t.EventDate, t.DogProfileID, t.IsFixed, t.CreatedTimeUTC, t.CreatedByUserID });
            
            CreateTable(
                "dbo.WarriorInfo",
                c => new
                    {
                        WarriorID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        GraduationDate = c.DateTime(),
                        Phone = c.String(maxLength: 30),
                        Email = c.String(maxLength: 200),
                        Address = c.String(maxLength: 200),
                        City = c.String(maxLength: 100),
                        State = c.String(maxLength: 2),
                        Zip = c.String(maxLength: 10),
                        CreatedTimeUTC = c.DateTime(nullable: false),
                        CreatedByUserID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.WarriorID);
            
            AddColumn("dbo.DogProfiles", "WarriorID", c => c.Int());
            DropIndex("dbo.DogMedications", new[] { "DogProfileID" });
            DropIndex("dbo.DogImages", new[] { "DogProfileID" });
            DropIndex("dbo.Users", new[] { "UserTypeID" });
            DropForeignKey("dbo.DogMedications", "DogProfileID", "dbo.DogProfiles");
            DropForeignKey("dbo.DogImages", "DogProfileID", "dbo.DogProfiles");
            DropForeignKey("dbo.Users", "UserTypeID", "dbo.UserTypes");
            AlterColumn("dbo.DogSkillNames", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Users", "UserTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.DogProfiles", "Color", c => c.String(maxLength: 200));
            AlterColumn("dbo.DogProfiles", "Breed", c => c.String(maxLength: 200));
            AlterColumn("dbo.DogProfiles", "Name", c => c.String(maxLength: 200));
            DropTable("dbo.DogMedications");
            RenameColumn(table: "dbo.DogImages", name: "DogProfileID", newName: "DogProfile_ProfileID");
            CreateIndex("dbo.DogImages", "DogProfile_ProfileID");
            CreateIndex("dbo.Users", "UserTypeId");
            CreateIndex("dbo.DogProfiles", "WarriorID");
            AddForeignKey("dbo.DogImages", "DogProfile_ProfileID", "dbo.DogProfiles", "ProfileID");
            AddForeignKey("dbo.Users", "UserTypeId", "dbo.UserTypes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.DogProfiles", "WarriorID", "dbo.WarriorInfo", "WarriorID");
        }
    }
}
