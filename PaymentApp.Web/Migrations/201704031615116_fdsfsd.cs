namespace PaymentApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdsfsd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sessions", "Started", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sessions", "Started", c => c.DateTime(nullable: false));
        }
    }
}
