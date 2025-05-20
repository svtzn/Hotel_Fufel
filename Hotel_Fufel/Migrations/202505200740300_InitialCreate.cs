namespace Hotel_Fufel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomsAmount = c.Int(nullable: false),
                        PricePerNight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImagePath = c.String(),
                        Smoke = c.Boolean(nullable: false),
                        Balcony = c.Boolean(nullable: false),
                        WiFi = c.Boolean(nullable: false),
                        ComfortLevel = c.String(),
                        Kitchen = c.Boolean(nullable: false),
                        Shower = c.Boolean(nullable: false),
                        IsFree = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        ProfilePic = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Rooms");
        }
    }
}
