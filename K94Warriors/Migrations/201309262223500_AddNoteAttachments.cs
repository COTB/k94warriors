namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNoteAttachments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DogNoteAttachments",
                c => new
                    {
                        DogNoteAttachmentID = c.Int(nullable: false, identity: true),
                        DogNoteID = c.Int(nullable: false),
                        BlobKey = c.Guid(nullable: false),
                        MimeType = c.String(),
                        DogNote_NoteID = c.Int(),
                    })
                .PrimaryKey(t => t.DogNoteAttachmentID)
                .ForeignKey("dbo.DogNotes", t => t.DogNote_NoteID)
                .Index(t => t.DogNote_NoteID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.DogNoteAttachments", new[] { "DogNote_NoteID" });
            DropForeignKey("dbo.DogNoteAttachments", "DogNote_NoteID", "dbo.DogNotes");
            DropTable("dbo.DogNoteAttachments");
        }
    }
}
