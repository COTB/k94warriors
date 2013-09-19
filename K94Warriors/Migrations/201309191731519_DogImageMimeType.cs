namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DogImageMimeType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DogImages", "MimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DogImages", "MimeType");
        }
    }
}
