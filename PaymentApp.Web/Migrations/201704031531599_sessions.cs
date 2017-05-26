namespace PaymentApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sessions : DbMigration
    {
        public override void Up()
        {
           
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        TableId = c.Int(nullable: false),
                        Slot = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Started = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        SessionId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
          
            
        }
        
        public override void Down()
        {
           
            DropTable("dbo.Transactions");
            DropTable("dbo.Sessions");
           
        }
    }
}
