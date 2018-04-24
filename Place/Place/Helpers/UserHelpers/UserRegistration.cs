using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Place.Core.Consts;
using Place.Core.Data.Entites;
using WebGrease.Css.Extensions;

namespace Place.Web.Helpers.UserHelpers
{
    public abstract class UserRegistration
    {
        private ApplicationUserManager UserManager { get; set; }

        public UserRegistration(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public async Task<ApplicationUser> Register(string firstName, string lastName, string login, string password, bool emailConfirmed)
        {
            var user = CreateApplicationUser(firstName, lastName, login, emailConfirmed);
            var result = await UserManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var message = "";
                if (result.Errors.Any())
                {
                    result.Errors.ForEach(x => message = message + x);
                }
                throw new Exception(message);
            }
            await UserManager.AddToRolesAsync(user.Id, Consts.UserRole);

            return user;
        }

        public static ApplicationUser CreateApplicationUser(string firstName, string lastName, string login,
            bool emailConfirmed)
        {
            return new ApplicationUser
            {
                FirstName = firstName,
                LastName = lastName,
                EmailConfirmed = emailConfirmed,
                UserName = Guid.NewGuid().ToString(),
                DateCreated = DateTime.Now,
                Email = login
            };
        }


        public static string GeneratePassword()
        {
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder builder = new StringBuilder();
            char ch;
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                ch = input[random.Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public abstract void SendConfirmation(ApplicationUser user, string password);
    }
}