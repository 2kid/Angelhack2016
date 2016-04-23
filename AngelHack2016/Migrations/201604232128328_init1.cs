namespace AngelHack2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Feedbacks", "SentimentScore", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedbacks", "SentimentScore", c => c.Single());
        }
    }
}
