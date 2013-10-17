namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteAttachmentFileName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DogNoteAttachments", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DogNoteAttachments", "FileName");
        }
    }
}
