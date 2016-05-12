namespace Whapp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemoveUnusedEntities : DbMigration
    {
        public override void Up()
        {
            this.DropForeignKey("dbo.PlayerBat", "Player_ID", "dbo.Player");
            this.DropForeignKey("dbo.PlayerBat", "Bat_ID", "dbo.Bat");
            this.DropForeignKey("dbo.Player", "TeamID", "dbo.Team");
            this.DropIndex("dbo.Player", new[] { "TeamID" });
            this.DropIndex("dbo.PlayerBat", new[] { "Player_ID" });
            this.DropIndex("dbo.PlayerBat", new[] { "Bat_ID" });
            this.DropTable("dbo.Bat");
            this.DropTable("dbo.Player");
            this.DropTable("dbo.Team");
            this.DropTable("dbo.PlayerBat");
        }
        
        public override void Down()
        {
            this.CreateTable(
                "dbo.PlayerBat",
                c => new
                    {
                        Player_ID = c.Int(nullable: false),
                        Bat_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Player_ID, t.Bat_ID });
            
            this.CreateTable(
                "dbo.Team",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TeamName = c.String(nullable: false, maxLength: 50),
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
                .PrimaryKey(t => t.ID);
            
            this.CreateTable(
                "dbo.Bat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BatName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            this.CreateIndex("dbo.PlayerBat", "Bat_ID");
            this.CreateIndex("dbo.PlayerBat", "Player_ID");
            this.CreateIndex("dbo.Player", "TeamID");
            this.AddForeignKey("dbo.Player", "TeamID", "dbo.Team", "ID");
            this.AddForeignKey("dbo.PlayerBat", "Bat_ID", "dbo.Bat", "ID", cascadeDelete: true);
            this.AddForeignKey("dbo.PlayerBat", "Player_ID", "dbo.Player", "ID", cascadeDelete: true);
        }
    }
}
