namespace NCProgramMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posizioneprodotti : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CimcoDetts", "Posizione", c => c.Int(nullable: false));
            AddColumn("dbo.Cimcoes", "Posizione", c => c.Int(nullable: false));
            AddColumn("dbo.MazacamDetts", "Posizione", c => c.Int(nullable: false));
            AddColumn("dbo.Mazacams", "Posizione", c => c.Int(nullable: false));
            AddColumn("dbo.TdmDetts", "Posizione", c => c.Int(nullable: false));
            AddColumn("dbo.Tdms", "Posizione", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tdms", "Posizione");
            DropColumn("dbo.TdmDetts", "Posizione");
            DropColumn("dbo.Mazacams", "Posizione");
            DropColumn("dbo.MazacamDetts", "Posizione");
            DropColumn("dbo.Cimcoes", "Posizione");
            DropColumn("dbo.CimcoDetts", "Posizione");
        }
    }
}
