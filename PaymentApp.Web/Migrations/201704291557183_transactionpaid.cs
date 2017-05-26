namespace PaymentApp.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transactionpaid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "IsPaid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "IsPaid");
        }
    }
}
