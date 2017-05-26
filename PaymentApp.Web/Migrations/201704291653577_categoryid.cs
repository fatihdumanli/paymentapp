namespace PaymentApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CatId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CatId");
        }
    }
}
