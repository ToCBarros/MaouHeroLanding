namespace MaouHeroLanding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            CreateTable(
                "dbo.Artigos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Tipo = c.String(),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Data_Entrada = c.DateTime(nullable: false),
                        imagem = c.String(),
                        Descricao = c.String(),
                        Produtor = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EncomendaFK = c.Int(nullable: false),
                        ArtigoFK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Artigos", t => t.ArtigoFK)
                .ForeignKey("dbo.Encomendas", t => t.EncomendaFK)
                .Index(t => t.EncomendaFK)
                .Index(t => t.ArtigoFK);
            
            CreateTable(
                "dbo.Encomendas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Local_entrega = c.String(),
                        Preco = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Estado = c.Boolean(nullable: false),
                        ClienteFK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clientes", t => t.ClienteFK)
                .Index(t => t.ClienteFK);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        NIF = c.String(),
                        Data_Nasc = c.DateTime(nullable: false),
                        Telemovel = c.String(),
                        Username = c.String(),
                        Codigo_postal = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Compras", "EncomendaFK", "dbo.Encomendas");
            DropForeignKey("dbo.Encomendas", "ClienteFK", "dbo.Clientes");
            DropForeignKey("dbo.Compras", "ArtigoFK", "dbo.Artigos");
            DropIndex("dbo.Encomendas", new[] { "ClienteFK" });
            DropIndex("dbo.Compras", new[] { "ArtigoFK" });
            DropIndex("dbo.Compras", new[] { "EncomendaFK" });
            DropTable("dbo.Clientes");
            DropTable("dbo.Encomendas");
            DropTable("dbo.Compras");
            DropTable("dbo.Artigos");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
        }
    }
}
