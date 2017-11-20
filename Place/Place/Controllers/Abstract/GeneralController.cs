using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity.Owin;
using Place.Core.Consts;
using Place.Core.Data.Entites;
using Place.Core.Repositories.Abstract;
using Place.Services.Services.EntityServices;
using Place.Services.Services.Factory;

namespace Place.Web.Controllers.Abstract
{
    public class GeneralController : Controller
    {
        private HttpContextBase _context;
        private ApplicationUserManager _userManager;
        private ApplicationUser _currentUser;

        protected IApplicationUserService ApplicationUserService;
        public IServiceManager ServiceManager;

        protected IUnitOfWork UnitOfWork;

        protected GeneralController(IServiceManager serviceManager)
        {
            ServiceManager = serviceManager;
            ApplicationUserService = ServiceManager.ApplicationUserService;
        }

        public ApplicationUser CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    var identity = _context.User.Identity;
                    _currentUser = ApplicationUserService.GetApplicationUser(identity.Name, true);
                }

                return _currentUser;
            }
            set { _currentUser = value; }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            protected set
            {
                _userManager = value;
            }
        }

        protected override void Initialize(RequestContext rc)
        {
            _context = rc.HttpContext;

            if (_context.Request[Consts.RequestDirect] != "1")
            {
                if (CurrentUser != null)
                {
                    ViewBag.CurrentUser = CurrentUser;
                }
            }

            base.Initialize(rc);
        }
    }
}