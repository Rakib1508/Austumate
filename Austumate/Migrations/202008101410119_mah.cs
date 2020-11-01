namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mah : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Majors",
                c => new
                    {
                        MajorID = c.String(nullable: false, maxLength: 50),
                        MajorName = c.String(nullable: false, maxLength: 100),
                        CollegeName = c.String(nullable: false, maxLength: 50),
                        Duration = c.Int(nullable: false),
                        Level = c.String(nullable: false, maxLength: 50),
                        Requirements = c.String(maxLength: 512),
                    })
                .PrimaryKey(t => t.MajorID)
                .ForeignKey("dbo.Colleges", t => t.CollegeName, cascadeDelete: true)
                .Index(t => t.CollegeName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Majors", "CollegeName", "dbo.Colleges");
            DropIndex("dbo.Majors", new[] { "CollegeName" });
            DropTable("dbo.Majors");
        }
    }
}
