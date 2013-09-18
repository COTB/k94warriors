namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DogEvents",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false, maxLength: 200),
                        Body = c.String(),
                        IsComplete = c.Boolean(nullable: false),
                        EventDate = c.DateTime(nullable: false),
                        DogProfileID = c.Int(nullable: false),
                        EventTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.DogProfiles", t => t.DogProfileID, cascadeDelete: true)
                .ForeignKey("dbo.EventTypes", t => t.EventTypeId, cascadeDelete: true)
                .Index(t => t.DogProfileID)
                .Index(t => t.EventTypeId);
            
            CreateTable(
                "dbo.DogProfiles",
                c => new
                    {
                        ProfileID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Breed = c.String(maxLength: 200),
                        Age = c.Int(),
                        Color = c.String(maxLength: 200),
                        Gender = c.String(),
                        PickedUpDate = c.DateTime(),
                        IsFixed = c.Boolean(nullable: false),
                        GraduationDate = c.DateTime(),
                        WarriorID = c.Int(),
                        CreatedTimeUTC = c.DateTime(nullable: false),
                        CreatedByUserID = c.Int(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProfileID)
                .ForeignKey("dbo.WarriorInfo", t => t.WarriorID)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.WarriorID)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.DogMedicalRecords",
                c => new
                    {
                        RecordID = c.Int(nullable: false, identity: true),
                        DogProfileID = c.Int(nullable: false),
                        RecordType = c.String(maxLength: 200),
                        Title = c.String(maxLength: 200),
                        RecordExpirationDate = c.DateTime(),
                        RecordURL = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.RecordID)
                .ForeignKey("dbo.DogProfiles", t => t.DogProfileID, cascadeDelete: true)
                .Index(t => t.DogProfileID);
            
            CreateTable(
                "dbo.DogNotes",
                c => new
                    {
                        NoteID = c.Int(nullable: false, identity: true),
                        DogProfileID = c.Int(nullable: false),
                        Note = c.String(),
                        IsCritical = c.Boolean(nullable: false),
                        NoteTypeId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedByUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NoteID)
                .ForeignKey("dbo.DogProfiles", t => t.DogProfileID, cascadeDelete: true)
                .ForeignKey("dbo.NoteTypes", t => t.NoteTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId, cascadeDelete: true)
                .Index(t => t.DogProfileID)
                .Index(t => t.NoteTypeId)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "dbo.NoteTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 300),
                        Phone = c.String(maxLength: 30),
                        DisplayName = c.String(maxLength: 200),
                        CreatedTimeUTC = c.DateTime(nullable: false),
                        PhoneProvider = c.String(maxLength: 200),
                        UserTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
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
            
            CreateTable(
                "dbo.DogSkills",
                c => new
                    {
                        DogSkilID = c.Int(nullable: false, identity: true),
                        DogProfileID = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        SkillNameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DogSkilID)
                .ForeignKey("dbo.DogProfiles", t => t.DogProfileID, cascadeDelete: true)
                .ForeignKey("dbo.DogSkillNames", t => t.SkillNameId, cascadeDelete: true)
                .Index(t => t.DogProfileID)
                .Index(t => t.SkillNameId);
            
            CreateTable(
                "dbo.DogSkillNames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EventTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
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

            CreateTable("dbo.webpages_Membership", c => new
            {
                UserID = c.Int(nullable: false),
                CreateDate = c.DateTime(),
                ConfirmationToken = c.String(maxLength: 128),
                IsConfirmed = c.Boolean(defaultValue: false),
                LastPasswordFailureDate = c.DateTime(),
                PasswordFailuresSinceLastSuccess = c.Int(nullable: false, defaultValue: 0),
                Password = c.String(maxLength: 128, nullable: false),
                PasswordChangedDate = c.DateTime(),
                PasswordSalt = c.String(maxLength: 128, nullable: false),
                PasswordVerificationToken = c.String(maxLength: 128),
                PasswordVerificationTokenExpirationDate = c.DateTime()
            })
            .PrimaryKey(i => i.UserID);

            CreateTable("dbo.webpages_OAuthMembership", c => new
            {
                Provider = c.String(maxLength: 30, nullable: false),
                ProviderUserId = c.String(maxLength: 100, nullable: false),
                UserId = c.Int(nullable: false)
            })
            .PrimaryKey(i => new { i.Provider, i.ProviderUserId })
            .ForeignKey("dbo.Users", i => i.UserId);

            CreateTable("dbo.webpages_Roles", c => new
            {
                RoleId = c.Int(nullable: false, identity: true),
                RoleName = c.String(maxLength: 256, nullable: false)
            })
            .PrimaryKey(i => i.RoleId);

            CreateTable("dbo.webpages_UsersInRoles", c => new
            {
                UserId = c.Int(nullable: false),
                RoleId = c.Int(nullable: false)
            })
            .PrimaryKey(i => new { i.UserId, i.RoleId })
            .ForeignKey("dbo.Users", i => i.UserId)
            .ForeignKey("dbo.webpages_Roles", i => i.RoleId);
        }
        
        public override void Down()
        {
            DropIndex("dbo.DogSkills", new[] { "SkillNameId" });
            DropIndex("dbo.DogSkills", new[] { "DogProfileID" });
            DropIndex("dbo.Users", new[] { "UserTypeId" });
            DropIndex("dbo.DogNotes", new[] { "CreatedByUserId" });
            DropIndex("dbo.DogNotes", new[] { "NoteTypeId" });
            DropIndex("dbo.DogNotes", new[] { "DogProfileID" });
            DropIndex("dbo.DogMedicalRecords", new[] { "DogProfileID" });
            DropIndex("dbo.DogProfiles", new[] { "LocationId" });
            DropIndex("dbo.DogProfiles", new[] { "WarriorID" });
            DropIndex("dbo.DogEvents", new[] { "EventTypeId" });
            DropIndex("dbo.DogEvents", new[] { "DogProfileID" });
            DropForeignKey("dbo.DogSkills", "SkillNameId", "dbo.DogSkillNames");
            DropForeignKey("dbo.DogSkills", "DogProfileID", "dbo.DogProfiles");
            DropForeignKey("dbo.Users", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.DogNotes", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.DogNotes", "NoteTypeId", "dbo.NoteTypes");
            DropForeignKey("dbo.DogNotes", "DogProfileID", "dbo.DogProfiles");
            DropForeignKey("dbo.DogMedicalRecords", "DogProfileID", "dbo.DogProfiles");
            DropForeignKey("dbo.DogProfiles", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.DogProfiles", "WarriorID", "dbo.WarriorInfo");
            DropForeignKey("dbo.DogEvents", "EventTypeId", "dbo.EventTypes");
            DropForeignKey("dbo.DogEvents", "DogProfileID", "dbo.DogProfiles");
            DropTable("dbo.DogNotesReport");
            DropTable("dbo.DogEventsReport");
            DropTable("dbo.EventTypes");
            DropTable("dbo.Locations");
            DropTable("dbo.DogSkillNames");
            DropTable("dbo.DogSkills");
            DropTable("dbo.WarriorInfo");
            DropTable("dbo.UserTypes");
            DropTable("dbo.webpages_UsersInRoles");
            DropTable("dbo.webpages_Roles");
            DropTable("dbo.webpages_OAuthMembership");
            DropTable("dbo.webpages_Membership");
            DropTable("dbo.Users");
            DropTable("dbo.NoteTypes");
            DropTable("dbo.DogNotes");
            DropTable("dbo.DogMedicalRecords");
            DropTable("dbo.DogProfiles");
            DropTable("dbo.DogEvents");
        }
    }
}
