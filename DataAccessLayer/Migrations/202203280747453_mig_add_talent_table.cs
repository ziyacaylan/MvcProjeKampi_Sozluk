namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_add_talent_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Talents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SkillName = c.String(maxLength: 50),
                        SkillDetails = c.String(maxLength: 250),
                        SkillLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Talents");
        }
    }
}
