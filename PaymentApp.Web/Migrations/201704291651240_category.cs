namespace PaymentApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class category : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Category", c => c.String());
            DropColumn("dbo.Products", "CatId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CatId", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "Category");
        }
    }
}
