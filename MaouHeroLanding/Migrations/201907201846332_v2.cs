namespace MaouHeroLanding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Compras", new[] { "EncomendaFK" });
            AlterColumn("dbo.Compras", "EncomendaFK", c => c.Int());
            CreateIndex("dbo.Compras", "EncomendaFK");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Compras", new[] { "EncomendaFK" });
            AlterColumn("dbo.Compras", "EncomendaFK", c => c.Int(nullable: false));
            CreateIndex("dbo.Compras", "EncomendaFK");
        }
    }
}
