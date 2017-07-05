namespace NCProgramMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ruoli : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Nome", c => c.String());
            AddColumn("dbo.AspNetUsers", "Cognome", c => c.String());
            AddColumn("dbo.AspNetUsers", "Indirizzo", c => c.String());
            AddColumn("dbo.AspNetUsers", "Città", c => c.String());
            AddColumn("dbo.AspNetUsers", "CAP", c => c.String());
            AddColumn("dbo.AspNetUsers", "Telefono", c => c.String());
            AddColumn("dbo.AspNetUsers", "Professione", c => c.String());
            AddColumn("dbo.AspNetUsers", "Organizzazione", c => c.String());
            AddColumn("dbo.AspNetUsers", "Privacy", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "Bloccato", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Bloccato");
            DropColumn("dbo.AspNetUsers", "Privacy");
            DropColumn("dbo.AspNetUsers", "Organizzazione");
            DropColumn("dbo.AspNetUsers", "Professione");
            DropColumn("dbo.AspNetUsers", "Telefono");
            DropColumn("dbo.AspNetUsers", "CAP");
            DropColumn("dbo.AspNetUsers", "Città");
            DropColumn("dbo.AspNetUsers", "Indirizzo");
            DropColumn("dbo.AspNetUsers", "Cognome");
            DropColumn("dbo.AspNetUsers", "Nome");
        }
    }
}
