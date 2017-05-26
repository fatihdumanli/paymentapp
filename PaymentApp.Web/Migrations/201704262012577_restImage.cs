namespace PaymentApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "ImageUri", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "ImageUri");
        }
    }
}
