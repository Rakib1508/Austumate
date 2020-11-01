namespace Austumate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class semester : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        SemesterID = c.String(nullable: false, maxLength: 20),
                        Session = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.SemesterID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Semesters");
        }
    }
}
