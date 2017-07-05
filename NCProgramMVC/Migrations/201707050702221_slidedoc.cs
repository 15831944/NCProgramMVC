namespace NCProgramMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class slidedoc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documentis",
                c => new
                    {
                        Documenti_Id = c.Int(nullable: false, identity: true),
                        Titolo = c.String(),
                        Descrizione = c.String(),
                        Nomefile = c.String(),
                        Riservato = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Documenti_Id);
            
            CreateTable(
                "dbo.Slides",
                c => new
                    {
                        Slide_Id = c.Int(nullable: false, identity: true),
                        Titolo = c.String(),
                        Sottotitolo = c.String(),
                        Descrizione = c.String(),
                        Sfondo = c.String(),
                        Posizione = c.Int(nullable: false),
                        Pubblica = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Slide_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Slides");
            DropTable("dbo.Documentis");
        }
    }
}
