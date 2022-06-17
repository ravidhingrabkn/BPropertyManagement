namespace BPropertyManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPhone : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Realtors", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Realtors", "Phone", c => c.Int(nullable: false));
        }
    }
}
