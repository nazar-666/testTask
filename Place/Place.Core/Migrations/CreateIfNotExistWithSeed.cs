using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Place.Core.Consts;
using Place.Core.Data;
using Place.Core.Data.Entites;
using Place.Core.Helpers;

namespace Place.Core.Migrations
{
    public class CreateIfNotExistWithSeed : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        private const string UserPasswrod = "Adm!nPlace";
        private const string EmailDomain = "@place.com";
        private const string MotrukLastName = "Motruk";
        private const string AdminLastName = "Admin";

        private static readonly IList<string> AdminEmails = new List<string>
        {
            MotrukLastName + EmailDomain,
            MotrukLastName + EmailDomain
        };

        private static readonly string[][] Profiles =
        {
            new[] {"Nazar", MotrukLastName},
            new[] {"Admin", AdminLastName },
            new[] {"User", "User" }
        };

        protected override void Seed(ApplicationDbContext context)
        {
            InitializeDb(context);
        }

        public static void InitializeDb(ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == Consts.Consts.AdminRole))
            {
                Init(context);
            }
        }

        private static void Init(ApplicationDbContext context)
        {
            // init different things

            InitUsers(context);
        }

        private static void InitUsers(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            roleManager.Create(new IdentityRole { Name = Consts.Consts.AdminRole });
            roleManager.Create(new IdentityRole { Name = Consts.Consts.UserRole });

            foreach (var userRole in ConstantHelper.GetConstantFields(new CommonRoles()))
            {
                if (context.Roles.Any(r => r.Name == userRole.Value))
                    continue;
                var role = new IdentityRole { Name = userRole.Value };
                roleManager.Create(role);
            }

            var userHelper = new UserHelper(context);
            var emails = new List<string>();


            foreach(var profile in Profiles)
            {
                var email = profile[1] + EmailDomain;
                var user = userHelper.GetUser(email);
                emails.Add(email);

                if (user != null) continue;

                user = new ApplicationUser
                {
                    FirstName = profile[0],
                    LastName = profile[1],
                    
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true,
                    DateCreated = DateTime.Now
                };
                userManager.Create(user, UserPasswrod);
                context.SaveChanges();
                if (AdminEmails.Contains(user.Email))
                {
                    userManager.AddToRole(user.Id, Consts.Consts.AdminRole);
                    userManager.AddToRole(user.Id, CommonRoles.GlobalAdmin);
                }
                userManager.AddToRole(user.Id, Consts.Consts.UserRole);
            }
            context.SaveChanges();

        }


        private class UserHelper
        {
            public UserHelper(ApplicationDbContext context)
            {
                Context = context;
            }

            private ApplicationDbContext Context { get; set; }

            public ApplicationUser GetUser(string email)
            {
                return Context.Users.FirstOrDefault(u => u.Email.Equals(email));
            }
        }
    }
}
