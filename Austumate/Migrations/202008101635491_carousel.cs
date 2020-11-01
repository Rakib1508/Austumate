namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class carousel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileCarousels",
                c => new
                    {
                        ProfileID = c.String(nullable: false, maxLength: 128),
                        CarouselImage = c.String(),
                    })
                .PrimaryKey(t => t.ProfileID)
                .ForeignKey("dbo.UserProfiles", t => t.ProfileID)
                .Index(t => t.ProfileID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileCarousels", "ProfileID", "dbo.UserProfiles");
            DropIndex("dbo.ProfileCarousels", new[] { "ProfileID" });
            DropTable("dbo.ProfileCarousels");
        }
    }
}
