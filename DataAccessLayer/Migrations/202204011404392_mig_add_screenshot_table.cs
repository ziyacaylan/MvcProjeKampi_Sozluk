namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_add_screenshot_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScreenShots",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageName = c.String(maxLength: 100),
                        ImagePath = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ScreenShots");
        }
    }
}
