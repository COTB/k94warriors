namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDogNoteAttachmentForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DogNoteAttachments", "DogNote_NoteID", "dbo.DogNotes");
            DropIndex("dbo.DogNoteAttachments", new[] { "DogNote_NoteID" });
            DropColumn("dbo.DogNoteAttachments", "DogNoteID");
            RenameColumn(table: "dbo.DogNoteAttachments", name: "DogNote_NoteID", newName: "DogNoteID");
            AddForeignKey("dbo.DogNoteAttachments", "DogNoteID", "dbo.DogNotes", "NoteID", cascadeDelete: true);
            CreateIndex("dbo.DogNoteAttachments", "DogNoteID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DogNoteAttachments", new[] { "DogNoteID" });
            DropForeignKey("dbo.DogNoteAttachments", "DogNoteID", "dbo.DogNotes");
            RenameColumn(table: "dbo.DogNoteAttachments", name: "DogNoteID", newName: "DogNote_NoteID");
            AddColumn("dbo.DogNoteAttachments", "DogNoteID", b => b.Int());
            CreateIndex("dbo.DogNoteAttachments", "DogNote_NoteID");
            AddForeignKey("dbo.DogNoteAttachments", "DogNote_NoteID", "dbo.DogNotes", "NoteID");
        }
    }
}
