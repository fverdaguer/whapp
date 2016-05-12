namespace ManyToMany.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocationFieldsToProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "LocationName", c => c.String());
            AddColumn("dbo.Project", "LocationGooglePlaceId", c => c.String());
            AddColumn("dbo.Project", "LocationLatitude", c => c.Double(nullable: false));
            AddColumn("dbo.Project", "LocationLongitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "LocationLongitude");
            DropColumn("dbo.Project", "LocationLatitude");
            DropColumn("dbo.Project", "LocationGooglePlaceId");
            DropColumn("dbo.Project", "LocationName");
        }
    }
}
