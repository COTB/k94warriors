namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Feeding : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DogFeedingEntries",
                c => new
                    {
                        DogFeedingEntryID = c.Int(nullable: false, identity: true),
                        DogProfileID = c.Int(nullable: false),
                        AmountDescription = c.String(nullable: false, maxLength: 100),
                        FoodName = c.String(nullable: false, maxLength: 100),
                        AMFeeding = c.Boolean(nullable: false),
                        NoonFeeding = c.Boolean(nullable: false),
                        PMFeeding = c.Boolean(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.DogFeedingEntryID)
                .ForeignKey("dbo.DogProfiles", t => t.DogProfileID, cascadeDelete: true)
                .Index(t => t.DogProfileID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DogFeedingEntries", new[] { "DogProfileID" });
            DropForeignKey("dbo.DogFeedingEntries", "DogProfileID", "dbo.DogProfiles");
            DropTable("dbo.DogFeedingEntries");
        }
    }
}
