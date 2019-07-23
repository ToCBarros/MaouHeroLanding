using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using MaouHeroLanding.Models;

[assembly: OwinStartupAttribute(typeof(MaouHeroLanding.Startup))]
namespace MaouHeroLanding
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            iniciaAplicacao();


        }

        private void iniciaAplicacao()
        {

            // identifica a base de dados de serviço à aplicação
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criar a Role
            if (!roleManager.RoleExists("cliente"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "cliente";
                roleManager.Create(role);
            }

            // criar a Role
            if (!roleManager.RoleExists("funcionario"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "funcionario";
                roleManager.Create(role);
            }

            // criar a Role
            if (!roleManager.RoleExists("gestor"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "gestor";
                roleManager.Create(role);
            }
            
            var user2 = new ApplicationUser();

            user2.UserName = "funcionario@funcionario.com";
            user2.Email = "funcionario@funcionario.com";
            string userPWD2 = "123Qwe.";
            var chkUser2 = userManager.Create(user2, userPWD2);
            
            if (chkUser2.Succeeded)
            {
                var result1 = userManager.AddToRole(user2.Id, "funcionario");
            }
            
            var user3 = new ApplicationUser();

            user3.UserName = "gestor@gestor.com";
            user3.Email = "gestor@gestor.com";
            string userPWD3 = "123Qwe.";
            var chkUser3 = userManager.Create(user3, userPWD3);
            
            if (chkUser3.Succeeded)
            {
                var result1 = userManager.AddToRole(user3.Id, "gestor");
            }

        }
    }
}
