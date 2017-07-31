namespace NCProgramMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prodotti : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CimcoDetts",
                c => new
                    {
                        CimcoDett_Id = c.Int(nullable: false, identity: true),
                        Cimco_Id = c.Int(nullable: false),
                        ProdottoDett = c.String(),
                        Descrizione = c.String(),
                    })
                .PrimaryKey(t => t.CimcoDett_Id)
                .ForeignKey("dbo.Cimcoes", t => t.Cimco_Id, cascadeDelete: true)
                .Index(t => t.Cimco_Id);
            
            CreateTable(
                "dbo.Cimcoes",
                c => new
                    {
                        Cimco_Id = c.Int(nullable: false, identity: true),
                        Prodotto = c.String(),
                        Descrizione = c.String(),
                    })
                .PrimaryKey(t => t.Cimco_Id);
            
            CreateTable(
                "dbo.MazacamDetts",
                c => new
                    {
                        MazacamDett_Id = c.Int(nullable: false, identity: true),
                        Mazacam_Id = c.Int(nullable: false),
                        ProdottoDett = c.String(),
                        Descrizione = c.String(),
                    })
                .PrimaryKey(t => t.MazacamDett_Id)
                .ForeignKey("dbo.Mazacams", t => t.Mazacam_Id, cascadeDelete: true)
                .Index(t => t.Mazacam_Id);
            
            CreateTable(
                "dbo.Mazacams",
                c => new
                    {
                        Mazacam_Id = c.Int(nullable: false, identity: true),
                        Prodotto = c.String(),
                        Descrizione = c.String(),
                    })
                .PrimaryKey(t => t.Mazacam_Id);
            
            CreateTable(
                "dbo.TdmDetts",
                c => new
                    {
                        TdmDett_Id = c.Int(nullable: false, identity: true),
                        Tdm_Id = c.Int(nullable: false),
                        ProdottoDett = c.String(),
                        Descrizione = c.String(),
                    })
                .PrimaryKey(t => t.TdmDett_Id)
                .ForeignKey("dbo.Tdms", t => t.Tdm_Id, cascadeDelete: true)
                .Index(t => t.Tdm_Id);
            
            CreateTable(
                "dbo.Tdms",
                c => new
                    {
                        Tdm_Id = c.Int(nullable: false, identity: true),
                        Prodotto = c.String(),
                        Descrizione = c.String(),
                    })
                .PrimaryKey(t => t.Tdm_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TdmDetts", "Tdm_Id", "dbo.Tdms");
            DropForeignKey("dbo.MazacamDetts", "Mazacam_Id", "dbo.Mazacams");
            DropForeignKey("dbo.CimcoDetts", "Cimco_Id", "dbo.Cimcoes");
            DropIndex("dbo.TdmDetts", new[] { "Tdm_Id" });
            DropIndex("dbo.MazacamDetts", new[] { "Mazacam_Id" });
            DropIndex("dbo.CimcoDetts", new[] { "Cimco_Id" });
            DropTable("dbo.Tdms");
            DropTable("dbo.TdmDetts");
            DropTable("dbo.Mazacams");
            DropTable("dbo.MazacamDetts");
            DropTable("dbo.Cimcoes");
            DropTable("dbo.CimcoDetts");
        }
    }
}
