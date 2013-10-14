namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocationDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DogProfiles", "LocationDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DogProfiles", "LocationDescription");
        }
    }
}
