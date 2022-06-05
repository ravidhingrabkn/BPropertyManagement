namespace BPropertyManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Realtor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Realtors",
                c => new
                    {
                        RealtorId = c.Int(nullable: false, identity: true),
                        RealtorName = c.String(),
                        Phone = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.RealtorId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Realtors");
        }
    }
}
