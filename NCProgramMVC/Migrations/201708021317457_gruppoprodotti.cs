namespace NCProgramMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gruppoprodotti : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GruppoProdottis",
                c => new
                    {
                        GruppoPodotti_Id = c.Int(nullable: false, identity: true),
                        Prodotto = c.String(),
                        Descrizione = c.String(),
                    })
                .PrimaryKey(t => t.GruppoPodotti_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GruppoProdottis");
        }
    }
}
