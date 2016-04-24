namespace AngelHack2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Businesses", "cuisine", c => c.String());
            DropColumn("dbo.Businesses", "quicine");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Businesses", "quicine", c => c.String());
            DropColumn("dbo.Businesses", "cuisine");
        }
    }
}
