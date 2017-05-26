namespace PaymentApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sessions2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "Ended", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sessions", "Ended");
        }
    }
}
