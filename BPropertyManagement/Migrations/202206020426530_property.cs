namespace BPropertyManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class property : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        PropertyId = c.Int(nullable: false, identity: true),
                        PropertyName = c.String(),
                        Type = c.String(),
                        Style = c.String(),
                        Size = c.Int(nullable: false),
                        ListPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PropertyId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Properties");
        }
    }
}
