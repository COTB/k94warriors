namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DogGenderDataType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DogProfiles", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DogProfiles", "Gender", c => c.String());
        }
    }
}