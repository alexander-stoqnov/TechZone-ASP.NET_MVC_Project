namespace TechZone.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;

    internal sealed class Configuration : DbMigrationsConfiguration<TechZoneContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TechZoneContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // SeedRoles(roleManager);
            // SeedUsers(context, userManager);

        }

        private void SeedUsers(TechZoneContext context, UserManager<ApplicationUser> userManager)
        {
            var admin = userManager.FindByName("admin");
            if (admin == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "admin",
                    FirstName = "Admin",
                    LastName = "Adminer",
                    Email = "admin@g.c",
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Admin");
                context.Customers.Add(new Customer() { UserId = newUser.Id });
            }

            var pesho = userManager.FindByName("pesho");
            if (pesho == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "pesho",
                    FirstName = "Petar",
                    LastName = "Dimitrov",
                    Email = "pesho@g.c",
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Admin");
                userManager.AddToRole(newUser.Id, "Customer");
                context.Customers.Add(new Customer() { UserId = newUser.Id });
            }

            var bojo = userManager.FindByName("bojo");
            if (bojo == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "bojo",
                    FirstName = "Bozhidar",
                    LastName = "Gevechanov",
                    Email = "bojo@g.c",
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Publisher");
                context.Publishers.Add(new Publisher() { UserId = newUser.Id });
            }

            var joro = userManager.FindByName("joro");
            if (joro == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "joro",
                    FirstName = "Georgi",
                    LastName = "Stoimenov",
                    Email = "joro@g.c",
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Publisher");
                context.Publishers.Add(new Publisher() { UserId = newUser.Id });
            }

            var jicata = userManager.FindByName("jicata");
            if (jicata == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = "jicata",
                    FirstName = "Svetlin",
                    LastName = "Galov",
                    Email = "jicata@g.c",
                };
                userManager.Create(newUser, "Pesho1!");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, "Customer");
                context.Customers.Add(new Customer() { UserId = newUser.Id });
            }

            context.SaveChanges();
        }

        private void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole("Admin"));
            if (!roleManager.RoleExists("Publisher"))
                roleManager.Create(new IdentityRole("Publisher"));
            if (!roleManager.RoleExists("Moderator"))
                roleManager.Create(new IdentityRole("Moderator"));
            if (!roleManager.RoleExists("Customer"))
                roleManager.Create(new IdentityRole("Customer"));
        }
    }
}