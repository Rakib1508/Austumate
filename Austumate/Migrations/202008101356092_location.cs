namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class location : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationID = c.String(nullable: false, maxLength: 128),
                        LocationName = c.String(nullable: false, maxLength: 100),
                        CampusName = c.String(nullable: false, maxLength: 128),
                        LocationDetails = c.String(),
                    })
                .PrimaryKey(t => t.LocationID)
                .ForeignKey("dbo.Campuses", t => t.CampusName, cascadeDelete: true)
                .Index(t => t.CampusName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locations", "CampusName", "dbo.Campuses");
            DropIndex("dbo.Locations", new[] { "CampusName" });
            DropTable("dbo.Locations");
        }
    }
}
