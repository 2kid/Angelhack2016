namespace AngelHack2016.Migrations
{
    using AngelHack2016.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AngelHack2016.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AngelHack2016.Models.ApplicationDbContext context)
        {
            // Roles
            if (!context.Roles.Any(r => r.Name == "Customer"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Customer" };

                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "Business"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Business" };

                manager.Create(role);
            }
         
            // Accounts
            if (!context.Users.Any(u => u.UserName == "moses@spectres.solutions"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "moses@spectres.solutions",
                    Email = "moses@spectres.solutions",
                    EmailConfirmed = true
                };

                manager.Create(user, "Password!1");
                manager.AddToRole(user.Id, "Business");
            }

            if (!context.Users.Any(u => u.UserName == "ahmed@spectres.solutions"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "ahmed@spectres.solutions",
                    Email = "ahmed@spectres.solutions",
                    EmailConfirmed = true
                };

                manager.Create(user, "Password!1");
                manager.AddToRole(user.Id, "Customer");
            }
        }
    }
}
