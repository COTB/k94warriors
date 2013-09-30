namespace K94Warriors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscriminatorColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DogProfiles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DogProfiles", "Discriminator");
        }
    }
}
