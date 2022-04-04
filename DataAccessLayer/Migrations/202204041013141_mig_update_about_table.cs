namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_update_about_table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Abouts", "AboutTitle", c => c.String(maxLength: 500));
            AddColumn("dbo.Abouts", "AboutDetails", c => c.String(maxLength: 1000));
            DropColumn("dbo.Abouts", "AboutDetails1");
            DropColumn("dbo.Abouts", "AboutDetails2");
            DropColumn("dbo.Abouts", "AboutImage1");
            DropColumn("dbo.Abouts", "AboutImage2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Abouts", "AboutImage2", c => c.String(maxLength: 100));
            AddColumn("dbo.Abouts", "AboutImage1", c => c.String(maxLength: 100));
            AddColumn("dbo.Abouts", "AboutDetails2", c => c.String(maxLength: 1000));
            AddColumn("dbo.Abouts", "AboutDetails1", c => c.String(maxLength: 500));
            DropColumn("dbo.Abouts", "AboutDetails");
            DropColumn("dbo.Abouts", "AboutTitle");
        }
    }
}
