namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteAttachmentFileExtension : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DogNoteAttachments", "FileExtension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DogNoteAttachments", "FileExtension");
        }
    }
}
