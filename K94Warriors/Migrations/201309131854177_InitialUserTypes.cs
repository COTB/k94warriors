namespace K94Warriors.Migrations
{
    using K94Warriors.Enums;
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUserTypes : DbMigration
    {
        public override void Up()
        {
            // PI: I realized after creating this that this should be done in the Seed method of Configuration. But leave it in place anyways.
            this.Sql("INSERT INTO dbo.UserTypes (ID, Name) VALUES (" + (int)UserTypeEnum.Administrator + ", 'Administrator')");
            this.Sql("INSERT INTO dbo.UserTypes (ID, Name) VALUES (" + (int)UserTypeEnum.Trainer + ", 'Trainer')");
            this.Sql("INSERT INTO dbo.UserTypes (ID, Name) VALUES (" + (int)UserTypeEnum.Volunteer + ", 'Volunteer')");
        }
        
        public override void Down()
        {
            this.Sql("TRUNCATE TABLE dbo.UserTypes;");
        }
    }
}
