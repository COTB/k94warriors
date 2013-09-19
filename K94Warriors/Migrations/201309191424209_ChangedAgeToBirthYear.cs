namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAgeToBirthYear : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.DogProfiles", "Age", "BirthYear");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.DogProfiles", "BirthYear", "Age");
        }
    }
}
