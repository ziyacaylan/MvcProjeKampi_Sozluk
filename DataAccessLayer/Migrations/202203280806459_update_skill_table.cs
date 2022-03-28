namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_skill_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Talents", "SkillArea", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Talents", "SkillArea");
        }
    }
}
