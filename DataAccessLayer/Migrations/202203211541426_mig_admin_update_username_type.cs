namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_admin_update_username_type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "AdminUserName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "AdminUserName", c => c.Binary());
        }
    }
}
