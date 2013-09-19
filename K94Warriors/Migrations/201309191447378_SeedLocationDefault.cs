namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedLocationDefault : DbMigration
    {
        public override void Up()
        {
            this.Sql("INSERT INTO dbo.Locations (Name) VALUES ('Not On Premise')");
        }
        
        public override void Down()
        {
            this.Sql("DELETE FROM dbo.Locations WHERE Name = 'Not On Premise';");
        }
    }
}
