namespace NCProgramMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gruppoprodotti1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GruppoProdottis", "DescrizioneDett", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GruppoProdottis", "DescrizioneDett");
        }
    }
}
