namespace BPropertyManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class propertyrealtor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Properties", "RealtorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Properties", "RealtorId");
            AddForeignKey("dbo.Properties", "RealtorId", "dbo.Realtors", "RealtorId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Properties", "RealtorId", "dbo.Realtors");
            DropIndex("dbo.Properties", new[] { "RealtorId" });
            DropColumn("dbo.Properties", "RealtorId");
        }
    }
}
