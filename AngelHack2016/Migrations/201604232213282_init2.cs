namespace AngelHack2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Feedbacks", "AnalyzedFeedbackId");
            DropColumn("dbo.Feedbacks", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Feedbacks", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Feedbacks", "AnalyzedFeedbackId", c => c.Int());
        }
    }
}
