namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAgeToBirthYear : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DogProfiles", "BirthYear", c => c.Int());
            DropColumn("dbo.DogProfiles", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DogProfiles", "Age", c => c.Int());
            DropColumn("dbo.DogProfiles", "BirthYear");
        }
    }
}
