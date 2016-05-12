namespace ManyToMany.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUnusedEntities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlayerBat", "Player_ID", "dbo.Player");
            DropForeignKey("dbo.PlayerBat", "Bat_ID", "dbo.Bat");
            DropForeignKey("dbo.Player", "TeamID", "dbo.Team");
            DropIndex("dbo.Player", new[] { "TeamID" });
            DropIndex("dbo.PlayerBat", new[] { "Player_ID" });
            DropIndex("dbo.PlayerBat", new[] { "Bat_ID" });
            DropTable("dbo.Bat");
            DropTable("dbo.Player");
            DropTable("dbo.Team");
            DropTable("dbo.PlayerBat");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PlayerBat",
                c => new
                    {
                        Player_ID = c.Int(nullable: false),
                        Bat_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Player_ID, t.Bat_ID });
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TeamName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
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
            
            CreateTable(
                "dbo.Bat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BatName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.PlayerBat", "Bat_ID");
            CreateIndex("dbo.PlayerBat", "Player_ID");
            CreateIndex("dbo.Player", "TeamID");
            AddForeignKey("dbo.Player", "TeamID", "dbo.Team", "ID");
            AddForeignKey("dbo.PlayerBat", "Bat_ID", "dbo.Bat", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PlayerBat", "Player_ID", "dbo.Player", "ID", cascadeDelete: true);
        }
    }
}
