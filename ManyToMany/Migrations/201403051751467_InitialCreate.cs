namespace Whapp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Bat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BatName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            this.CreateTable(
                "dbo.Player",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PlayerFName = c.String(nullable: false, maxLength: 30),
                        PlayerLName = c.String(nullable: false, maxLength: 50),
                        DOB = c.DateTime(nullable: false),
                        TeamID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Team", t => t.TeamID)
                .Index(t => t.TeamID);
            
            this.CreateTable(
                "dbo.Team",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TeamName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            this.CreateTable(
                "dbo.PlayerBat",
                c => new
                    {
                        Player_ID = c.Int(nullable: false),
                        Bat_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Player_ID, t.Bat_ID })
                .ForeignKey("dbo.Player", t => t.Player_ID, cascadeDelete: true)
                .ForeignKey("dbo.Bat", t => t.Bat_ID, cascadeDelete: true)
                .Index(t => t.Player_ID)
                .Index(t => t.Bat_ID);
            
        }
        
        public override void Down()
        {
            this.DropForeignKey("dbo.Player", "TeamID", "dbo.Team");
            this.DropForeignKey("dbo.PlayerBat", "Bat_ID", "dbo.Bat");
            this.DropForeignKey("dbo.PlayerBat", "Player_ID", "dbo.Player");
            this.DropIndex("dbo.Player", new[] { "TeamID" });
            this.DropIndex("dbo.PlayerBat", new[] { "Bat_ID" });
            this.DropIndex("dbo.PlayerBat", new[] { "Player_ID" });
            this.DropTable("dbo.PlayerBat");
            this.DropTable("dbo.Team");
            this.DropTable("dbo.Player");
            this.DropTable("dbo.Bat");
        }
    }
}
