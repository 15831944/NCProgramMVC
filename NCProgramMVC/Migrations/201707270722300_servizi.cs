namespace NCProgramMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class servizi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiziDetts",
                c => new
                    {
                        ServizoDett_Id = c.Int(nullable: false, identity: true),
                        Servizio_Id = c.Int(nullable: false),
                        ServizioDett = c.String(),
                        Descrizione = c.String(),
                    })
                .PrimaryKey(t => t.ServizoDett_Id)
                .ForeignKey("dbo.Servizis", t => t.Servizio_Id, cascadeDelete: true)
                .Index(t => t.Servizio_Id);
            
            CreateTable(
                "dbo.Servizis",
                c => new
                    {
                        Servizio_Id = c.Int(nullable: false, identity: true),
                        Servizio = c.String(),
                        Descrizione = c.String(),
                    })
                .PrimaryKey(t => t.Servizio_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiziDetts", "Servizio_Id", "dbo.Servizis");
            DropIndex("dbo.ServiziDetts", new[] { "Servizio_Id" });
            DropTable("dbo.Servizis");
            DropTable("dbo.ServiziDetts");
        }
    }
}
