namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DogProfileHealthCondition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DogProfiles", "HealthCondition", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DogProfiles", "HealthCondition");
        }
    }
}
