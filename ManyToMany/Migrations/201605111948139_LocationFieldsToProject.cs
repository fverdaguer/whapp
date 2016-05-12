namespace Whapp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class LocationFieldsToProject : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.Project", "LocationName", c => c.String());
            this.AddColumn("dbo.Project", "LocationGooglePlaceId", c => c.String());
            this.AddColumn("dbo.Project", "LocationLatitude", c => c.Double(nullable: false));
            this.AddColumn("dbo.Project", "LocationLongitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            this.DropColumn("dbo.Project", "LocationLongitude");
            this.DropColumn("dbo.Project", "LocationLatitude");
            this.DropColumn("dbo.Project", "LocationGooglePlaceId");
            this.DropColumn("dbo.Project", "LocationName");
        }
    }
}
