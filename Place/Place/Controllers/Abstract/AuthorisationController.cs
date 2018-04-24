using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Place.Core.Data.Entites;
using Place.Services.Services.Factory;
using Place.Web.Helpers.UserHelpers;

namespace Place.Web.Controllers.Abstract
{
    public class AuthorisationController : GeneralController
    {
        private readonly GeneralController _controller;

        protected AuthorisationController(IServiceManager serviceManager) : base(serviceManager)
        {
            _controller = this;
        }

        public async Task<ApplicationUser> RegisterUser(string login, string firstName, string lastName, string password)
        {
            return await RegisterUser(login, firstName, lastName, password, null);
        }

        public async Task<ApplicationUser> RegisterUser(string login, string firstName, string lastName, string password,
    UserLoginInfo userLoginInfo)
        {
            password = string.IsNullOrEmpty(password) ? UserRegistration.GeneratePassword() : password;
            var provider = new EmailRegistration(this, UserManager);
            var user = await provider.Register(firstName, lastName, login, password, false);

            if (userLoginInfo != null)
                await UserManager.AddLoginAsync(user.Id, userLoginInfo);

            // sending not realized
            provider.SendConfirmation(user, password);

            return user;
        }

        protected class EmailRegistration : UserRegistration
        {
            private readonly GeneralController _controller;
            private readonly IServiceManager _serviceManager;
            private readonly ApplicationUserManager _userManager;

            public EmailRegistration(GeneralController controller, ApplicationUserManager userManager)
                : base(userManager)
            {
                _userManager = userManager;
                _serviceManager = controller.ServiceManager;
                _controller = controller;
            }


            [Authorize]
            [HttpPost]
            [ValidateAntiForgeryToken]
            public override void SendConfirmation(ApplicationUser user, string password)
            {
                var token = _userManager.GenerateEmailConfirmationToken(user.Id);
                var callbackUrl = _controller.Url.Action("ConfirmEmail", "Account", new { code = token, redirectUrl = "" }, _controller?.Request?.Url?.Scheme);
                try
                {
                    //use ServicesBus
                    //EmailConfirmationRegistration confirmEmail = new EmailConfirmationRegistration(user.UserName, callbackUrl, password);
                    //ServiceBusHelper.SendMessageConfirmRegistrationAsync(confirmEmail).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    //ignore this
                }
            }
        }
    }
}