namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DogImages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DogImages",
                c => new
                    {
                        DogImageID = c.Int(nullable: false, identity: true),
                        DogProfileID = c.Int(nullable: false),
                        BlobKey = c.Guid(nullable: false),
                        DogProfile_ProfileID = c.Int(),
                    })
                .PrimaryKey(t => t.DogImageID)
                .ForeignKey("dbo.DogProfiles", t => t.DogProfile_ProfileID)
                .Index(t => t.DogProfile_ProfileID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DogImages", new[] { "DogProfile_ProfileID" });
            DropForeignKey("dbo.DogImages", "DogProfile_ProfileID", "dbo.DogProfiles");
            DropTable("dbo.DogImages");
        }
    }
}
