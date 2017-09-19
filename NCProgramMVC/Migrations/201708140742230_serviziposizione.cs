namespace NCProgramMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serviziposizione : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiziDetts", "Posizione", c => c.Int(nullable: false));
            AddColumn("dbo.Servizis", "Posizione", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Servizis", "Posizione");
            DropColumn("dbo.ServiziDetts", "Posizione");
        }
    }
}
