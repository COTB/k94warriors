namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DogProfileDeletedFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DogProfiles", "Deleted", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DogProfiles", "Deleted");
        }
    }
}
