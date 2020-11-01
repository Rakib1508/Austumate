namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campuses",
                c => new
                    {
                        CampusID = c.String(nullable: false, maxLength: 128),
                        CampusName = c.String(nullable: false, maxLength: 100),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CampusID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Campuses");
        }
    }
}
